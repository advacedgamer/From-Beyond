using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class camera : MonoBehaviour
{
    //variables initialized
    public GameObject player, boss, cooldownBig, cooldownMulti, multiBack, bigBack, smallBlast, bigBlast;
    public Text timer;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //offset variable is set equal to the distance between the camera's position and player's position
        offset = transform.position - player.transform.position;
        timer.enabled = true;
        BossMovement.isDead = false;
    }
    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        //sets the camera position to the players position while having the offset of distance
        
        if (BossMovement.isDead == true)
        {
            transform.position = boss.transform.position + offset;
            Destroy(cooldownBig);
            Destroy(cooldownMulti);
            Destroy(multiBack);
            Destroy(bigBack);
            Destroy(smallBlast);
            Destroy(bigBlast);
            timer.enabled = false;
        }
        else
        {
            transform.position = player.transform.position + offset;
            timer.enabled = true;
        }
    }
}
