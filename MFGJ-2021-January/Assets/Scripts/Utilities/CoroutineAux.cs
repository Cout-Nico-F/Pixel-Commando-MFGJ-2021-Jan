using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineAux : MonoBehaviour
{
   public void HideObject(float time, GameObject gameobject)
    {
        StartCoroutine(HideObjectCoroutine(time, gameobject));
    }

    public IEnumerator HideObjectCoroutine(float time, GameObject gameobject)
    {
        //hide
        gameobject.SetActive(false);
        //wait
        yield return new WaitForSeconds(time);
        gameobject.SetActive(true);
        //show again and exit
    }
}
