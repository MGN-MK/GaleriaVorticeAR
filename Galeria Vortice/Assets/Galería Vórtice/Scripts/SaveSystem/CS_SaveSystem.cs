using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TreeEditor;

public static class CS_SaveSystem
{
    private static string savePath = Path.Combine(Application.persistentDataPath + "/SaveApp.gvs");

    public static void SaveTexture(Texture2D tex, int i)
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

        var imgPath = Path.Combine(Application.persistentDataPath + "/SaveIMG" + i + ".gvs");

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(imgPath, FileMode.Create))
        {
            formatter.Serialize(stream, texData );
        }

        Debug.Log("Texture saved in " + imgPath);
    }

    public static Texture2D LoadTexture(int i)
    {
        var imgPath = Path.Combine(Application.persistentDataPath + "/SaveIMG" + i + ".gvs");

        if (File.Exists(imgPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            TextureData textureData;
            using (FileStream stream = new FileStream(imgPath, FileMode.Open))
            {
                textureData = (TextureData)formatter.Deserialize(stream);
            }

            Texture2D tex = new Texture2D(textureData.width, textureData.height);
            tex.LoadImage(textureData.data);

            Debug.Log("Texture loaded from " + imgPath);
            return tex;
        }
        else
        {
            Debug.Log("Save file not found in " + imgPath);
            return null;
        }
    }

    public static void DeleteFile(int i)
    {
        var deletePath = Path.Combine(Application.persistentDataPath + "/SaveIMG" + i + ".gvs");
        if (File.Exists(deletePath))
        {
            File.Delete(deletePath);
        }
    }


    //Saves the app data
    public static void SaveAppData(CS_AudioManager audioManager)
    {     
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Create);
        AppData data = new AppData(audioManager);
        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved Settings");
    }

    //Reads the gallery data of a binary file of type .gvs
    public static AppData LoadAppData()
    {     
        if(File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);

            AppData loadedData = bf.Deserialize(stream) as AppData;
            stream.Close();
            Debug.Log("Loaded");
            //Return the converted App Data
            return loadedData;
        }
        else
        {
            //If the save file doesn´t exists, load the error and returns null
            Debug.Log("Save file not found in " + savePath);
            return null;
        }
    }    
}
