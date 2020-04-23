using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveUIManager : MonoBehaviour
{
    [SerializeField] private GameObject saveButtonPrefab = null;
    [SerializeField] private Transform saveParent = null;
    [SerializeField] private int saveStateNumber = 6;

    private void Start()
    {
        SaveUI();
    }

    public void LoadUI()
    {
        CreateButtons(true);
    }

    public void SaveUI()
    {
        CreateButtons(false);
    }

    private void CreateButtons(bool load)
    {
        GameObject saveState;
        UnityAction OnClickDo;

        for (int i = 0; i < saveStateNumber; i++)
        {
            saveState = Instantiate(saveButtonPrefab, saveParent);
            int btnNmbr = i;

            if (load)
            {
                if (!File.Exists(Path.Combine(Application.persistentDataPath, "Save_" + i))) saveState.GetComponent<Button>().interactable = false;
                OnClickDo = delegate { GameManager.Instance.LoadProgression(btnNmbr); };
            }
            else OnClickDo = delegate { GameManager.Instance.SaveProgression(btnNmbr); };

            if (File.Exists(Path.Combine(Application.persistentDataPath, "Save_" + i + ".png")))
            {
                byte[] bytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "Save_" + i + ".png"));
                Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
                texture.filterMode = FilterMode.Trilinear;
                texture.LoadImage(bytes);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.0f), 1.0f);

                saveState.GetComponent<Image>().sprite = sprite;
            }


            saveState.GetComponent<Button>().onClick.AddListener(OnClickDo);
        }
    }
}
