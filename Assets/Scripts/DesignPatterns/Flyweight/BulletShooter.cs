using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public BulletFlyweightFactory factory;

    private void Awake()
    {
        if (!factory) factory = FindFirstObjectByType<BulletFlyweightFactory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Shoot("Pistol");
        if (Input.GetKeyDown(KeyCode.Alpha2)) Shoot("Shotgun");
    }

    private void Shoot(string type)
    {
        BulletFlyweight fw = factory.Get(type);

        GameObject go = new GameObject($"Bullet_{type}");
        go.transform.position = transform.position;

        var bullet = go.AddComponent<BulletInstance>();
        bullet.Init(fw, transform.forward);
    }
}