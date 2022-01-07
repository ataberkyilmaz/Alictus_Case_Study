using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class level3Controller : MonoBehaviour
{
    private Vector3 touchPos;
    private bool isTouch;

    [SerializeField] private bool pickObj;

    [SerializeField] private GameObject selectedItem;
    [SerializeField] private int selectedId;

    [SerializeField] private int pingNum;
    public List<int> matrix;

    public int foodLeft;

    public List<GameObject> spawnedAreas = new List<GameObject>(30);

    //myMatrix theMatrix = new myMatrix();
    void Start()
    {
        //using (StreamReader r = new StreamReader(jsonPath))
        //{
        //    print("using stream reader");
        //    string json = r.ReadToEnd();
        //    //print("json = " + json);
        //    //myMatrix theMatrix = JsonConvert.DeserializeObject<myMatrix>(json);
        //    myMatrix theMatrix = JsonConvert.DeserializeObject<myMatrix>(json);
        //    print("theMatrix" + theMatrix.name);
        //    print("Count is " + theMatrix.matrix.Length);
        //    for (int i = 0; i < theMatrix.matrix.Length; i++)
        //    {
        //        print("matrix element " + i + " is " + matrix[i]);
        //    }
        //}

        pingNum = 0;
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
        Ray castPoint = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        LayerMask rayWall = LayerMask.GetMask("Ray Wall");
        /// Observe if the ray hits our "Ray Wall" object
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, rayWall))
        {
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

    public void addArea(GameObject obj)
    {
        spawnedAreas.Add(obj);
    }

    public bool checkBoard()
    {
        bool deleted = false;
        pingNum += 1;
        for(int idx = 0; idx < 29; idx++)
        {
            if (spawnedAreas[idx].GetComponent<areaScript>().objectIn != null && pingNum % 2 == 0)
            {
                List<GameObject> horizontalDestroy = new List<GameObject>();
                List<GameObject> verticalDestroy = new List<GameObject>();
                GameObject currentObj = spawnedAreas[idx].GetComponent<areaScript>().objectIn;

                //Debug.Log("checking for object " + currentObj.tag + " at index " + idx);
                horizontalDestroy.Add(currentObj);
                verticalDestroy.Add(currentObj);

                int horizontalIdx = idx;
                while ((horizontalIdx - 1) % 5 != 4 && horizontalIdx - 1 >= 0)
                {
                    if (spawnedAreas[horizontalIdx - 1].GetComponent<areaScript>().objectIn != null)
                    {
                        //Debug.Log("Loop 1 for game object " + currentObj.tag);
                        horizontalIdx -= 1;
                        if (spawnedAreas[horizontalIdx].GetComponent<areaScript>().objectIn.tag == currentObj.tag)
                            horizontalDestroy.Add(spawnedAreas[horizontalIdx].GetComponent<areaScript>().objectIn);
                        else
                            horizontalIdx = -1;
                    }
                    else
                    {
                        horizontalIdx = -1;
                    }
                }
                horizontalIdx = idx;
                while ((horizontalIdx + 1) % 5 != 0 && horizontalIdx + 1 <= 29)
                {
                    if (spawnedAreas[horizontalIdx + 1].GetComponent<areaScript>().objectIn != null)
                    {
                        //Debug.Log("Loop 2 for game object " + currentObj.tag);
                        horizontalIdx += 1;
                        if (spawnedAreas[horizontalIdx].GetComponent<areaScript>().objectIn.tag == currentObj.tag)
                            horizontalDestroy.Add(spawnedAreas[horizontalIdx].GetComponent<areaScript>().objectIn);
                        else
                            horizontalIdx = 30;
                    }
                    else
                    {
                        horizontalIdx = 30;
                    }
                }

                int verticalIdx = idx;
                while ((verticalIdx - 5) >= 0)
                {
                    if (spawnedAreas[verticalIdx - 5].GetComponent<areaScript>().objectIn != null)
                    {
                        //Debug.Log("Loop 3 for game object " + currentObj.tag);
                        verticalIdx -= 5;
                        if (spawnedAreas[verticalIdx].GetComponent<areaScript>().objectIn.tag == currentObj.tag)
                            verticalDestroy.Add(spawnedAreas[verticalIdx].GetComponent<areaScript>().objectIn);
                        else
                            verticalIdx = -1;
                    }
                    else
                        verticalIdx = -1;
                }

                verticalIdx = idx;
                while ((verticalIdx + 5) <= 29)
                {
                    if (spawnedAreas[verticalIdx + 5].GetComponent<areaScript>().objectIn != null)
                    {
                        //Debug.Log("Loop 4 for game object " + currentObj.tag);
                        verticalIdx += 5;
                        if (spawnedAreas[verticalIdx].GetComponent<areaScript>().objectIn.tag == currentObj.tag)
                            verticalDestroy.Add(spawnedAreas[verticalIdx].GetComponent<areaScript>().objectIn);
                        else
                            verticalIdx = 30;
                    }
                    else
                        verticalIdx = 30;
                }

                if (verticalDestroy.Count >= 3 || horizontalDestroy.Count >= 3)
                {
                    //print("VerticalDestroy count is " + verticalDestroy.Count);
                    //print("HorizontalDestroy count is " + horizontalDestroy.Count);

                    for (int i = 0; i < verticalDestroy.Count; i++)
                    {
                        //print("For vertical " + verticalDestroy[i].GetComponent<fruitScript3>().fruitCellId + "location, obj - " + verticalDestroy[i].tag);
                    }
                    for (int i = 0; i < horizontalDestroy.Count; i++)
                    {
                        //print("For horizontal " + horizontalDestroy[i].GetComponent<fruitScript3>().fruitCellId + "location, obj - " + horizontalDestroy[i].tag);
                    }
                    if (verticalDestroy.Count >= horizontalDestroy.Count)
                    {
                        for (int i = 0; i < verticalDestroy.Count; i++)
                        {
                            //Debug.Log("Vertical destroy for game object " + currentObj.tag);
                            Destroy(spawnedAreas[verticalDestroy[i].GetComponent<fruitScript3>().fruitCellId].GetComponent<areaScript>().objectIn);
                            spawnedAreas[verticalDestroy[i].GetComponent<fruitScript3>().fruitCellId].GetComponent<areaScript>().objectIn = null;
                            foodLeft -= 1;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < horizontalDestroy.Count; i++)
                        {
                            //Debug.Log("Horizontal destroy for game object " + currentObj.tag);
                            Destroy(spawnedAreas[horizontalDestroy[i].GetComponent<fruitScript3>().fruitCellId].GetComponent<areaScript>().objectIn);
                            spawnedAreas[horizontalDestroy[i].GetComponent<fruitScript3>().fruitCellId].GetComponent<areaScript>().objectIn = null;
                            foodLeft -= 1;
                        }
                    }
                    deleted = true;
                }
            } 
        }
        return deleted;
    }
}
