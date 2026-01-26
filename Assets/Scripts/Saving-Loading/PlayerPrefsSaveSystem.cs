using UnityEngine;

public class PlayerPrefsSaveSystem : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    public void Save()
    {
        PlayerPrefs.SetString("PlayerName", data.playerName);
        PlayerPrefs.SetInt("Level", data.level);
        PlayerPrefs.SetFloat("Health", data.health);

        PlayerPrefs.Save();   // Forces it to write to disk

        Debug.Log("Saved using PlayerPrefs");
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            Debug.Log("No PlayerPrefs data found!");
            return;
        }

        string name = PlayerPrefs.GetString("PlayerName");
        int level = PlayerPrefs.GetInt("Level");
        float health = PlayerPrefs.GetFloat("Health");

        Debug.Log("Loaded from PlayerPrefs:");
        Debug.Log(name);
        Debug.Log(level);
        Debug.Log(health);
    }
    
    [ContextMenu("Clear")]
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs cleared");
    }
}
