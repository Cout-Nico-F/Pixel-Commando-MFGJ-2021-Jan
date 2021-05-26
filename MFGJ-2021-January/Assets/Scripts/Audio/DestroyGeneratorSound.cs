using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGeneratorSound : MonoBehaviour
{
    [SerializeField] GameObject ElectricFloor1;
    [SerializeField] GameObject ElectricFloor2;

    private void Awake()
    {
        ElectricFloor1 = GameObject.Find("ElectrifiedFloorSoundVertical");
        ElectricFloor2 = GameObject.Find("ElectrifiedFloorSoundHorizontal");
    }

    void Start()
    {
        ElectricFloor1.SetActive(false);
        ElectricFloor2.SetActive(false);

        AudioManager.instance.PlaySound("DestroyHut");
        AudioManager.instance.PlaySound("BombExplossion");
        Debug.Log("Boom");
    }
}
