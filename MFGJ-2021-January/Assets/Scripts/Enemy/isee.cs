using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isee : MonoBehaviour
{
    public VariableController ooga1;

    void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("VariableController");

        ooga1 = g.GetComponent<VariableController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ooga1.ooga = true;
        }

        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ooga1.ooga = false;
        }

        
    }
}
