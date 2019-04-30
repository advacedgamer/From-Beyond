using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    public float speed = 2f;

    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D rbd;

    public GameObject target;

    private Transform gfx;
    private Animator anim;

    public Transform firePoint;

    public bool playerDetected = false;
    private bool isPatrol = true;
    private bool atA = true;
    private bool atB = false;
    private bool isFacingRight = false;
    public bool shootForever = false;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gfx = transform.Find("Graphics");
    }

    // Update is called once per frame
    void Update()
    {
        if (shootForever)
        {
            playerDetected = true;
        }
        else
        {
            if (isPatrol)
            {
                if (atA)
                {
                    target = pointB;
                    MoveTo(pointB);

                    if (transform.position.x >= pointB.transform.position.x)
                    {
                        StartCoroutine(WaitToMove());
                        atA = false;
                        atB = true;
                    }
                }
                else if (atB)
                {
                    target = pointA;
                    MoveTo(pointA);

                    if (transform.position.x <= pointA.transform.position.x)
                    {
                        StartCoroutine(WaitToMove());
                        atB = false;
                        atA = true;
                    }
                }
                else
                {
                    MoveTo(pointA);
                }

                CanFlip();
            }

            if (playerDetected)
            {
                isPatrol = false;
            }
        }
    }

    private void MoveTo(GameObject point)
    {
        rbd.MovePosition(new Vector2((transform.position.x + point.transform.position.x * speed * Time.deltaTime), (transform.position.y + point.transform.position.y * speed * Time.deltaTime))); //(transform.position, point.transform.position, speed * Time.deltaTime);
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

        if(firePoint.transform.rotation.y == 0)
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rbd.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rbd.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rbd.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerDetected = true;

            target = collision.gameObject;

            CanFlip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerDetected = false;
            isPatrol = true;

            CanFlip();
        }
    }

    IEnumerator WaitToMove()
    {
        isPatrol = false;
        yield return new WaitForSeconds(3);
        isPatrol = true;

    }
}