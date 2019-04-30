using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    //variables intialized
    Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        //sets localScale to the health bars local scale
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //decreases players health bar as damage is taken using the playerHealth script
        localScale.x = playerHealth.health/20;
        transform.localScale = localScale;
    }
}
