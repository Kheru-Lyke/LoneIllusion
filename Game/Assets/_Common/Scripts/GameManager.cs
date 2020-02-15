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

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common {
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private DialogueChunk _currentDialogueChunk;

        [Header("UI management")]
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private Transform choiceUI;
        [SerializeField] private GameObject choiceButtonPrefab;

        [Header("Character management")]
        [SerializeField] private Transform left;
        [SerializeField] private Transform center;
        [SerializeField] private Transform right;
        [Space]
        [SerializeField] private GameObject characterVisualPrefab;

        [Header("Scene management")]
        [SerializeField] private Image background;
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource backgroundEffect;

        private int lineIndex = 0;
        private CharacterChanging currentSpeaker;
        private Dictionary<CharacterChanging, GameObject> charactersInScene = new Dictionary<CharacterChanging, GameObject>();
        private DialogueLine line;


        //  Dialogue management
        public void ReadLine()
        {
            line = CurrentDialogueChunk.Lines[lineIndex];


            if (line.Speaker.CheckState())
            {
                ChangeSpeakerState(line.Speaker);
                return;
            }
            if (currentSpeaker != line.Speaker)
            {
                ChangeSpeaker(line.Speaker);
                currentSpeaker = line.Speaker;
            }

            if (line.Text != "") text.text = line.Text;
            if (line.Anonymous) speakerName.text = "???";
            else speakerName.text = line.Speaker.CharacterName;

            line.CharacterMovement?.Move();
            UpdateCharacterSprite();
            if (line is DialogueChoice) DisplayChoice();

            lineIndex++;

            //  TEMP
            PlayerData.bluntLevel = PlayerData.cunningLevel = PlayerData.nobleLevel = PlayerData.trueLevel = 3;
        }

        private void ChangeSpeakerState(CharacterChanging speaker)
        {
            Debug.Log("This is a change cutscene! Congrats! It's " + speaker.CharacterName);
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
            GameObject characterVisual = Instantiate(characterVisualPrefab, center);

            charactersInScene.Add(line.Speaker, characterVisual);
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

        internal void ChangeScene(Sprite newBackground, AudioClip newMusic, AudioClip newBackgroundEffect)
        {
            background.sprite = newBackground;

            music.clip = newMusic;
            music.Play();

            backgroundEffect.clip = newBackgroundEffect;
            backgroundEffect.Play();
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
}