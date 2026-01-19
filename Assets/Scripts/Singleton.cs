using UnityEngine;

public class Singleton : MonoBehaviour
{
    // Static reference to the single instance
    public static Singleton Instance { get; private set; }

    public int score = 0;

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance
        Instance = this;

        // Keep this object when loading new scenes
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score is now: " + score);
    }
}
