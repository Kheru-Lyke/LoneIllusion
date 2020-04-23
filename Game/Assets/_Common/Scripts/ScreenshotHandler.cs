using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{

    private Camera screenShotCamera = null;
    private bool takeScreenshotOnNextFrame = false;
    private string screenShotPath;

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = screenShotCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            File.WriteAllBytes(screenShotPath+ ".png", byteArray);
            Debug.Log("Saved " + screenShotPath + ".png");

            RenderTexture.ReleaseTemporary(renderTexture);
            screenShotCamera.targetTexture = null;
        }
    }

    public void TakeScreenshot(int width, int height, string path)
    {
        screenShotCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        screenShotPath = path;

        takeScreenshotOnNextFrame = true;
    }
    public static Sprite GetSpriteFromFile(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);
        return Sprite.Create(texture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.0f), 1.0f);
    }


    //  Instance related code
    private static ScreenshotHandler instance;

    public static ScreenshotHandler Instance { get { return instance; } }


    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        screenShotCamera = GetComponent<Camera>();
        screenShotPath = Application.dataPath;
    }

    private void OnDestroy()
    {
        if (this == instance) instance = null;
    }
}
