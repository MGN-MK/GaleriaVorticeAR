using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Data;

public class CS_Gallery : MonoBehaviour
{
    public string albumName;
    [SerializeField] Image showScreenshot;
    public GameObject closeUpScreenshot, screenshotPrfb, gallery;

    //Takes a screenshot
    public void TakeScreenshot()
    {
        StartCoroutine(TakeandShowScreenshot());
    }

    private IEnumerator TakeandShowScreenshot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();

        Texture2D newScreenshot = new Texture2D(screenshot.width, screenshot.height, TextureFormat.RGB24, false);
        newScreenshot.SetPixels(screenshot.GetPixels());
        newScreenshot.Apply();

        Destroy(screenshot);

        Sprite screenshotSprite = Sprite.Create(newScreenshot, new Rect(0, 0, newScreenshot.width, newScreenshot.height), new Vector2(0.5f, 0.5f));

        showScreenshot.enabled = true;
        showScreenshot.sprite = screenshotSprite;

        yield return new WaitForSeconds(2);

        showScreenshot.enabled = false;
        NativeGallery.SaveImageToGallery(newScreenshot, albumName, "GVScreenshot_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
        Destroy(newScreenshot);
    }
}
