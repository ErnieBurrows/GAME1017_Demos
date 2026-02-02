using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStatsSO;
    public PlayerData playerData;

    private void Awake()
    {
        playerData = new PlayerData
        {
            playerName = playerStatsSO.playerName,
            level = playerStatsSO.level,
            health = playerStatsSO.health
        };
    }

    [ContextMenu("Damage Player (Health -10)")]
    public void DamagePlayer()
    {
        if (playerData == null) return;

        playerData.health -= 10f;
        Debug.Log("Damaged player. New health: " + playerData.health);
    }

    [ContextMenu("Level Up (+1)")]
    public void LevelUp()
    {
        if (playerData == null) return;

        playerData.level += 1;
        Debug.Log("Leveled up. New level: " + playerData.level);
    }
}
