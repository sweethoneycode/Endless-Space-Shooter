using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public SaveDataNow activeSave;

    public static SaveManager instance;

    public bool hasLoaded;


    private void Awake()
    {
        instance = this;
        Load();

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
       Load();

    }

    public void Create(string user)
    {
        activeSave.saveName = user;
        Save();

    }


    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        string path2 = "/saves";

        string pathDir = Path.Combine(dataPath, path2);

        var serializer = new XmlSerializer(typeof(SaveDataNow));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".sav", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();

        hasLoaded = true;
    }

    public void playerLoad(string saveName)
    {
        try
        {
            activeSave.saveName = null;
        }
        finally
        {
            activeSave.saveName = saveName;
        }

        string dataPath = Application.persistentDataPath;
        string path2 = "/saves";

        string pathDir = Path.Combine(dataPath, path2);

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".sav"))
        {
            var serializer = new XmlSerializer(typeof(SaveDataNow));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".sav", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveDataNow;
            stream.Close();

        }
    }

    public void Load()
    {
        if (activeSave.saveName == "")
        {
            activeSave.saveName = "user1";
        }


        string dataPath = Application.persistentDataPath;
        string path2 = "/saves";

        string pathDir = Path.Combine(dataPath, path2);

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".sav"))
        {
            var serializer = new XmlSerializer(typeof(SaveDataNow));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".sav", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveDataNow;
            stream.Close();

        }
    }
}

[System.Serializable]

public class SaveDataNow
{
    public string saveName;
    public string playerName;
    public float highScore;
    public float soundVolume;
}
