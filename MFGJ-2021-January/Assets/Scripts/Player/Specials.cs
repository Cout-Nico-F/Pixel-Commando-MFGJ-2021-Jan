using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specials : MonoBehaviour
{

    private static bool hasTools;
    private Explosives explosives;
    private CoroutineAux coroutineAux;
    private UI_BeltInventory uiBeltInventory;

    public static bool HasTools { get => hasTools; set => hasTools = value; }
    public Explosives Explosives { get => explosives; set => explosives = value; }

    private void Awake()
    {
        explosives = gameObject.GetComponent<Explosives>();
        coroutineAux = FindObjectOfType<CoroutineAux>();
        uiBeltInventory = FindObjectOfType<UI_BeltInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ExplosivesAmmo"))
        {
            explosives.Explosive = collision.GetComponent<IExplode>();
            explosives.HasBombs = true;
            explosives.BombsAmount = explosives.MaxExplosivesAmount;
            uiBeltInventory.EnableUIItem("Bombs");
            uiBeltInventory.HasBombsUI(true);
            uiBeltInventory.TriggerPickupAnimation(collision.gameObject);
            //UI needs to print the Bomb/remote/tnt Sprite based on this collision 
            //we want some animations and sounds so the player notices the pickup too.
            AudioManager.instance.PlaySound("PickUpWeapon");
            AudioManager.instance.PlaySound("PickUpWeapon");
            //hide collision for 3 second.
            coroutineAux.HideObject(3, collision.gameObject);
        }
    }
}
