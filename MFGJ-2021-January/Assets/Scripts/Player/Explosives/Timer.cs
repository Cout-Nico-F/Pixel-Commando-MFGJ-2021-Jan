using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float delay;
    [SerializeField]
    private GameObject explosionPrefab;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        var gunning = FindObjectOfType<Gunning>();
        //set isplanted to false
        gunning.Explosives.BombIsPlanted = false;
        //instantiate explosion
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //destroy this object.
        Destroy(this.gameObject);
    }
}
