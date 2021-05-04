using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedElectricGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        var trapsAnimations = GameObject.FindGameObjectsWithTag("TrapAnimation");
        for (var i=0; i<trapsAnimations.Length; i++){
            trapsAnimations[i].GetComponent<Animator>().SetBool("trap_active", false);
        }

        var traps = GameObject.FindGameObjectsWithTag("Trap");
        for (var i=0; i<traps.Length; i++){
            traps[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
