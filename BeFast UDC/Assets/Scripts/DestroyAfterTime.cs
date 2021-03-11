using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [SerializeField] bool isLight;
    void Start()
    {
        if (!isLight)
        {
            Destroy(this.gameObject, timeToDestroy);
        }
        else
        {

        }
    }
    /*IEnumerator ChangeLightSize()
    {
        
    }*/
}
