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


        //  Dialogue management
        public void ReadLine()
        {
            if (lineIndex >= CurrentDialogueChunk.Lines.Length)
            {
                Debug.LogWarning("[SCENE] No more lines to read in the current Dialogue Chunk");
                return;
            }

            line = CurrentDialogueChunk.Lines[lineIndex];


            line.CharacterMovement?.Move();
            if (line.Speaker.CheckState())
            {
                ChangeSpeakerState(line.Speaker);
            }
            if (currentSpeaker != line.Speaker)
            {
                ChangeSpeaker(line.Speaker);
                currentSpeaker = line.Speaker;
            }

            if (line.Text != "") text.text = line.Text;
            if (line.Anonymous) speakerName.text = "???";
            else speakerName.text = line.Speaker.CharacterName;

            UpdateCharacterSprite();
            if (line is DialogueChoice) DisplayChoice();

            lineIndex++;
        }

        private void ChangeSpeakerState(CharacterChanging speaker)
        {
            //TEMP
            Debug.Log("This is a change cutscene! Congrats! It's " + speaker.CharacterName);
            UpdateCharacterSprite();
        }

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

        //  Textbox management
        private void ChangeSpeaker(CharacterChanging newSpeaker)
        {
            text.font = newSpeaker.TextFont;
            text.fontStyle = newSpeaker.TextFontStyle;
            text.color = newSpeaker.TextColor;
        }

        //  Character management
        private void UpdateCharacterSprite()
        {
            charactersInScene.TryGetValue(line.Speaker, out GameObject visual);
            if (visual != null)
            {
                Image visualIMG = visual.GetComponent<Image>();
                if (visualIMG != null) visualIMG.sprite = line.Speaker.Sprites[line.Emotion];
            }
        }

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

        //  Scene Management

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
            saveState.lineIndex = lineIndex - 1;

            saveState.playerData = PlayerCharacter.GetDataJSON();

            saveState.characters = GetCharacterPositions();

            string filePath = Path.Combine(Application.persistentDataPath, "Save_" + saveNumber);
            File.WriteAllText(filePath, saveState);

            ScreenshotHandler.Instance.TakeScreenshot(Screen.width, Screen.height, filePath);
        }

        public void LoadProgression(int saveNumber)
        {
            string filePath = Path.Combine(Application.persistentDataPath, "Save_" + saveNumber);
            Save saveState = JsonUtility.FromJson<Save>(File.ReadAllText(filePath));

            PlayerCharacter.LoadDataJSON(saveState.playerData);

            ChangeScene(saveState.background, saveState.music, saveState.backgroundEffect);
            CurrentDialogueChunk = saveState.currentDialogue;
            lineIndex = saveState.lineIndex;

            LoadCharacters(saveState.characters);

            ReadLine();
        }

        private void LoadCharacters(PositionCharacter_SerializableDictionary characters)
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

        private PositionCharacter_SerializableDictionary GetCharacterPositions()
        {
            PositionCharacter_SerializableDictionary result = new PositionCharacter_SerializableDictionary();

            int length = charactersInScene.Count;
            KeyValuePair<CharacterChanging, GameObject> keyValuePair;

            for (int i = 0; i < length; i++)
            {
                keyValuePair = charactersInScene.ElementAt(i);

                if (keyValuePair.Value == center) result.Add(Position.CENTER, keyValuePair.Key);
                else if (keyValuePair.Value == left) result.Add(Position.LEFT, keyValuePair.Key);
                else result.Add(Position.RIGHT, keyValuePair.Key);
            }

            return result;
        }

        //  Instance related code
        private static GameManager instance;
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
        public PositionCharacter_SerializableDictionary characters = new PositionCharacter_SerializableDictionary();
        public DialogueChunk currentDialogue;
        public int lineIndex;

        public string playerData;

        public static implicit operator string(Save save)
        {
            return JsonUtility.ToJson(save);
        }
    }
}