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
    public level3Controller levelController;

    public List<int> matrix = new List<int>(30); 
    void Awake()
    {

        // JSON dosyasını okuyamadım o yüzden array ile yolluyorum
        matrix = new List<int> {1, 0, 1, 2, 1,
                                0, 1, 0, 1, 2,
                                2, 2, 1, 2, 1,
                                1, 1, 2, 1, 0,
                                0, 2, 0, 0, 1,
                                2, 0, 2, 0, 1 };

        firstX = area1.position.x;
        spawnPos = new Vector3(area1.position.x, objectHeight, area1.position.z);
        levelController = FindObjectOfType<level3Controller>();
        xDistance = area2.position.x - area1.position.x;
        zDistance = area1.position.z - area3.position.z;

        spawnObjects();
    }

    void spawnObjects()
    {
        for (int i = 0; i < 30; i++)
        {
            // first Areas are spawned
            GameObject areaObj = Instantiate(area, spawnPos, Quaternion.identity);
            levelController.addArea(areaObj);
            areaObj.GetComponent<areaScript>().id = i;
            areaObj.transform.parent = parentArea;

            int idx = matrix[i];

            GameObject obj = Instantiate(objects[idx], spawnPos, Quaternion.identity);


            // object is placed as a child of parent object
            obj.transform.parent = parentObj;


            // next position will be initialized
            if(i % 5 == 4)
            {
                spawnPos = new Vector3(firstX, objectHeight, spawnPos.z - zDistance);
            }
            else
            {
                spawnPos += new Vector3(xDistance, 0, 0);
            }
        }
    }
}
