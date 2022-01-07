using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class level1Controller : MonoBehaviour
{
    [SerializeField] private int collectedOlive;
    private bool levelEnd;
    void Start()
    {
        levelEnd = false;
        collectedOlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkEnd();
    }

    public void incrementOlive()
    {
        collectedOlive += 1;
    }

    void checkEnd()
    {
        if(!levelEnd && collectedOlive == 64)
        {
            levelEnd = true;
            Debug.Log("Level has ended!");
            Invoke("loadLevel2", 1f);
        }
    }

    void loadLevel2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
