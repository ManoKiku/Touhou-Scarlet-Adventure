using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumiaAttack : MonoBehaviour
{
    protected BulletSpawner bulletSpawner;
    protected RadialBulletController radialBulletController;
    protected Vector3 lockPosition;


    [Header("First attack")]
    public Vector3 offset;
    public GameObject spikeBullet;
    public GameObject smallBullet;
    public GameObject mediumBullet;
    public GameObject bigBullet;

    private void Awake() {
        bulletSpawner = GetComponent<BulletSpawner>();
        radialBulletController = GetComponent<RadialBulletController>();
    }

    public void LockPlayer()
    {
        lockPosition = PlayerControl.instance.transform.position + offset;
    }

    public virtual void LockedPositionFire(float bulletSpeed = 5f)
    {
        bulletSpawner.bulletSpeed = bulletSpeed;
        Vector2 angle =  (lockPosition - transform.position).normalized;
        bulletSpawner.Shoot(Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg);
    }

    public virtual void RadialSmallAttack(int amount)
    {
        radialBulletController.ProjectilePrefab = smallBullet;
        radialBulletController.projectileSpeed = 5;
        radialBulletController.SpawnProjectile(amount);
    }

    public virtual void RadialSpikeAttack(int amount)
    {
        radialBulletController.ProjectilePrefab = spikeBullet;
        radialBulletController.projectileSpeed = 5;
        radialBulletController.SpawnProjectile(amount);
    }

    public virtual void RadialBigAttack(int amount)
    {
        radialBulletController.ProjectilePrefab = bigBullet;
        radialBulletController.projectileSpeed = 5;
        radialBulletController.SpawnProjectile(amount);
    }
}
