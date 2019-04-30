using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFire : MonoBehaviour
{
    //variables intialized
    public GameObject bulletRight, bigBulletRight;
    float fireRate = 0.5f;
    float fireRateMulti = 5.0f;
    float fireRateBig = 30.0f;
    public static float coolBig = 30f;
    public static float coolMulti = 5;

    float nextFire = 0.0f;
    float nextFireMulti = 0.0f;
    float nextFireBig = 0.0f;
    public Transform firePoint;

    public GameObject barBig, barMulti;
    Vector3 localScaleS;
    Vector3 localScaleB;
    Vector3 localScaleM;
    public static bool gameOver = false;
    public float CDMulti = 2.5f;
    public float timeStampBig = 0;
    public float timeStampMulti = 0;
    private bool multiOnCD = false;
    private bool bigOnCD = false;
    private bool cdStartBig = false;
    private bool cdStartMulti = false;
    private float multiRot = 0;

    private AudioSource audio;
    public AudioClip pBlast;
    public AudioClip pBigBlast;
    public AudioClip pMultiBlast;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        //setting the bars and the gameover whenever the scene is loaded
        localScaleB = barBig.transform.localScale;
        localScaleM = barMulti.transform.localScale;
        barBig.transform.localScale += new Vector3(10, 0, 0);
        barMulti.transform.localScale += new Vector3(10, 0, 0);
        gameOver = false;
    }

    void Update()
    {
        //if the player is alive he can use these controls
        if (!gameOver)
        {
            //when clicking the fire button player shoots the small bullet
            if (Input.GetButtonDown("Small Blast") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                audio.PlayOneShot(pBlast, 1f);
                fire();
            }
            //player fire big bullets when e is pressed
            if (Input.GetButtonDown("Big Blast") && Time.time > nextFireBig && !bigOnCD)
            {
                bigOnCD = true;
                cdStartBig = true;
                nextFireBig = Time.time + fireRateBig;
                coolDownBig();
                audio.PlayOneShot(pBigBlast, 1f);
                bigFire();
            }
            //fires multiple small shots when player presses f
            if (Input.GetButtonDown("Multi Blast") && Time.time > nextFireMulti && !multiOnCD)
            {
                multiOnCD = true;
                cdStartMulti = true;
                nextFireMulti = Time.time + fireRateMulti;
                coolDownMulti();
                audio.PlayOneShot(pMultiBlast, 1f);
                multiFire();
            }
        }
    }
    //function to fire the small bullets depending on where the character faces
    void fire()
    {
            Instantiate(bulletRight, firePoint.position, firePoint.rotation);
    }
    //function to fire the big bullets depending on where the character faces
    void bigFire()
    {
            Instantiate(bigBulletRight, firePoint.position, firePoint.rotation);
    }
    //function to fire the multiple bullets depending on where the character faces
    void multiFire()
    {
        if (firePoint.rotation.y <= 0)
        {
            multiRot = 180;
        }
        else
        {
            multiRot = 0;
        }

        Instantiate(bulletRight, new Vector3(firePoint.position.x, firePoint.position.y - 2, firePoint.position.z), Quaternion.Euler(firePoint.rotation.x, multiRot, firePoint.rotation.z + 2));
        Instantiate(bulletRight, firePoint.position, firePoint.rotation);
        Instantiate(bulletRight, new Vector3(firePoint.position.x, firePoint.position.y + 2, firePoint.position.z), Quaternion.Euler(firePoint.rotation.x, multiRot, firePoint.rotation.z - 2));
    }
    //visual cooldown bar 
    IEnumerator CountDownBig()
    {
        yield return new WaitForSeconds(1);
        if (cdStartBig)
        {
            timeStampBig = Time.time + coolBig;
            cdStartBig = false;
        }

        

        if (timeStampBig <= Time.time)
        {
            barBig.transform.localScale += new Vector3(10, 0, 0);
            bigOnCD = false;
            Debug.Log("CD done");

        }
        else
        {
            barBig.transform.localScale -= new Vector3(.33333333333f, 0, 0);
            StartCoroutine(CountDownBig());
        }
    }
    //visual cooldown bar
    IEnumerator CountDownMulti()
    {
        yield return new WaitForSeconds(1);
        if (cdStartMulti)
        {
            timeStampMulti = Time.time + coolMulti;
            cdStartMulti = false;
        }



        if (timeStampMulti <= Time.time)
        {
            barMulti.transform.localScale += new Vector3(10, 0, 0);
            multiOnCD = false;
            Debug.Log("CD done");

        }
        else
        {
            barMulti.transform.localScale -= new Vector3(2, 0, 0);
            StartCoroutine(CountDownMulti());
        }
    }

    void coolDownBig()
    {
        StartCoroutine(CountDownBig());
    }
    void coolDownMulti()
    {
        StartCoroutine(CountDownMulti());
    }

}
