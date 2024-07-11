using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppData
{
    //To save the elements in the gallery
    public CS_Screenshot[] galleryItems = new CS_Screenshot[24];

    //To save the elements fo the audio system
    public bool musicOn, sfxOn;
    public float musicVolume, sfxVolume;

    public AppData (CS_GalleryManager SaveGallery, CS_AudioManager SaveAudio)
    {
        //Save the gallery
        galleryItems = SaveGallery.gallery;

        //Save the audio config
        musicOn = SaveAudio.musicSource.mute;
        sfxOn = SaveAudio.sfxSource.mute;
        musicVolume = SaveAudio.musicSource.volume;
        sfxVolume = SaveAudio.sfxSource.volume;
    }
}
