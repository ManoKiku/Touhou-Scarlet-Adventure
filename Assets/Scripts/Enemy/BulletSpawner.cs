using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Settings")]
    public float bulletSpeed = 5f;
    public float rotationSpeed = 200f;
    public float bulletLife = 5f;
    private Transform target;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public Vector3 offset;

    private void Start() {
        target = PlayerControl.instance.transform;
    }

    public void ShootAtTarget()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        var buff = bullet.GetComponent<Rigidbody2D>();
        buff.velocity = (target.transform.position + offset - bullet.transform.position).normalized * bulletSpeed;    
        Destroy(bullet, bulletLife);       
    }
}