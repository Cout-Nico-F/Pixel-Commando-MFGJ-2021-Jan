
using UnityEngine;

public class OverlapBox : MonoBehaviour
{
    Collider2D[] hitColliders;


    public Collider2D[] HitColliders { get => hitColliders; }

    private void Start()
    {
        MyCollisions();
        //Debug.Log("hitcolliders at start: "+hitColliders.Length);
    }
    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.       
        hitColliders = Physics2D.OverlapBoxAll(transform.position, this.GetComponent<Renderer>().bounds.size, 0f);
        int i = 0;       
        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            //Debug.Log("Hit : " + hitColliders[i].name + i);

            if (hitColliders[i].gameObject.GetComponent<Enemy>() != null)
            {
                hitColliders[i].gameObject.SetActive(true);
            }
            
            i++;
        }
    }
}