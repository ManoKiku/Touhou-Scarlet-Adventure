using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CirnoAttack : RumiaAttack
{
    [SerializeField]
    protected GameObject autoAimBullet;
    [SerializeField]
    protected Sprite[] randomSprites;
    [SerializeField]
    protected Sprite lockSprite;


    public void LockedPositionFireAngle(float angle = 0f)
    {
        bulletSpawner.bulletSpeed = 5;
        bulletSpawner.bulletPrefab = spikeBullet;
        Vector2 vectorAngle = (lockPosition - transform.position).normalized;
        bulletSpawner.Shoot(Mathf.Atan2(vectorAngle.y, vectorAngle.x) * Mathf.Rad2Deg + angle);
    }

    public override void RadialSmallAttack(int amount)
    {
        radialBulletController.ProjectilePrefab = smallBullet;
        radialBulletController.projectileSpeed = 2;
        radialBulletController.SpawnProjectile(amount);
    }

    public void RadialAutoAimAttack(int amount)
    {
        radialBulletController.ProjectilePrefab = autoAimBullet;
        radialBulletController.projectileSpeed = 4;
        radialBulletController.SpawnProjectile(amount);
    }

    public void RandomColorAttack()
    {
        mediumBullet.GetComponent<SpriteRenderer>().sprite = randomSprites[Random.Range(0, randomSprites.Length)];
        bulletSpawner.bulletPrefab = mediumBullet;
        bulletSpawner.bulletSpeed = Random.Range(2, 7);
        bulletSpawner.Shoot(Random.Range(0, 360));
    }

    public void LockBullets()
    {
        GameObject[] bulletsToLock = GameObject.FindGameObjectsWithTag("BulletLock");

        foreach(var i in bulletsToLock)
        {
            i.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            i.GetComponent<SpriteRenderer>().sprite = lockSprite;
        }
    }

    public void UnlockBullets()
    {
        GameObject[] bulletsToLock = GameObject.FindGameObjectsWithTag("BulletLock");

        foreach(var i in bulletsToLock)
        {
            i.GetComponent<Rigidbody2D>().gravityScale = Random.Range(20, 50) / 100.0f;
            i.GetComponent<SpriteRenderer>().sprite =  randomSprites[Random.Range(0, randomSprites.Length)];
        }
    }
}
