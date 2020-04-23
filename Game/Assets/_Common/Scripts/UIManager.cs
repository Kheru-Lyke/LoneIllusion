///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 23/04/2020 18:35
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common {
	public class UIManager : MonoBehaviour {

        

		private static UIManager instance;
		public static UIManager Instance { get { return instance; } }
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}