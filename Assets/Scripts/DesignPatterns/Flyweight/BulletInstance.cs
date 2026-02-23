using UnityEngine;

public class BulletInstance : MonoBehaviour
{
    private BulletFlyweight flyweight;
    private Vector3 direction;

    private MeshFilter mf;
    private MeshRenderer mr;

    private void Awake()
    {
        mf = gameObject.AddComponent<MeshFilter>();
        mr = gameObject.AddComponent<MeshRenderer>();
    }

    public void Init(BulletFlyweight fw, Vector3 dir)
    {
        flyweight = fw;
        direction = dir.normalized;

        mf.sharedMesh = flyweight.mesh;
        mr.sharedMaterial = flyweight.material;
    }

    private void Update()
    {
        transform.position += direction * flyweight.speed * Time.deltaTime;
    }
}