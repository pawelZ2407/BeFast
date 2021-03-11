using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLightScript : MonoBehaviour
{
    Vector2 startPosition;
    [SerializeField] float moveSpeed;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime,Space.World);
        Vector2 laserEndPos = GameManager.instance.laserEndPosition;
        if(Vector2.Distance(startPosition,transform.position) > Vector2.Distance(startPosition, laserEndPos))
        {
            gameObject.SetActive(false);
        }
    }
}
