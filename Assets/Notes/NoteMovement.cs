using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class NoteMovement : MonoBehaviour
{
    public float speed = 500f;
    private float startY;
    private float startTime;
    void Start()
    {
        startY = transform.position.y;
        startTime = Time.time;
    }

    void Update()
    {
        float timeAlive = Time.time - startTime;
        float currentY = startY - (timeAlive * speed);
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);

        if (transform.position.y < -540f) 
        { 
            Debug.Log("short miss");
            Destroy(gameObject); 
            TrackController.shortmissCount++;
        }
    }
}
        //if (tailObject != null)
        //{
        //    tailObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
