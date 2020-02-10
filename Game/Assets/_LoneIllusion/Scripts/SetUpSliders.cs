///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 10/02/2020 21:47
///-----------------------------------------------------------------

using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.LoneIllusion {
	public class SetUpSliders : MonoBehaviour {

        [SerializeField] private Slider master;
        [SerializeField] private Slider music;
        [SerializeField] private Slider bgEffects;
        [SerializeField] private Slider effects;
        [SerializeField] private Slider voices;

        public void SetUp()
        {
            master.value = Config.Instance.MasterVolume;
            music.value = Config.Instance.MusicVolume;
            bgEffects.value = Config.Instance.BgEffectVolum;
            effects.value = Config.Instance.EffectsVolume;
            voices.value = Config.Instance.VoicesVolume;
        }
	}
}