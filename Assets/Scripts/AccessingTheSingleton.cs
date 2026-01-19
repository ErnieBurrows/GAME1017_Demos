using UnityEngine;

public class AccessingTheSingleton : MonoBehaviour
{
    private void Start()
    {
        // Access the singleton safely
        if (Singleton.Instance != null)
        {
            Singleton.Instance.AddScore(10);
        }
        else
        {
            Debug.LogError("GameManager instance not found!");
        }
    }
}
