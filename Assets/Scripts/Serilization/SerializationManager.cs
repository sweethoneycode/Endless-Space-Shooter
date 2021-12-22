using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SerializationManager
{
    public static bool Save(string saveName, object saveData)
    {

        string path1 = Application.persistentDataPath;
        string path2 = "/saves";

        string pathDir = Path.Combine(path1, path2);

        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(pathDir))
        {
            Directory.CreateDirectory(pathDir);
        }

        string path3 = saveName + ".save";
        string pathFile = Path.Combine(path1, path2, path3);

        FileStream file = File.Create(pathFile);

        formatter.Serialize(file, saveData);

        file.Close();

        Debug.Log(pathFile);

        return true;

    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("failed to load file at {0}", path);
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        
        return formatter;
    }
}
