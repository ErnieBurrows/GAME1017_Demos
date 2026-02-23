using System.Collections.Generic;
using UnityEngine;

public class BulletFlyweightFactory : MonoBehaviour
{
    public BulletFlyweight pistol;
    public BulletFlyweight shotgun;

    private readonly Dictionary<string, BulletFlyweight> cache = new();

    public BulletFlyweight Get(string type)
    {
        if (cache.TryGetValue(type, out var fw))
            return fw;

        fw = type == "Shotgun" ? shotgun : pistol; // default pistol
        cache[type] = fw;
        return fw;
    }
}