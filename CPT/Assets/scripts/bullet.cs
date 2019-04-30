using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //variables intialized 
    float bulletSpeed = -15;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //gives the bullet a velocity through variables and destroys them every 4 seconds
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
        Destroy(gameObject, 2f);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
