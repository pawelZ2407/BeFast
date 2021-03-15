using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LaserPickUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 36000), 999, RotateMode.FastBeyond360);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
