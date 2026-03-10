using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class LongNoteMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool isBeingHeld = false;
    public Transform bodyChange;
    public Transform tailChange;
    public Transform headChange;

    public GameObject headObject;
    public GameObject tailObject;

    public longNoteManager data;

    public float OriginalHeight = 1f;
    public float initialBodyLocalPos;
    public float initialLocalScaleY;
    public float judgeLineY = -5.05f;
    private bool hasInitialized = false;
    public float bodySpriteUnitHeight;
    private float myScale=1f;

    public void SetMyScale(float s)
    {
        myScale = s; 
    }
    void Start()
    {
        myScale = data.defaultHeight;
        SpriteRenderer sr = bodyChange.GetComponent<SpriteRenderer>();
        bodySpriteUnitHeight = sr.sprite.bounds.size.y;
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (!hasInitialized&&headChange.transform.position.y <= judgeLineY)
        {
            initialBodyLocalPos = tailChange.position.y-judgeLineY;
            initialLocalScaleY = bodyChange.localScale.y;
            hasInitialized = true;
            //bodyChange.position = headChange.position;
        }
        
        if(hasInitialized)
        {
            
            if (bodyChange != null && bodyChange.localScale.y <= 0.01f)
            {
                Debug.Log("long finished!");
                Destroy(gameObject);
                return;
            }
            if (isBeingHeld)
            {
                isBeingHeld = true;
                LongNoteShrink();

            }
            else
            {
                isBeingHeld = false;
            }

            if (tailChange.position.y <= judgeLineY) {
                Debug.Log("long finished");
                Destroy(gameObject); return;
            }

            //if (transform.position.y < -4.5) { Destroy(gameObject); }
        }
    }
    void LongNoteShrink()
    {
        Vector3 headPos = headChange.position;
        headPos.y = judgeLineY;
        headChange.position = headPos;

        float currentLength = tailChange.position.y - judgeLineY;
        if (currentLength > 0)
        {
            float parentScaleY = transform.lossyScale.y;
            Vector3 newScale=bodyChange.localScale;

            newScale.y = currentLength / ((bodySpriteUnitHeight * parentScaleY)*(myScale));
        }
    }
 }
