using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [Header("Object to Spawn")]
    public GameObject Object;
    public int spawnAmount;

    [Header("Spawn Area")]
    public Transform point1;
    public Transform point2;
    void Awake()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            spawnObject();
        }
    }

    void spawnObject()
    {
        float spawnX = Random.Range(point1.position.x, point2.position.x);
        float spawnZ = Random.Range(point1.position.z, point2.position.z);

        Instantiate(Object, new Vector3(spawnX, 0.6f, spawnZ), Quaternion.identity);
    }
}
