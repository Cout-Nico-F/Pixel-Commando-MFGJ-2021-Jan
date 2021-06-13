using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform RespawnPosition;
    [SerializeField] private GameObject flag;
    [SerializeField] private Sprite flag_Sprite;
    [SerializeField] private bool dontDestroyOnUse;


    private LevelManager lm;

    private void Awake()
    {
        lm = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lm.Checkpoint = RespawnPosition;

            if (flag != null)
            {
                flag.GetComponent<SpriteRenderer>().sprite = flag_Sprite;
                AudioManager.instance.PlaySound("Checkpoint");
            }

            if (dontDestroyOnUse == false)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
