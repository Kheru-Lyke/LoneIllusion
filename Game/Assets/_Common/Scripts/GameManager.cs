///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 19:24
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using Com.SchizophreniaStudios.LoneIllusionDestiny.Common.CharacterMovements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Com.SchizophreniaStudios.LoneIllusionDestiny.LoneIllusion;
using System.IO;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private DialogueChunk _currentDialogueChunk;

        [Header("UI management")]
        [SerializeField] private TextMeshProUGUI text = null;
        [SerializeField] private TextMeshProUGUI speakerName = null;
        [SerializeField] private Transform choiceUI = null;
        [SerializeField] private GameObject choiceButtonPrefab = null;

        [Header("Character management")]
        [SerializeField] private Transform left = null;
        [SerializeField] private Transform center = null;
        [SerializeField] private Transform right = null;
        [Space]
        [SerializeField] private GameObject characterVisualPrefab = null;

        [Header("Scene management")]
        [SerializeField] private Image background = null;
        [SerializeField] private AudioSource music = null;
        [SerializeField] private AudioSource backgroundEffect = null;

        private int lineIndex = 0;
        private CharacterChanging currentSpeaker = null;
        private Dictionary<CharacterChanging, GameObject> charactersInScene = new Dictionary<CharacterChanging, GameObject>();
        private DialogueLine line = null;


        /// <summary>
        /// Read a line, applies the proper changes and readies to read the next line.
        /// </summary>
        public void ReadLine()
        {

            if (lineIndex >= CurrentDialogueChunk.Lines.Length)                     //No more lines in this chunk, prevents error
            {
                Debug.LogWarning("[SCENE] No more lines to read in the current Dialogue Chunk");
                if (!text.GetComponent<TypewriterModuleUI>().Reveal(CurrentDialogueChunk.Lines[lineIndex-1].Text.Length)) return;   //Typewriter-reveal text management
                return;
            }

            line = CurrentDialogueChunk.Lines[lineIndex];

            if (!text.GetComponent<TypewriterModuleUI>().Reveal(line.Text.Length)) return;      //If the function was called to reveal the text
            if (line.Text != "") text.text = line.Text;

            if (currentSpeaker != line.Speaker)                 //Mangage speaker if it changed since the last line
            {
                ChangeSpeaker(line.Speaker);
                currentSpeaker = line.Speaker;
            }

            int length = line.CharacterMovement.Count;
            for (int i = 0; i < length; i++)                    //Manage character movements for this line, in order
            {
                if (line.CharacterMovement[i] is ChangeChunk) ChunkChange = line.CharacterMovement[i].Move;     //Manage the dialogue chunk changes
                line.CharacterMovement[i].Move();
            }

            if (line.Speaker.CheckState())                      //Manage the character forms changes with stats
            {
                ChangeSpeakerState(line.Speaker);
            }

            if (line.Anonymous) speakerName.text = "???";
            else speakerName.text = line.Speaker.CharacterName;

            UpdateCharacterSprite();                            //Manage sprite displayed with form & emotion
            if (line is DialogueChoice) DisplayChoice();


            lineIndex++;

            ChunkChange?.Invoke();          //Change chunk at the end if necessary
            ChunkChange = null;
        }

        /// <summary>
        /// Manages the cutscene for a character changing state
        /// </summary>
        /// <param name="speaker">The character changing state</param>
        private void ChangeSpeakerState(CharacterChanging speaker)
        {
            //TEMP
            Debug.Log("This is a change cutscene! Congrats! It's " + speaker.CharacterName);
            UpdateCharacterSprite();
        }

        /// <summary>
        /// Shows the choice UI
        /// </summary>
        private void DisplayChoice()
        {
            CanvasGroup Ui = choiceUI.GetComponentInParent<CanvasGroup>();
            if (Ui != null) Ui.interactable = true;

            GameObject button;
            DialogueChoice choice = line as DialogueChoice;

            for (int i = 0; i < choice.Choices.Count; i++)
            {
                var nameChunkPair = choice.Choices.ElementAt(i);

                button = Instantiate(choiceButtonPrefab, choiceUI);

                button.GetComponentInChildren<TextMeshProUGUI>().text = nameChunkPair.Key;
                button.GetComponent<Button>().onClick.AddListener(delegate
                {
                    CurrentDialogueChunk = nameChunkPair.Value;
                    Ui.interactable = false;

                    for (int j = Ui.transform.childCount - 1; j >= 0; j--)
                    {
                        Destroy(Ui.transform.GetChild(j).gameObject);
                    }

                    ReadLine();
                });
            }
        }


        public DialogueChunk CurrentDialogueChunk
        {
            get => _currentDialogueChunk;
            set
            {
                if (value == null)
                {
                    Debug.LogError("No new dialogue chunk");
                    return;
                }
                lineIndex = 0;
                _currentDialogueChunk = value;
            }
        }

        /// <summary>
        /// Textbox management (text style, etc...)
        /// </summary>
        /// <param name="newSpeaker">The character speaking</param>
        private void ChangeSpeaker(CharacterChanging newSpeaker)
        {
            text.font = newSpeaker.TextFont;
            text.fontStyle = newSpeaker.TextFontStyle;
            text.color = newSpeaker.TextColor;
        }

        /// <summary>
        /// Automatically manages character sprite (state / emotion)
        /// </summary>
        private void UpdateCharacterSprite()
        {
            charactersInScene.TryGetValue(line.Speaker, out GameObject visual);
            if (visual != null)
            {
                Image visualIMG = visual.GetComponent<Image>();
                if (visualIMG != null) visualIMG.sprite = line.Speaker.Sprites[line.Emotion];
            }
        }

        /// <summary>
        /// Creates a new character visual with the current line speaker parameters
        /// </summary>
        public void AddCharacterToScene()
        {
            if (charactersInScene.ContainsKey(line.Speaker))
            {
                Debug.LogWarning("[SCENE] Trying to add a character that already exists");
                return;
            }
            GameObject characterVisual = Instantiate(characterVisualPrefab, center);

            charactersInScene.Add(line.Speaker, characterVisual);
        }

        /// <summary>
        /// Forces a character in the scene, no check if the character already exists
        /// </summary>
        /// <param name="character">The character to force in the scene</param>
        /// <param name="parent">The part of the scene to add the character to</param>
        public void ForceCharacterInScene(CharacterChanging character, Transform parent)
        {
            GameObject characterVisual = Instantiate(characterVisualPrefab, parent);

            charactersInScene.Add(character, characterVisual);

            Image visualIMG = characterVisual.GetComponent<Image>();
            if (visualIMG != null) visualIMG.sprite = character.Sprites[Emotions.NONE];

        }

        internal void RemoveCharacterFromScreen()
        {
            Destroy(charactersInScene[currentSpeaker]);
            charactersInScene[currentSpeaker] = null;
        }

        public void MoveTo(Position position)
        {
            Transform target;
            if (position == Position.LEFT) target = left;
            else if (position == Position.CENTER) target = center;
            else target = right;

            charactersInScene[line.Speaker]?.transform.SetParent(target);
        }

        /// <summary>
        /// Scene management
        /// </summary>
        /// <param name="newBackground">The background image to use</param>
        /// <param name="newMusic">The music to use</param>
        /// <param name="newBackgroundEffect">The ambient sound / background noise to use</param>
        public void ChangeScene(Sprite newBackground, AudioClip newMusic, AudioClip newBackgroundEffect)
        {
            background.sprite = newBackground;

            music.clip = newMusic;
            music.Play();

            backgroundEffect.clip = newBackgroundEffect;
            backgroundEffect.Play();
        }

        public void SaveProgression(int saveNumber = 0)
        {
            Save saveState = new Save();

            saveState.background = background.sprite;
            saveState.music = music.clip;
            saveState.backgroundEffect = backgroundEffect.clip;
            saveState.currentDialogue = CurrentDialogueChunk;
            saveState.lineIndex = lineIndex;                            //Save the progression in the story

            saveState.playerData = PlayerCharacter.GetDataJSON();       //Save the player's stats

            saveState.characters = GetCharacterPositions();             //Save the character's position on the screen

            string filePath = Path.Combine(Application.persistentDataPath, "Save_" + saveNumber);
            File.WriteAllText(filePath, saveState);

            ScreenshotHandler.Instance.TakeScreenshot(Screen.width, Screen.height, filePath);       //Add a screenshot to the save
        }

        public void LoadProgression(int saveNumber)
        {
            string filePath = Path.Combine(Application.persistentDataPath, "Save_" + saveNumber);
            Save saveState = JsonUtility.FromJson<Save>(File.ReadAllText(filePath));

            PlayerCharacter.LoadDataJSON(saveState.playerData);
            LoadCharacters(saveState.characters);

            ChangeScene(saveState.background, saveState.music, saveState.backgroundEffect);
            CurrentDialogueChunk = saveState.currentDialogue;
            lineIndex = saveState.lineIndex;

            ReadLine();
        }

        private void LoadCharacters(List<KeyValuePair<Position, CharacterChanging>> characters)
        {
            int length = characters.Count;
            KeyValuePair<Position, CharacterChanging> chara;
            Transform parent = center;

            for (int i = 0; i < length; i++)
            {
                chara = characters.ElementAt(i);

                switch (chara.Key)
                {
                    case Position.LEFT:
                        parent = left;
                        break;
                    case Position.RIGHT:
                        parent = right;
                        break;
                    default:
                        parent = center;
                        break;
                }

                ForceCharacterInScene(chara.Value, parent);
            }
        }

        private List<KeyValuePair<Position, CharacterChanging>> GetCharacterPositions()
        {
            List<KeyValuePair<Position, CharacterChanging>> result = new List<KeyValuePair<Position, CharacterChanging>>();

            int length = charactersInScene.Count;
            KeyValuePair<CharacterChanging, GameObject> originKey;
            KeyValuePair<Position, CharacterChanging> resultKey;

            for (int i = 0; i < length; i++)
            {
                originKey = charactersInScene.ElementAt(i);

                if (originKey.Value == center) resultKey = new KeyValuePair<Position, CharacterChanging>(Position.CENTER, originKey.Key);
                else if (originKey.Value == left) resultKey = new KeyValuePair<Position, CharacterChanging>(Position.LEFT, originKey.Key);
                else resultKey = new KeyValuePair<Position, CharacterChanging>(Position.RIGHT, originKey.Key);

                result.Add(resultKey);
            }

            return result;
        }

        //  Instance related code
        private static GameManager instance;
        private Action ChunkChange;

        public static GameManager Instance { get { return instance; } }


        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void OnDestroy()
        {
            if (this == instance) instance = null;
        }
    }

    [Serializable]
    public class Save
    {
        public Sprite background;
        public AudioClip music;
        public AudioClip backgroundEffect;
        public List<KeyValuePair<Position, CharacterChanging>> characters = new List<KeyValuePair<Position, CharacterChanging>>();
        public DialogueChunk currentDialogue;
        public int lineIndex;

        public string playerData;

        public static implicit operator string(Save save)
        {
            return JsonUtility.ToJson(save);
        }
    }
}