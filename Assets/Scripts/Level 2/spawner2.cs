using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner2 : MonoBehaviour
{
    [Header("Object to Spawn")]
    public GameObject[] objects = new GameObject[6];
    public int spawnAmount2;
    private List<Vector3> objectPos = new List<Vector3>();


    [Header("Spawn Area")]

    /// Point 1 is located at the bottom left of play area to determine the minimum coordinates
    public Transform point1_2;

    /// Point 2 is located at the bottom left of play area to determine the maximum coordinates
    public Transform point2_2;
    void Awake()
    {
        for (int i = 0; i < spawnAmount2; i++)
        {
            spawnObject2();
        }
    }

    void spawnObject2()
    {
        float posX;
        float posZ;
        Vector3 tempPos;

        posX = Random.Range(point1_2.position.x, point2_2.position.x);
        posZ = Random.Range(point1_2.position.z, point2_2.position.z);
        tempPos = new Vector3(posX, 7.625983f, posZ);
        if (objectPos.Count != 0)
        {
            while (!checkDistance(tempPos))
            {
                posX = Random.Range(point1_2.position.x, point2_2.position.x);
                posZ = Random.Range(point1_2.position.z, point2_2.position.z);
                tempPos = new Vector3(posX, 7.625983f, posZ);
            } 
        }
        objectPos.Add(tempPos);
        Instantiate(objects[0], tempPos, Quaternion.identity);
    }

    bool checkDistance(Vector3 temp)
    {
        for(int i = 0; i < objectPos.Count; i++)
        {
            if (Vector3.Distance(temp, objectPos[i]) < .5f)
                return false;
        }
        return true;
    }

    public void combineTwo(int idx, Vector3 pos)
    {
        /// if objects are cheese
        if(idx == objects.Length - 1)
        {
            Debug.Log("Level is completed");
        }

        /// if objects are not cheese
        else
        {
            Instantiate(objects[idx + 1], pos, Quaternion.identity);
        }
    }
}
