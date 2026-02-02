using UnityEngine;

[CreateAssetMenu(menuName = "Teaching Examples/Player Stats", fileName = "PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public string playerName = "Ernie";
    public int level = 1;
    public float health = 100f;
}