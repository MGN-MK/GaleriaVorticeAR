using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TreeEditor;

public static class CS_SaveSystem
{
    private static string path = Path.Combine(Application.persistentDataPath + "/SaveApp.gvs");    

    public static void SaveTexture(Texture2D tex)
    {
        Texture2D UnTex = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);

        UnTex.SetPixels(tex.GetPixels());
        tex.Apply();

        byte[] texBytes = UnTex.EncodeToPNG();

        TextureData texData = new TextureData
        {
            data = texBytes,
            width = tex.width,
            height = tex.height,
            format = TextureFormat.ARGB32,
        };

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, texData );
        }

        Debug.Log("Texture saved in " + path);
    }

    public static Texture2D LoadTexture()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            TextureData textureData;
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                textureData = (TextureData)formatter.Deserialize(stream);
            }

            Texture2D tex = new Texture2D(textureData.width, textureData.height);
            tex.LoadImage(textureData.data);

            Debug.Log("Texture loaded from " + path);
            return tex;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }


    //Saves the app data
    public static void SaveAppData(CS_GalleryManager gallery, CS_AudioManager audioManager)
    {     
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        AppData data = new AppData(gallery, audioManager);
        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved");
    }

    //Reads the gallery data of a binary file of type .gvs
    public static AppData LoadAppData()
    {     
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AppData loadedData = bf.Deserialize(stream) as AppData;
            stream.Close();
            Debug.Log("Loaded");
            //Return the converted App Data
            return loadedData;
        }
        else
        {
            //If the save file doesn´t exists, load the error and returns null
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
    
}
