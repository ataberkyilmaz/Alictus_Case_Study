using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oliveCollectingScript : MonoBehaviour
{
    public float pullForce;
    public Transform collectingArea;
    public Material collectedMaterial;


    private Rigidbody rb;
    private level1Controller level1Controller;
    private Renderer objRenderer;
    private bool collected;
    private void Start()
    {
        collected = false;
        rb = GetComponent<Rigidbody>();
        level1Controller = FindObjectOfType<level1Controller>();
        objRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        /// if the olive is marked as collected, it is pulled towards the collecting zone
        if(collected)
        {
            Vector3 areaPos = collectingArea.position;

            Vector3 forcetoAdd = areaPos - this.transform.position;

            rb.velocity = forcetoAdd * pullForce;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collection Area" && !collected)
        {
            collected = true;

            /// number of olives collected is incremented to check if all 64 is collected
            level1Controller.incrementOlive(); 

            /// Material of collected olives are changed
            objRenderer.material = collectedMaterial; 
        }
    }
}
