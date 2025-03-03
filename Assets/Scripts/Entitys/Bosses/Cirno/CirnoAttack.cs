using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirnoAttack : RumiaAttack
{
    public Sprite[] randomSprites;

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


    public void RandomColorAttack()
    {
        mediumBullet.GetComponent<SpriteRenderer>().sprite = randomSprites[Random.Range(0, randomSprites.Length)];
        bulletSpawner.bulletPrefab = mediumBullet;
        bulletSpawner.bulletSpeed = Random.Range(2, 7);
        bulletSpawner.Shoot(Random.Range(0, 360));
    }

    public void LockBullets()
    {

    }
}
