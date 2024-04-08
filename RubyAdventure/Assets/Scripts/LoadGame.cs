using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public SaveData Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.txt");
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            SaveData data = (SaveData)formatter.Deserialize(file);
            file.Close();
            return data;
        }
        return null;
    }

}
