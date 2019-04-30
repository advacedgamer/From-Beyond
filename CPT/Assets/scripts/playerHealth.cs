using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    //variables initialized
    public static GameObject player;
    public static float health = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        //sets the variables to default when the scene loads
        if (sceneName == "SampleScene")
        {
            health = 100;
        }
        player = GameObject.Find("mc");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if the player collides with a warp tile he goes to boss level
        if (col.gameObject.tag == "warpTile")
        {
            //damagePlayer(40);
            SceneManager.LoadScene("BossRoom");
        }
        //if the player collides with a deadly object it takes damage
        if (col.gameObject.tag == "deadlyObsticle")
        {
            damagePlayer(40);
        }
        
        
    }
    //if player collides into the health item he gains health
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "healthItem")
        {
            if (health > 70)
            {
                health = 100;
                Debug.Log(health);
            }
            else
            {
                health += 30;
                Debug.Log(health);
            }
        }
    }
    //calculates the damage a player takes and subtracts the players health accordingly until the player dies
    public static void damagePlayer(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
