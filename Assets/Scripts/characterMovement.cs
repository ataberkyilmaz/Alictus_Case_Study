using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    private Vector3 playerPos;
    private Vector3 touchPos; // Same as mouse position

    private bool isTouch;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
            isTouch = getMousePos();
        else if (Application.isPlaying)
            isTouch = getTouchPos();

        if(isTouch)
        {
            moveCharacter();
        }
       
    }

    bool getMousePos()
    {
        if(Input.GetMouseButton(0))
        {
            touchPos = Input.mousePosition;
            return true;
        }
        return false;
    }

    bool getTouchPos()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = touch.position;
            return true;
        }
        return false;
    }

    void moveCharacter()
    {
        /// Shoot a ray from the touched position
       
        Ray castPoint = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;
        LayerMask rayWall = LayerMask.GetMask("Ray Wall");
        /// Observe if the ray hits our "Ray Wall" object
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, rayWall)) {
            Vector3 destination = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);

            Vector3 velocityToAdd = destination - rb.position;

            rb.velocity = velocityToAdd * speed;
        }
    }
}
