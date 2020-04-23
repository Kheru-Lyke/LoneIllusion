using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveUIManager : MonoBehaviour
{
    [SerializeField] private GameObject saveButtonPrefab = null;
    [SerializeField] private Button back = null;
    [SerializeField] private Transform saveParent = null;
    [SerializeField] private int saveStateNumber = 6;

    [SerializeField] private Animator UiAnim = null;

    private bool toDestroy = false;

    public void LoadUI()
    {
        if (toDestroy) DestroyButtons();
        else CreateButtons(true);
        toDestroy = !toDestroy;
    }

    private void DestroyButtons()
    {
        for (int i = 0; i < saveParent.childCount; i++)
        {
            Destroy(saveParent.GetChild(i).gameObject);
        }

        back.onClick.RemoveAllListeners();
    }

    public void SaveUI()
    {
        if (toDestroy) DestroyButtons();
        else CreateButtons(false);
        toDestroy = !toDestroy;
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
                OnClickDo = delegate
                {
                    GameManager.Instance.LoadProgression(btnNmbr);
                    UiAnim.SetTrigger("Load");
                    UiAnim.SetTrigger("Start");
                };
                back.onClick.AddListener(delegate { UiAnim.SetTrigger("Load"); });
            }
            else
            {
                OnClickDo = delegate { GameManager.Instance.SaveProgression(btnNmbr);
                    UiAnim.SetTrigger("Save"); };
                back.onClick.AddListener(delegate { UiAnim.SetTrigger("Save"); });
            }

            if (File.Exists(Path.Combine(Application.persistentDataPath, "Save_" + i + ".png")))
            {
                Sprite sprite = ScreenshotHandler.GetSpriteFromFile(Path.Combine(Application.persistentDataPath, "Save_" + i + ".png"));
                saveState.GetComponent<Image>().sprite = sprite;
            }


            saveState.GetComponent<Button>().onClick.AddListener(OnClickDo);
        }

    }
}
