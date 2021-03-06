﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour , IExplode
{
    [SerializeField]
    private GameObject deployedBomb;
    private Gunning gunning;
    private void Start()
    {
        gunning = FindObjectOfType<Gunning>();
    }
    public void Detonate()
    {
        //this Bomb cant be detonated. Its timer based.
    }

    public void Plant()
    {
        Vector3 offset = new Vector3(0, -0.25f, 0);
        gunning = FindObjectOfType<Gunning>();//Refresh the gunning reference because it changes on gun swapping.
        //Instantiate a bomb prefab in front of the player
        Instantiate(deployedBomb, gunning.transform.position + offset, Quaternion.identity);
        //bomb prefab will contain animation and sound
        //bomb prefab has a timer, then explodes.
        //bomb prefab sets bombisplanted to false before exploding
        Debug.Log("The bomb has been planted ! ! ! ");
    }
}
