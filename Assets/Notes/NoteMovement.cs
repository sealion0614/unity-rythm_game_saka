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
        Debug.Log(gameObject.name + " 的起跑點是：" + startY + "，速度是：" + speed);
    }

    void Update()
    {
        float timeAlive = Time.time - startTime;
        float currentY = startY - (timeAlive * speed);
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);

        if (transform.position.y < 0f) { Destroy(gameObject); }
    }
}
        //if (tailObject != null)
        //{
        //    tailObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
