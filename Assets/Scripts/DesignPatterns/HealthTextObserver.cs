using UnityEngine;
using UnityEngine.UI;

public class HealthTextObserver : MonoBehaviour
{
    public HealthSubject health;
    public Text uiText;

    private void Awake()
    {
        if (!health) health = FindFirstObjectByType<HealthSubject>();
    }

    private void OnEnable()
    {
        if (health != null)
            health.OnHealthChanged += UpdateText;
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnHealthChanged -= UpdateText;
    }

    private void UpdateText(int current, int max)
    {
        if (uiText) uiText.text = $"HP: {current}/{max}";
        Debug.Log($"HP changed: {current}/{max}");
    }
}