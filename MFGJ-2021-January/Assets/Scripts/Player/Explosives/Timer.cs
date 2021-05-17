using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float delay;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject explosionPoint;

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
        //var special = FindObjectOfType<Specials>();
        ////set isplanted to false
        //special.Explosives.BombIsPlanted = false;
        ////instantiate explosion
        Instantiate(explosionPrefab, explosionPoint.transform.position, transform.rotation);
        //destroy this object.
        Destroy(this.gameObject);
    }
}
