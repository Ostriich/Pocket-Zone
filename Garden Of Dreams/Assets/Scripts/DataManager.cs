using UnityEngine;
using System.IO;

public class DataManager
{
    // Send new info in dataFile
    public static void SaveBag(BagController bagController)
    {
        string path = Application.persistentDataPath + "/Bag.data";

        BagData data = new BagData(bagController);
        string jsonString = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonString);
    }

    // Load saved info from dataFile
    public static BagData LoadBag()
    {
        string path = Application.persistentDataPath + "/Bag.data";

        if (File.Exists(path))
        {
            string fileData = File.ReadAllText(path);
            BagData data = JsonUtility.FromJson<BagData>(fileData);

            return data;
        }
        else return null;
    }
}
