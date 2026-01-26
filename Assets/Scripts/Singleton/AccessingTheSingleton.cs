using UnityEngine;

public class AccessingTheSingleton : MonoBehaviour
{
    [ContextMenu("AddScore")]
    private void AddScore()
    {
        GameManager.Instance.AddScore(10);
    }
}
