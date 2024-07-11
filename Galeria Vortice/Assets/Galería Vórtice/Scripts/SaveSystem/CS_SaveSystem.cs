using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CS_SaveSystem
{
    //Saves the gallery data in a binary file of type .gvs
    public static void SaveAppData(CS_GalleryManager gallery, CS_AudioManager audioManager)
    {
        //Set a formatter to save the file in binary
        BinaryFormatter formatter = new BinaryFormatter();

        //Set the path to save the save file, .gvs is an invented file type under galleria vortice save
        string path = Application.persistentDataPath + "/SaveApp.gvs";

        //Set filestream to save the file
        FileStream stream = new FileStream(path, FileMode.Create);

        //Sets the data with the GalleryData script
        AppData data = new AppData(gallery,audioManager);

        //Saves the data in the path with the binary format
        formatter.Serialize(stream, data);
    }

    //Reads the gallery data of a binary file of type .gvs
    public static AppData LoadAppData()
    {
        string path = Application.persistentDataPath + "/SaveApp.gvs";

        if(File.Exists(path))
        {
            //Set a formatter to save the file in binary and a filestream to open the file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            //Coverts the binary data to GalleryData
            AppData data = formatter.Deserialize(stream) as AppData;

            //Close filestream to avoid errors
            stream.Close();

            //Return the converted Gallery Data
            return data;
        }
        else
        {
            //If the save file doesn;t exists, load the error and returns null
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
