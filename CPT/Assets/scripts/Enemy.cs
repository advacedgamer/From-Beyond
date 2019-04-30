using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float damageRange = 0;
    public float damage = 0;
    public float fireRate = 0;
    private float timeToFire = 0;
    private AudioSource audio;
    public AudioClip shoot;
    public AudioClip death;


    private WalkingEnemy theEnemy;

    public GameObject bullet;
    private BulletDamage bulletDMG;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        theEnemy = GetComponent<WalkingEnemy>();
        bulletDMG = bullet.GetComponent<BulletDamage>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(theEnemy.playerDetected)
        {
            if (fireRate == 0)
            {
                Shoot();
            }
            else
            {
                if(Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / fireRate;
                    Shoot();
                }
            }
        }
    }

    public void TakeDamage(int dmgAmo)
    {
        health -= dmgAmo;

        if(health <= 0)
        {
            audio.PlayOneShot(death, 1.5f);
            StartCoroutine(Destroy());
        }
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

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }
}