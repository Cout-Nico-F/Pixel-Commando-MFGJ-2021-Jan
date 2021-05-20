using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField]
    private GameObject exit;
    [SerializeField]
    private GameObject entrance;

    private static bool playerInside;
    private static float restoreValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerInside) //esta saliendo
            {
                playerInside = false;
                RestoreZoom();
                entrance.SetActive(true);
            }
            else //esta entrando
            {
                playerInside = true;
                ApplyZoom();
                exit.SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }

    private void ApplyZoom()
    {
        restoreValue = Camera.main.orthographicSize;
        Camera.main.orthographicSize *= 0.5f;
        //TODO: Method
    }
    private void RestoreZoom()
    {
        Camera.main.orthographicSize = restoreValue;
        //TODO: Method
    }

}
