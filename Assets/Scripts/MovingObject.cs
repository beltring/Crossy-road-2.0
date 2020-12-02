using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingObject : MonoBehaviour
{
    private const int MAX_OBJECT_POSITION = 36;
    private const int MIN_OBJECT_POSITION = -36;

    private float speed;
    
    public GameObject movingObject;

    private void Start()
    {
        if (movingObject.tag == "Bus")
        {
            speed = Random.Range(5f, 7.5f);
        }
        else
        {
            speed = Random.Range(2.5f, 4f);
        }
    }

    private void Update()
    {
        if (movingObject.tag == "Bus")
        {
            
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        
        DeactivateObject();
    }

    private void DeactivateObject()
    {
        float zPosition = transform.position.z;
        if (zPosition > MAX_OBJECT_POSITION || zPosition < MIN_OBJECT_POSITION)
        {
            movingObject.SetActive(false);
        }
    }
}
