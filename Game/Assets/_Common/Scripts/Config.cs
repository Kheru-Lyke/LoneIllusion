///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 10/02/2020 20:28
///-----------------------------------------------------------------

using Com.SchizophreniaStudios.LoneIllusionDestiny.LoneIllusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common
{

    public enum LanguageAvailable
    {
        FR,
        EN
    }

    public class Config : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private SetUpSliders soundSliders;
        [SerializeField] private Button LoadGameButton;

        private LanguageAvailable _language = LanguageAvailable.EN;
        private LanguageAvailable _defaultLanguage = LanguageAvailable.EN;
        private float _masterVolume;
        private float _musicVolume;
        private float _bgEffectVolume;
        private float _effectsVolume;
        private float _voicesVolume;

        private Dictionary<string, LanguageAvailable> stringToLanguage = new Dictionary<string, LanguageAvailable>()
        {
            { "EN", LanguageAvailable.EN },
            { "FR", LanguageAvailable.FR }
        };
        public LanguageAvailable Language => _language;
        public LanguageAvailable DefaultLanguage => _defaultLanguage;

        public void SetMasterVolume(float volume)
        {
            _masterVolume = volume;
            audioMixer.SetFloat("MasterVolume", volume);
        }
        public void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
            audioMixer.SetFloat("MusicVolume", volume);
        }
        public void SetBgEffectsVolume(float volume)
        {
            _bgEffectVolume = volume;
            audioMixer.SetFloat("BgEffectsVolume", volume);
        }
        public void SeteffectsVolume(float volume)
        {
            _effectsVolume = volume;
            audioMixer.SetFloat("EffectsVolume", volume);
        }
        public void SetVoicesVolume(float volume)
        {
            _voicesVolume = volume;
            audioMixer.SetFloat("VoicesVolume", volume);
        }

        public void SetLanguage(string language)
        {
            if (!stringToLanguage.TryGetValue(language, out LanguageAvailable lang))
            {
                _language = _defaultLanguage;
                throw new KeyNotFoundException("Language doesn't exist. Switching to default.");
            }
            else _language = lang;
        }

        public void SaveConfig()
        {
            PlayerPrefs.SetString("Lang", _language.ToString());
            PlayerPrefs.SetFloat("Master", _masterVolume);
            PlayerPrefs.SetFloat("Music", _musicVolume);
            PlayerPrefs.SetFloat("BgEffect", _bgEffectVolume);
            PlayerPrefs.SetFloat("Effect", _effectsVolume);
            PlayerPrefs.SetFloat("Voices", _voicesVolume);
            PlayerPrefs.Save();
        }

        public void LoadConfig()
        {
            _language = stringToLanguage[PlayerPrefs.GetString("Lang")];
            _masterVolume = PlayerPrefs.GetFloat("Master");
            _musicVolume = PlayerPrefs.GetFloat("Music");
            _bgEffectVolume = PlayerPrefs.GetFloat("BgEffect");
            _effectsVolume = PlayerPrefs.GetFloat("Effect");
            _voicesVolume = PlayerPrefs.GetFloat("Voices");
            _savedGameChapter = PlayerPrefs.GetInt("Save", 0);

            soundSliders.SetUp();
            if (SavedGameChapter == 0) LoadGameButton.interactable = false;

            SetMasterVolume(_masterVolume);
            SetMusicVolume(_musicVolume);
            SetBgEffectsVolume(_bgEffectVolume);
            SeteffectsVolume(_effectsVolume);
            SetVoicesVolume(_voicesVolume);
        }

        private int _savedGameChapter;
        public void SaveGame(int chapterIndex)
        {
            _savedGameChapter = chapterIndex;
            PlayerPrefs.SetInt("Save", chapterIndex);
        }

        //  Instance related code
        private static Config instance;

        public static Config Instance { get { return instance; } }

        public float MasterVolume   => _masterVolume; 
        public float MusicVolume => _musicVolume;
        public float BgEffectVolum => _bgEffectVolume; 
        public float EffectsVolume => _effectsVolume; 
        public float VoicesVolume   => _voicesVolume;
        public int SavedGameChapter => _savedGameChapter; 

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void Start()
        {
            LoadConfig();
        }
        private void OnDestroy()
        {
            if (this == instance) instance = null;
        }
    }
}