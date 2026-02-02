using UnityEngine;

public class PlayerStatsSOReader : MonoBehaviour
{
    [Header("Drag your PlayerStats asset here")]
    public PlayerStatsSO stats;

    void Start()
    {
        PrintStats();
    }

    [ContextMenu("Print Stats")]
    public void PrintStats()
    {
        if (stats == null)
        {
            Debug.LogWarning("No PlayerStatsSO assigned!");
            return;
        }

        Debug.Log($"SO Stats -> Name: {stats.playerName}, Level: {stats.level}, Health: {stats.health}");
    }

    [ContextMenu("Damage Player (Health -10)")]
    public void DamagePlayer()
    {
        if (stats == null) return;

        stats.health -= 10f;
        Debug.Log("Damaged player. New health: " + stats.health);
    }

    [ContextMenu("Level Up (+1)")]
    public void LevelUp()
    {
        if (stats == null) return;

        stats.level += 1;
        Debug.Log("Leveled up. New level: " + stats.level);
    }
}