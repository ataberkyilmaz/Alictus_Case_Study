using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level2Controller : MonoBehaviour
{
    public string[] stages = { "olive", "cherry", "banana", "hotdog", "hamburger", "cheese", "watermelon" };
    public int totalCollected = 0;

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("cheese") == null)
        {
            Invoke("loadLevel3", 1f);
        }
    }

    void loadLevel3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
