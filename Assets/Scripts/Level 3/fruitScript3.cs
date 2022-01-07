using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitScript3 : MonoBehaviour
{
    public int fruitCellId;
    private bool firstAssign;
    private Vector3 currentPos;
    private Vector3 backupPos;
    private level3Controller levelController;
    public bool validSwitch;
    public bool secondObj;

    public int foodBefore;
    void Start()
    {
        foodBefore = FindObjectOfType<level3Controller>().foodLeft;
        firstAssign = true;
        currentPos = transform.position;
        backupPos = transform.position;
        levelController = FindObjectOfType<level3Controller>();
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
        List<GameObject> areaList = FindObjectOfType<level3Controller>().spawnedAreas;

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
                        currentPos = otherPos;
                        other.gameObject.GetComponent<areaScript>().objectIn = this.gameObject;
                        fruitCellId = areaScr.id;
                        
                    }
                    else
                    {
                        transform.position = currentPos;                   
                    }
                }
                else
                {
                    currentPos = otherPos;
                    other.gameObject.GetComponent<areaScript>().objectIn = this.gameObject;
                    fruitCellId = areaScr.id;
                }
            }
            validSwitch = levelController.checkBoard();
            int foodAfter = FindObjectOfType<level3Controller>().foodLeft;
            if (foodBefore > foodAfter)
            {
                backupPos = currentPos;
                foodBefore = foodAfter;
            }
            //else 
            //{
            //    transform.position = backupPos;
            //    other.gameObject.GetComponent<areaScript>().objectIn.transform.position = other.gameObject.GetComponent<areaScript>().objectIn.GetComponent<fruitScript3>().backupPos;
            //}
            secondObj = false;
        }
    }
}
