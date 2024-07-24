using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppData
{
    //To save the elements in the gallery
    public TextureData[] galleryItems = new TextureData[24];

    //To save the elements fo the audio system
    public bool musicOn, sfxOn;
    public float musicVolume, sfxVolume;

    public TextureData SaveIMG(Texture2D img)
    {
        //Convert texture to bytes
        byte[] textureBytes = img.EncodeToPNG();

        //Create an object of type Te
        TextureData textureData = new TextureData
        {
            data = textureBytes,
            width = img.width,
            height = img.height,
            format = img.format,
        };
        
        return textureData;
    }

    public AppData (CS_GalleryManager SaveGallery, CS_AudioManager SaveAudio)
    {
        for (int i = 0; i < galleryItems.Length; i++)
        {
            if(galleryItems[i] != null)
            {
                galleryItems[i] = SaveIMG(SaveGallery.gallery[i].image.texture);
            }
        }

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
