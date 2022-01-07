using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level4Controller : MonoBehaviour
{
    public int totalBlocks;
    void Start()
    {
        totalBlocks = 18;
    }

    // Update is called once per frame
    void Update()
    {
        if(totalBlocks == 0)
        {
            print("Level 4 complete");
            Invoke("loadLevel1", 1f);
        }
    }
    void loadLevel1()
    {
        SceneManager.LoadScene(0);
    }
}
