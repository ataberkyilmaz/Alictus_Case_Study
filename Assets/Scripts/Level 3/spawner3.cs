using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner3 : MonoBehaviour
{
    [Header("Spawn Object Info")]
    public GameObject[] objects = new GameObject[3];
    public float objectHeight;

    [Header("Spawn Area Info")]
    public GameObject area;
    public List<GameObject> spawnedAreas = new List<GameObject>(30);

    [Header("Parent of Spawned Objects")]
    [Space(15)]
    public Transform parentArea;
    public Transform parentObj;

    
    [Header("Spawn Location Adjustment")]
    [Space(15)]
    public Transform area1;
    public Transform area2;
    public Transform area3;

    

    private float xDistance, zDistance, firstX;
    private Vector3 spawnPos;
    void Awake()
    {
        firstX = area1.position.x;
        spawnPos = new Vector3(area1.position.x, objectHeight, area1.position.z);
        xDistance = area2.position.x - area1.position.x;
        zDistance = area1.position.z - area3.position.z;

        spawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnArea()
    {
    }

    void spawnObjects()
    {
        for(int i = 0; i < 30; i++)
        {
            // first Areas are spawned
            GameObject areaObj = Instantiate(area, spawnPos, Quaternion.identity);
            spawnedAreas.Add(areaObj);
            areaObj.GetComponent<areaScript>().id = i;
            areaObj.transform.parent = parentArea;

            // for now objects will be spawned randomly to test
            int idx = Random.Range(0, 3);
            GameObject obj = Instantiate(objects[idx], spawnPos, Quaternion.identity);
            // random spawn is completed

            // object is placed as a child of parent object
            obj.transform.parent = parentObj;

            // next position will be initialized
            if(i % 5 == 4)
            {
                //Debug.Log("i is " + i + " increasing the z!");
                spawnPos = new Vector3(firstX, objectHeight, spawnPos.z - zDistance);
            }
            else
            {
                spawnPos += new Vector3(xDistance, 0, 0);
            }
        }
    }
}
