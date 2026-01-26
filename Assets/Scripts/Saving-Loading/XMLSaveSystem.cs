using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XMLSaveSystem : MonoBehaviour
{
    [SerializeField] PlayerData data;
    string filePath;


    void Start()
    {
        filePath = Application.persistentDataPath + "/player.xml";
    }

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));

        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }

        Debug.Log("Saved XML to: " + filePath);
    }

    public void Load()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("No XML file found!");
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));

        using (FileStream stream = new FileStream(filePath, FileMode.Open))
        {
            PlayerData data = (PlayerData)serializer.Deserialize(stream);

            Debug.Log("Loaded from XML:");
            Debug.Log(data.playerName);
            Debug.Log(data.level);
            Debug.Log(data.health);
        }
    }
}
