using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitScript3 : MonoBehaviour
{
    [SerializeField] private int fruitCellId;
    private bool firstAssign;
    private Vector3 currentPos;

    public bool secondObj;
    void Start()
    {
        firstAssign = true;
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool checkIfCellNear(int id)
    {
        List<int> availableCells = new List<int>();
        if(fruitCellId == 0)
        {
            availableCells.Add(1);
            availableCells.Add(5);
        }

        else if(fruitCellId == 4)
        {
            availableCells.Add(3);
            availableCells.Add(9);
        }

        else if (fruitCellId == 25)
        {
            availableCells.Add(20);
            availableCells.Add(26);
        }

        else if (fruitCellId == 29)
        {
            availableCells.Add(24);
            availableCells.Add(28);
        }

        else if(fruitCellId % 5 == 0)
        {
            availableCells.Add(fruitCellId - 5);
            availableCells.Add(fruitCellId + 5);
            availableCells.Add(fruitCellId + 1);
        }

        else if (fruitCellId % 5 == 4)
        {
            availableCells.Add(fruitCellId - 5);
            availableCells.Add(fruitCellId + 5);
            availableCells.Add(fruitCellId - 1);
        }

        else
        {
            availableCells.Add(fruitCellId - 5);
            availableCells.Add(fruitCellId + 5);
            availableCells.Add(fruitCellId - 1);
            availableCells.Add(fruitCellId + 1);
        }

        for(int k = 0; k < availableCells.Count; k++)
        {
            if (availableCells[k] == id)
                return true;
        }
        return false;
    }

    Vector3 searchAreaPos(int id)
    {
        List<GameObject> areaList = FindObjectOfType<spawner3>().spawnedAreas;

        Vector3 thePos = new Vector3();

        for(int i = 0; i < areaList.Count; i++)
        {
            if(i == id)
            {
                thePos = areaList[i].transform.position;
            }
        }

        return thePos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "area")
        {
            areaScript areaScr = other.gameObject.GetComponent<areaScript>();
            if (firstAssign)
            {
                other.gameObject.GetComponent<areaScript>().objectIn = this.gameObject;
                fruitCellId = other.gameObject.GetComponent<areaScript>().id;
                firstAssign = false;
            }
            else
            {
                int tempId = other.gameObject.GetComponent<areaScript>().id;
                Vector3 otherPos = searchAreaPos(tempId);
                if (!secondObj)
                {
                    
                    if (checkIfCellNear(tempId))
                    {
                        

                        // position switch
                        other.gameObject.GetComponent<areaScript>().objectIn.transform.position = currentPos;
                        other.gameObject.GetComponent<areaScript>().objectIn.GetComponent<fruitScript3>().secondObj = true;
                        transform.position = otherPos;

                        
                    }
                    else
                    {
                        transform.position = currentPos;
                    }
                }
                currentPos = otherPos;
                other.gameObject.GetComponent<areaScript>().objectIn = this.gameObject;
                fruitCellId = areaScr.id;
            }
            secondObj = false;
        }
    }
}
