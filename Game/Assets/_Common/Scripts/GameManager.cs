///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 10/02/2020 22:02
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common {
	public class GameManager : MonoBehaviour {

        [SerializeField] string mainSceneName;
        [SerializeField] List<DialogueChunk> chapters;

        public void LoadChapter(int chapterIndex)
        {
            SceneManager.LoadScene(mainSceneName);
            SceneManager.sceneLoaded += delegate
            {
                VNManager.Instance.CurrentDialogueChunk = chapters[chapterIndex];
                VNManager.Instance.ReadLine();
            };
        }

        public void LoadGame()
        {
            LoadChapter(Config.Instance.SavedGameChapter);
        }

        public void StartNewGame()
        {
            LoadChapter(0);
        }

        //Instance related code
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

            DontDestroyOnLoad(this.gameObject);
        }
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}