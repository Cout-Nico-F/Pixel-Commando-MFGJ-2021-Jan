using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specials : MonoBehaviour
{

    private static bool hasTools;
    private Explosives explosives;
    private CoroutineAux coroutineAux;

    public static bool HasTools { get => hasTools; set => hasTools = value; }
    public Explosives Explosives { get => explosives; set => explosives = value; }

    private void Awake()
    {
        explosives = new Explosives();
        coroutineAux = FindObjectOfType<CoroutineAux>();
    }

    private void Update()
    {
        explosives.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ExplosivesAmmo"))
        {
            explosives.Explosive = collision.GetComponent<IExplode>();
            explosives.HasBombs = true;
            //UI needs to print the Bomb/remote/tnt Sprite based on this collision 
            //we want some animations and sounds so the player notices the pickup too.
            AudioManager.instance.PlaySound("PickUpWeapon");
            AudioManager.instance.PlaySound("PickUpWeapon");
            //hide collision for 3 second.
            coroutineAux.HideObject(3, collision.gameObject);
        }
    }
}
