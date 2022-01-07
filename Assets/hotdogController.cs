using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hotdogController : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 playerPos;
    private Vector3 touchPos; // Same as mouse position

    private bool isTouch;
    private bool isReverse;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCharacter();
    }

    void moveCharacter()
    {
        if(Input.GetMouseButton(0))
        {
            touchPos = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit;
            LayerMask rayWall = LayerMask.GetMask("Ray Wall");
            /// Observe if the ray hits our "Ray Wall" object
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, rayWall))
            {
                if(!isReverse)
                {
                    transform.position = new Vector3(hit.point.x, transform.position.y, transform.position.z);
                }
                if(isReverse)
                {
                    transform.position = new Vector3(hit.point.x * -1, transform.position.y, transform.position.z);
                }
            }
        }
    }

    public void switchIsReverse()
    {
        isReverse = !isReverse;
    }
}