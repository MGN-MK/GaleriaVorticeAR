using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_GeneralManager : MonoBehaviour
{
    public static CS_GeneralManager instance;

    public CS_Catalogue catalogue;
    public GameObject ARItem;
    public GameObject[] menus;
    public Slider _musicSlider, _sfxSlider;

    private CS_ArtInfo selectedArt;

    //If there is no Audio Manager, add this, otherwise, destroy this
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
        //Set the current art from the cathaloge as the selected art
        selectedArt = catalogue.currentArt;

        //Set scale of the canvas object with the size of the selected art
        ARItem.GetComponent<MeshRenderer>().material.mainTexture = selectedArt.img.texture;
        ARItem.transform.localScale = new Vector3(selectedArt.sizes[0].y / 100, selectedArt.sizes[0].x / 100, 0.05f);
    }

    //Actives and deactives the UIs
    public void ActiveUI(GameObject btn)
    {
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

    //Closes the app (Add confirmation screen
    public void Exit()
    {
        Application.Quit();
    }
}
