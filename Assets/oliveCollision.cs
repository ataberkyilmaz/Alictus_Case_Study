using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class oliveCollision : MonoBehaviour
{
    public GameObject olive;
    public GameObject hotdog;
    public void speedUp()
    {
        olive.GetComponent<Rigidbody>().AddForce(0f, 0f, -15f);
    }

    public void increasePadLength()
    {
        hotdog.transform.localScale += new Vector3(1f, 0f, 0f);
    }
    public void reverseControls()
    {
        FindObjectOfType<hotdogController>().switchIsReverse();
    }

    public void wobblingBall()
    {
        print("I'm wobbling");
    }

    public void biggerBall()
    {
        olive.transform.localScale += new Vector3(1f, 1f, 1f);
    }

    void destroyObject(GameObject obj)
    {
        Destroy(obj);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dead zone")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            if (collision.gameObject.tag == "cherry")
            {
                speedUp();
                destroyObject(collision.gameObject);
                FindObjectOfType<level4Controller>().totalBlocks -= 1;
            }
                
            else if (collision.gameObject.tag == "banana")
            {
                increasePadLength();
                destroyObject(collision.gameObject);
                FindObjectOfType<level4Controller>().totalBlocks -= 1;
            }
            
            else if (collision.gameObject.tag == "hamburger")
            {
                reverseControls();
                destroyObject(collision.gameObject);
                FindObjectOfType<level4Controller>().totalBlocks -= 1;
            }
            
            else if (collision.gameObject.tag == "cheese")
            {
                wobblingBall();
                destroyObject(collision.gameObject);
                FindObjectOfType<level4Controller>().totalBlocks -= 1;
            }
            
            else if (collision.gameObject.tag == "watermelon")
            {
                biggerBall();
                destroyObject(collision.gameObject);
                FindObjectOfType<level4Controller>().totalBlocks -= 1;
            }
        }
    }
}
