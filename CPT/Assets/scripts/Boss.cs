using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health = 100;
    public float damageRange = 0;
    public float damage = 0;
    public float fireRate = 0;
    private float timeToFire = 0;

    private BossMovement theEnemy;

    public GameObject bullet;
    private BossBulletDamage bulletDMG;
    private AudioSource audio;
    public AudioClip shoot;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        theEnemy = GetComponent<BossMovement>();
        audio = GetComponent<AudioSource>();
        bulletDMG = bullet.GetComponent<BossBulletDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theEnemy.playerDetected)
        {
            if (fireRate == 0)
            {
                Shoot();
            }
            else
            {
                if (Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / fireRate;
                    Shoot();
                }
            }
        }

        theEnemy.health = health;
    }

    public void TakeDamage(int dmgAmo)
    {
        health -= dmgAmo;
    }

    private void Shoot()
    {
        bulletDMG.theEnemy = this;

        audio.PlayOneShot(shoot, 1f);
        Instantiate(bullet, firePoint.position, firePoint.rotation);

        damage = Random.Range(damageRange, damageRange + 10);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "smallBullet")
        {
            TakeDamage(30);
        }
        if (col.gameObject.tag == "bigBullet")
        {
            TakeDamage(75);
        }
    }
}
