using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oliveMovement : MonoBehaviour
{
    private bool oliveLaunched;
    public Rigidbody olive;

    void Update()
    {
        if(!oliveLaunched)
        {
            if(Application.isEditor)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    launchBall();
                }
            }
            else if(Application.isPlaying)
            {
                if(Input.touchCount > 0)
                {
                    launchBall();
                }
            }
        }
    }

    void launchBall()
    {
        float xSpeed = Random.Range(-7.5f, 7.5f);
        olive.velocity = new Vector3(xSpeed, 0f, 15f);
        oliveLaunched = true;
    }
}
