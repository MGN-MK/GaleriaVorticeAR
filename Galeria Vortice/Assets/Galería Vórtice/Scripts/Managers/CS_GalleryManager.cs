using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Data;

public class CS_GalleryManager : MonoBehaviour
{
    public string albumName;
    public GameObject closeUp, screenshotPrfb, grid;
    public CS_Screenshot[] gallery = new CS_Screenshot[24];
    [SerializeField] Image showScreenshot;
    public int galleryCount = 0;
    private int index = 0;
    private GameObject item;

    public void AddToGallery(Sprite _screenshot)
    {
        

        if (gallery[galleryCount] == null)
        {
            item = Instantiate(screenshotPrfb);

            item.GetComponent<RectTransform>().SetParent(grid.transform);
            item.GetComponent<RectTransform>().localPosition = Vector3.zero;
            item.GetComponent<RectTransform>().localScale = Vector3.one;
            item.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            item = gallery[galleryCount].gameObject;
        }
        
        item.GetComponent<Image>().sprite = _screenshot;

        var element = item.GetComponent<CS_Screenshot>();        
        gallery.SetValue(element, galleryCount);        
        element.Set(_screenshot, galleryCount);

        if(galleryCount >= 23)
        {
            galleryCount = 0;
        }
        else
        {
            galleryCount++;
        }
    }

    //Selects the screenshot from the tapped button to the closeup
    public void SelectScreenshot(CS_Screenshot screenshot)
    {
        index = screenshot.id;

        //Sets the closeUpScreenshot´s sprite with the one corresponding to the gallery
        showScreenshot.sprite = gallery[index].image;
        closeUp.SetActive(true);
    }

    //On closeup changes the screenshot
    public void ChangeScreenshot(int value)
    {
        CS_AudioManager.instance.PlaySFX("UI");
        int notFoundTries = 0;

        //Adds the value(-1 or 1) to get the previus or next item
        index += value;

        //Sets the index in case of get outside the array
        if (index < 0)
        {
            index = gallery.Length - 1;
        }
        if (index > gallery.Length - 1)
        {
            index = 0;
        }

        //Checks the value to look left or right
        if (value < 0)
        {
            //Search for a not null item in gallery in 24 attemps
            while (gallery[index] == null && notFoundTries <= 23)
            {
                index--;

                //Sets the index in case of get outside the array
                if (index < 0)
                {
                    index = gallery.Length - 1;
                }

                notFoundTries++;
            }
        }
        else
        {
            //Search for a not null item in gallery in 24 attemps
            while (gallery[index] == null && notFoundTries <= 23)
            {
                index++;
                //Sets the index in case of get outside the array
                if (index > gallery.Length - 1)
                {
                    index = 0;
                }

                notFoundTries++;
            }
        }

        //Checks if the selected item is null to change the image or close the closeup menu
        if (gallery[index] != null)
        {
            //Sets the closeUpScreenshot´s sprite with the one corresponding to the gallery
            showScreenshot.sprite = gallery[index].image;
        }
        else
        {
            ReturnToGallery();
        }
    }

    //Saves the screenshot in the device
    public void SaveScreenshot()
    {
        //Play the sfx for save
        CS_AudioManager.instance.PlaySFX("Save");

        //Actually saves the image
        NativeGallery.SaveImageToGallery(gallery[index].image.texture, albumName, "GVScreenshot_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
    }

    //Closes the closeup menu
    public void ReturnToGallery()
    {
        CS_AudioManager.instance.PlaySFX("UI");
        closeUp.SetActive(false);
    }

    //Deletes the current screenshot
    public void DeleteScreenshot()
    {
        StartCoroutine("Delete");
        //CS_GeneralManager.instance.Save();
    }

    //
    private IEnumerator Delete()
    {
        CS_AudioManager.instance.PlaySFX("Erase");
        Handheld.Vibrate();
        CS_SaveSystem.DeleteFile(index);
        Destroy(gallery[index].gameObject);
        showScreenshot.color = Color.gray;

        yield return new WaitForSeconds(2);
        showScreenshot.color = Color.white;
        int tempIndex = 0;        

        foreach (var item in gallery)
        {
            if (item != null)
            {
                item.id = tempIndex;
                tempIndex++;
                galleryCount = tempIndex;
            }
        }

        ChangeScreenshot(0);
    }
}
