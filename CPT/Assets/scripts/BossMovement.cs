using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 2f;
    public float health = 500;
    public static bool isDead = false;
    public Transform TeleA;
    public Transform TeleB;
    public Transform SweepA;
    public Transform SweepB;
    public Transform resetPos;

    private Rigidbody2D rbd;

    public GameObject target;

    private Transform gfx;
    private Animator anim;

    public GameController endGame;

    public Transform firePoint;

    private AudioSource audio;
    public AudioClip death;
    public AudioClip tele;

    private bool isFacingRight = false;

    public bool playerDetected = true;
    private bool telea = false;
    private bool teleb = false;

    private bool sweepOne = false;
    private bool sweepTwo = false;
    private bool sweepAP = false;
    private bool sweepBP = false;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        gfx = transform.Find("Graphics");
        anim = gfx.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        isDead = false;
        rbd.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        firePoint.transform.LookAt(target.transform);

        if(health <= 400 && !telea)
        {
            transform.position = TeleA.position;
            telea = true;
            audio.PlayOneShot(tele, .8f);
            StartCoroutine(Reset());
        }
        
        else if(health <= 300 && !sweepOne && telea)
        {
            transform.position = SweepA.position;
            audio.PlayOneShot(tele, .8f);
            sweepOne = true;
        }

        else if (health <= 200 && !teleb && sweepOne)
        {
            transform.position = TeleB.position;
            teleb = true;
            audio.PlayOneShot(tele, .8f);
            StartCoroutine(Reset());
        }

        else if(health <= 100 && !sweepTwo && teleb)
        {
            transform.position = SweepB.position;
            audio.PlayOneShot(tele, .8f);
            sweepTwo = true;
        }

        else if (health <= 0 && !isDead)
        {
            GameController.myLoop.enabled = false;
            audio.PlayOneShot(death, .8f);
            transform.position = resetPos.position;
            playerDetected = false;
            anim.SetBool("isDead", true);
            StartCoroutine(DelBoss());
            isDead = true;
        }

        CanFlip();
    }

    private void Sweep(Transform point)
    {
        rbd.MovePosition(new Vector2((transform.position.x + point.transform.position.x * speed * Time.deltaTime), transform.position.y + point.transform.position.y * (speed * Time.deltaTime)));
        Debug.Log("SweepA");
    }

    private void CanFlip()
    {
        if (target.transform.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (target.transform.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        if (firePoint.transform.rotation.y == 0)
        {
            firePoint.transform.Rotate(0, 180, 0, Space.Self);
            Debug.Log("180");
        }
        else
        {
            firePoint.transform.Rotate(0, -180, 0, Space.Self);
            Debug.Log("0");
        }


        Vector3 theScale = gfx.localScale;
        theScale.x *= -1;
        gfx.localScale = theScale;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(10f);
        audio.PlayOneShot(tele, .8f);
        transform.position = resetPos.position;
    }

    IEnumerator DelBoss()
    {
        yield return new WaitForSeconds(5f);

        anim.enabled = false;
        gfx.gameObject.SetActive(false);
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("End");
        endGame.WinGame();
    }
}