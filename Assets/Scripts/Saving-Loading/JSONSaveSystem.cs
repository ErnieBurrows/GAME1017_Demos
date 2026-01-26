using System.IO;
using UnityEngine;

public class JSONSaveSystem : MonoBehaviour
{
    [SerializeField] PlayerData data;
    string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/player.json";
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(filePath, json);

        Debug.Log("Saved JSON to: " + filePath);
    }

    public void Load()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("No JSON file found!");
            return;
        }

        string json = File.ReadAllText(filePath);
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);

        Debug.Log("Loaded from JSON:");
        Debug.Log(data.playerName);
        Debug.Log(data.level);
        Debug.Log(data.health);
    }
}
