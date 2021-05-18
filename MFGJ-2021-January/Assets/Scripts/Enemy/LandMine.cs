using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField]
    private GameObject dirtPrefab;
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InfantryEnemy"))
        {
            return;
        }
        Instantiate(dirtPrefab, this.transform.position , this.transform.rotation);
        Instantiate(explosionPrefab, this.transform.position + new Vector3(0,2,0), this.transform.rotation);
        gameObject.SetActive(false);
    }
}
