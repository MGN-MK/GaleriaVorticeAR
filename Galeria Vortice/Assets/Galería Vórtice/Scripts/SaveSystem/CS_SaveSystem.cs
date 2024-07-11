using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CS_SaveSystem
{
    private static string path = Path.Combine(Application.persistentDataPath + "/SaveApp.json");    

    //Saves the app data
    public static void SaveAppData(CS_GalleryManager gallery, CS_AudioManager audioManager)
    {     
        //Sets the data with the GalleryData script
        AppData data = new AppData(gallery,audioManager);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    //Reads the gallery data of a binary file of type .gvs
    public static AppData LoadAppData()
    {     
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            AppData loadedData = JsonUtility.FromJson<AppData>(json);

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
