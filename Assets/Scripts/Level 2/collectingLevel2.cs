using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collectingLevel2 : MonoBehaviour
{
    private level2Controller level2Controller;
    private spawner2 spawner2;
    private int index;
    [SerializeField] private bool collected;
    void Start()
    {
        level2Controller = FindObjectOfType<level2Controller>();
        spawner2 = FindObjectOfType<spawner2>();

        /// find which object we currently have
        for (int i = 0; i < level2Controller.stages.Length; i++)
        {
            if (this.tag == level2Controller.stages[i])
                index = i;
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        string tag = this.tag;
        if(collision.collider.tag == this.tag && !collected)
        {
            /// to prevent collisions that are not wanted
            collected = true;

            /// evolve objects to the next stage
            level2Controller.totalCollected += 1;

            /// to prevent from two new objects spawning
            if (level2Controller.totalCollected % 2 == 0)
            {
                spawner2.combineTwo(index, transform.position);
                
            }

            /// destroy current objects
            Destroy(this.gameObject);
        }
    }
}
