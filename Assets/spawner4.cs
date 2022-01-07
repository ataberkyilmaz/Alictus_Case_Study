using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner4 : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;

    public List<GameObject> blocks = new List<GameObject>(5);

    private void Start()
    {
        spawnBlocks();
    }

    void spawnBlocks()
    {
        float xDifference = (pos2.position.x - pos1.position.x) / 5;
        float zDifference = (pos2.position.z - pos1.position.z) / 2;

        for(int i = 0; i < 3; i++)
        {
            for(int k = 0; k < 6; k++)
            {
                int choice = Random.Range(0, 5);
                Instantiate(blocks[choice], new Vector3(pos1.position.x + xDifference * k, pos1.position.y, pos1.position.z + zDifference * i), Quaternion.identity);
            }
        }
    }

}
