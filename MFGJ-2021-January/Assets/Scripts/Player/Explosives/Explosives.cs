using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosives : MonoBehaviour
{
    public UI_BeltInventory uiBeltInventory;

    private int bombsAmount;
    private bool hasBombs;

    private bool bombIsPlanted;

    private IExplode explosive;

    [SerializeField]
    private int maxExplosivesAmount;

    public bool HasBombs { get => hasBombs; set => hasBombs = value; }
    public bool BombIsPlanted { get => bombIsPlanted; set => bombIsPlanted = value; }
    public IExplode Explosive { get => explosive; set => explosive = value; }
    public int BombsAmount { get => bombsAmount; set => bombsAmount = value; }
    public int MaxExplosivesAmount { get => maxExplosivesAmount; }

    private void Start()
    {
        uiBeltInventory = FindObjectOfType<UI_BeltInventory>(); //moved here so we dont need to find more than once.
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {


            //if (bombIsPlanted)
            //{
            //    explosive.Detonate();          //this was intended for the remote bomb
            //    bombIsPlanted = false;
            //}
            //else
            //{
            if (hasBombs)
            {
                explosive.Plant();
                //bombIsPlanted = true;
                bombsAmount--;
                if (bombsAmount <= 0)
                {
                    hasBombs = false;
                    uiBeltInventory.HasBombsUI(false);
                }
            }
            else //has no ammo
            {
                //Shake or Animate the Bomb UI square

            }
            //}
        }
    }
}
