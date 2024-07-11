using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppData
{
    //To save the elements in the gallery
    public Sprite[] galleryItems = new Sprite[24];

    //To save the elements fo the audio system
    public bool musicOn, sfxOn;
    public float musicVolume, sfxVolume;

    public AppData (CS_GalleryManager SaveGallery, CS_AudioManager SaveAudio)
    {
        for (int i = 0; i < galleryItems.Length; i++)
        {
            if(galleryItems[i] != null)
            {
                galleryItems[i] = SaveGallery.gallery[i].image;
            }
        }

        //Save the audio config
        musicOn = SaveAudio.musicSource.mute;
        sfxOn = SaveAudio.sfxSource.mute;
        musicVolume = SaveAudio.musicSource.volume;
        sfxVolume = SaveAudio.sfxSource.volume;
    }
}
