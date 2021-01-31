using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHint : MonoBehaviour
{
    public HintsManager hintsManager;

    public string hintName;
    public float duration;

    private void Awake()
    {
        hintsManager = FindObjectOfType<HintsManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hintsManager.ShowHintPanel(hintName, duration);
            Destroy(this.gameObject);
        }
    }
}
