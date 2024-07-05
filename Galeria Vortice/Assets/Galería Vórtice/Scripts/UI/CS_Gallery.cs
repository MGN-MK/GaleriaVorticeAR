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
    public GameObject closeUp, screenshotPrfb, grid;
    private List<CS_Screenshot> gallery;
    [SerializeField] Image showScreenshot;
    private int index = 0;

    public void AddToGallery(Sprite _screenshot)
    {
        GameObject item = Instantiate(screenshotPrfb);

        item.GetComponent<RectTransform>().SetParent(grid.transform); 
        item.GetComponent<RectTransform>().localPosition = Vector3.zero;
        item.GetComponent<RectTransform>().localScale = Vector3.one;
        item.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
        item.GetComponent<Image>().sprite = _screenshot;

        var element = item.GetComponent<CS_Screenshot>();
        element.Set(_screenshot, gallery.Count);
        gallery.Add(element);
    }

    //Selects the screenshot from the tapped button to the closeup
    public void SelectScreenshot(CS_Screenshot item)
    {
        index = item.id;

        //Sets the closeUpScreenshot´s sprite with the one corresponding to the gallery
        showScreenshot.sprite = gallery[index].image;
        closeUp.SetActive(true);
    }

    //On closeup changes with the next screenshot
    public void ChangeScreenshot(int value)
    {
        //Adds the value(-1 or 1) to get the previus or next item
        index += value;

        //Sets the index in case of get outside the array
        if (index < 0) { index = gallery.Count - 1; }
        if (index > gallery.Count - 1) { index = 0; }

        //Sets the closeUpScreenshot´s sprite with the one corresponding to the gallery
        showScreenshot.sprite = gallery[index].image;
    }

    public void SaveScreenshot()
    {
        //NativeGallery.SaveImageToGallery(newScreenshot, albumName, "GVScreenshot_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
    }

    public void ReturnToGallery()
    {
        closeUp.SetActive(false);
    }
}
