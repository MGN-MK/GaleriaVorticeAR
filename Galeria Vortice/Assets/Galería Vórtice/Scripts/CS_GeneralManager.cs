using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CS_GeneralManager : MonoBehaviour
{
    public static CS_GeneralManager instance;

    public CS_Catalogue catalogue;
    public CS_Gallery gallery;
    public GameObject ARItem, ARUI, screenshotInfo;
    public GameObject[] menus;
    public Slider _musicSlider, _sfxSlider;
    public TextMeshProUGUI titletxt, yeartxt, artisttxt, techniquetxt, sizetxt;
    [SerializeField] Image showScreenshot;

    private CS_ArtInfo selectedArt;

    //If there is no General Manager, add this, otherwise, destroy this
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Starts the AR experience
    public void AR()
    {
        CS_AudioManager.instance.PlaySFX("UI");
        //Set the current art from the cathaloge as the selected art
        selectedArt = catalogue.currentArt;

        if (selectedArt != null)
        {
            //Update the text data to show
            titletxt.text = "Título: " + selectedArt.title;
            yeartxt.text = "Año: " + selectedArt.year.ToString();
            artisttxt.text = "Artista: " + selectedArt.artist.ToString().Replace("_", " ");
            techniquetxt.text = "Técnica: " + selectedArt.technique.ToString().Replace("_", " ");
            sizetxt.text = "Tamaño: " + selectedArt.sizes[0].x + " cm x " + selectedArt.sizes[0].y + " cm";
            sizetxt.text = "Tamaño: " + selectedArt.sizes[0].x + " cm x " + selectedArt.sizes[0].y + " cm";
        }

        //Set scale of the canvas object with the size of the selected art
        ARItem.GetComponent<MeshRenderer>().material.mainTexture = selectedArt.img.texture;
        ARItem.transform.localScale = new Vector3(selectedArt.sizes[0].y / 100, selectedArt.sizes[0].x / 100, 0.05f);
    }

    //Actives and deactives the UIs
    public void ActiveUI(GameObject btn)
    {
        CS_AudioManager.instance.PlaySFX("UI");
        string tag = btn.tag;

        foreach (var _menu in menus)
        {
            if( _menu.tag == tag)
            {
                _menu.SetActive(true);
            }
            else
            {
                _menu.SetActive(false);
            }
        }
    }

    //Takes a screenshot
    public void TakeScreenshot()
    {
        CS_AudioManager.instance.PlaySFX("Screenshot");
        ARUI.SetActive(false);
        screenshotInfo.SetActive(true);
        StartCoroutine(TakeandShowScreenshot());
    }

    //IEnumerator to set waiting times
    private IEnumerator TakeandShowScreenshot()
    {
        yield return new WaitForEndOfFrame();             

        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();        

        Texture2D newScreenshot = new Texture2D(screenshot.width, screenshot.height, TextureFormat.RGB24, false);
        newScreenshot.SetPixels(screenshot.GetPixels());
        newScreenshot.Apply();

        Destroy(screenshot);

        Sprite screenshotSprite = Sprite.Create(newScreenshot, new Rect(0, 0, newScreenshot.width, newScreenshot.height), new Vector2(0.5f, 0.5f));

        screenshotInfo.SetActive(false);
        ARUI.SetActive(true);

        showScreenshot.enabled = true;
        showScreenshot.sprite = screenshotSprite;

        yield return new WaitForSeconds(2);

        showScreenshot.enabled = false;
        gallery.AddToGallery(screenshotSprite);               
    }

    //From here is the settings menu
    //Mutes and unmutes the music
    public void ToggleMusic()
    {
        CS_AudioManager.instance.ToggleMusic();
    }

    //Mutes and unmutes the sfx
    public void ToggleSFX()
    {
        CS_AudioManager.instance.ToggleSFX();
    }

    //Changes the music volume
    public void MusicVolume()
    {
        CS_AudioManager.instance.MusicVolume(_musicSlider.value);
    }

    //Changes the sxf volume
    public void SFXVolume()
    {
        CS_AudioManager.instance.SFXVolume(_sfxSlider.value);        
    }


    //Closes the app (Add confirmation screen)
    public void Exit()
    {
        CS_AudioManager.instance.PlaySFX("UI");
        Application.Quit();
    }

}
