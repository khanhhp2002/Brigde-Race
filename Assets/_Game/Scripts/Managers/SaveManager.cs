using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

    public void SaveData<T>(T data, string fileName)
    {
        string path = $"{Application.persistentDataPath}/{fileName}.dat";
        FileStream file = File.Open(path, FileMode.OpenOrCreate);
        _binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public bool LoadData<T>(ref T data, string fileName)
    {
        if (!IsFileExist(fileName))
        {
            return false;
        }
        string path = $"{Application.persistentDataPath}/{fileName}.dat";
        FileStream file = File.Open(path, FileMode.Open);
        data = (T)_binaryFormatter.Deserialize(file);
        file.Close();
        return true;
    }

    public bool IsFileExist(string fileName)
    {
        string path = $"{Application.persistentDataPath}/{fileName}.dat";
        return File.Exists(path);
    }
}
