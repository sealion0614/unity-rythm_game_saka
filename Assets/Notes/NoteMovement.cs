using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class NoteMovement : MonoBehaviour
{
    public float speed = 500f;
   
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);


     if (transform.position.y < 0f) { Destroy(gameObject); }
    }
}
        //if (tailObject != null)
        //{
        //    tailObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
