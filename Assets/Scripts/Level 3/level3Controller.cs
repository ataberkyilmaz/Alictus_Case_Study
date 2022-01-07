using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3Controller : MonoBehaviour
{
    private Vector3 touchPos;
    private bool isTouch;

    [SerializeField] private bool pickObj;

    [SerializeField] private GameObject selectedItem;
    [SerializeField] private int selectedId;
    void Start()
    {
        pickObj = false;
    }

    void Update()
    {
        if (Application.isEditor)
            isTouch = getMousePos();
        else if (Application.isPlaying)
            isTouch = getTouchPos();

        if (isTouch && !pickObj)
        {
            grabItem();

            
        }
        if (selectedItem != null)
        {
            Vector3 pos = trackMouse();
            moveItem(pos);
        }


        itemDrop();
    }
    bool getMousePos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;
            return true;
        }
        return false;
    }

    bool getTouchPos()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            touchPos = touch.position;
            return true;
        }
        return false;
    }

    void grabItem()
    {
        Ray castPoint = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;
        LayerMask area = LayerMask.GetMask("area");
        /// Observe if the ray hits our "Ray Wall" object
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, area))
        {
            selectedItem = hit.transform.gameObject.GetComponent<areaScript>().objectIn;
            selectedId = hit.transform.gameObject.GetComponent<areaScript>().id;
            print("I picked an object which is: " + selectedItem.tag);
            pickObj = true;
        }
    }
    Vector3 trackMouse()
    {
        Vector3 pos = new Vector3();
        if (Application.isEditor)
            pos = Input.mousePosition;
        else if (Application.isPlaying)
            pos = Input.GetTouch(0).position;
        return pos;
    }
    void moveItem(Vector3 pos)
    {
        selectedItem.GetComponent<Collider>().enabled = false;
        print("move item is working");
        Ray castPoint = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        LayerMask rayWall = LayerMask.GetMask("Ray Wall");
        /// Observe if the ray hits our "Ray Wall" object
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, rayWall))
        {
            print("trying to move item!");
            selectedItem.transform.position = hit.point;
            
        }
    }

    void itemDrop()
    {
        if (Application.isEditor && Input.GetMouseButtonUp(0))
        {
            pickObj = false;
            selectedItem.GetComponent<Collider>().enabled = true;
            selectedItem = null;
            selectedId = -1;
        }
            
        if (Application.isPlaying && Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                pickObj = false;
                selectedItem.GetComponent<Collider>().enabled = true;
                selectedItem = null;
                selectedId = -1;
            }
        }
    }
}
