using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Settings")]
    public float bulletSpeed = 5f;
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

        float angle = Mathf.Atan2(buff.velocity.y, buff.velocity.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);    

        Destroy(bullet, bulletLife);       
    }

    public void Shoot(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        var buff = bullet.GetComponent<Rigidbody2D>();

        float angleInRadians = angle * Mathf.Deg2Rad;

        buff.velocity = new Vector2(
            Mathf.Cos(angleInRadians),
            Mathf.Sin(angleInRadians) 
        ).normalized * bulletSpeed;

        bullet.transform.rotation = Quaternion.Euler(0, 0, 90 + angle);    

        Destroy(bullet, bulletLife); 
    }
}