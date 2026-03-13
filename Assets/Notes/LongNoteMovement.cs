using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LongNoteMovement : MonoBehaviour
{
    public float speed = 500f;
    public bool isBeingHeld = false;
    public Transform bodyChange;
    public Transform tailChange;
    public Transform headChange;
    public Image sr;

    public GameObject headObject;
    public GameObject tailObject;

    public longNoteManager data;

    public float OriginalHeight = 1f;
    public float initialBodyLocalPos;
    public float initialLocalScaleY;
    public float judgeLineY = -2.55f;
    private bool hasInitialized = false;
    public float bodySpriteUnitHeight;
    private float myScale = 1f;

    public void SetMyScale(float s)
    {
        myScale = s;
    }
    void Start()
    {
        myScale = data.defaultHeight;
        sr = bodyChange.GetComponent<Image>();
        //bodySpriteUnitHeight = sr.sprite.bounds.size.y;

    }

    void Update()
    {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (!hasInitialized && headChange.transform.position.y <= judgeLineY)
        {
            initialBodyLocalPos = tailChange.position.y - judgeLineY;
            initialLocalScaleY = bodyChange.localScale.y;
            hasInitialized = true;
            //bodyChange.position = headChange.position;
        }

        if (hasInitialized)
        {

            if (bodyChange != null && bodyChange.localScale.y <= 0.01f)
            {
                Debug.Log("long finished!");
                Destroy(gameObject);
                return;
            }
            if (isBeingHeld && !(Input.GetKey(KeyCode.D)))
            {
                isBeingHeld = false;
            }
            if (isBeingHeld&&headChange.position.y==-400f)
            {
                Vector3 headPos = headChange.position;
                headPos.y = judgeLineY;
                headChange.position = headPos;
                //LongNoteShrink();

            }
            else
            {
                isBeingHeld = false;
                Color colors = sr.color;
                colors.a = 0.7f;
                sr.color = colors;
            }



            if (tailChange.position.y <= judgeLineY)
            {
                Debug.Log("long finished");
                Destroy(gameObject); return;
            }

            //if (transform.position.y < ) { Destroy(gameObject); }
        }
    }
}