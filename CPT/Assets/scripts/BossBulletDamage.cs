using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletDamage : MonoBehaviour
{
    public float damage;

    public Boss theEnemy;

    // Start is called before the first frame update
    void Start()
    {
        damage = theEnemy.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //add some logic here for damaging the player
            playerHealth.damagePlayer(damage);
        }

        Debug.Log("Collided with " + collision);
        if (collision.tag != "DetectionBox")
        {
            Destroy(this.gameObject);
        }
    }
}
