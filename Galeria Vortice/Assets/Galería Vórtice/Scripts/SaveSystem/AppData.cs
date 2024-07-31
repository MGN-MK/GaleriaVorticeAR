using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppData
{
    //To save the elements fo the audio system
    public bool musicOn, sfxOn;
    public float musicVolume, sfxVolume;

    public AppData (CS_AudioManager SaveAudio)
    {
        //Save the audio config
        musicOn = SaveAudio.musicSource.mute;
        sfxOn = SaveAudio.sfxSource.mute;
        musicVolume = SaveAudio.musicSource.volume;
        sfxVolume = SaveAudio.sfxSource.volume;
    }

}

[System.Serializable]
public class TextureData
{
    public byte[] data;
    public int width, height;
    public TextureFormat format;
}
