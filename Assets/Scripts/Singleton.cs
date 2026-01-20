using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    {
        get
        {
            if (instance == null)
            {
                // Check if an instance exists in the scene.
                instance = FindFirstObjectByType<GameManager>();

                // Create a GameObject with Singleton.
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(nameof(GameManager));
                    singletonObject.AddComponent<GameManager>();
                }
            }

            return instance;   
        }
    }  

    // Get the instance => check if its null => if null set the instance
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
        instance = this;

        // Keep this object when loading new scenes
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score is now: " + score);
    }
}
