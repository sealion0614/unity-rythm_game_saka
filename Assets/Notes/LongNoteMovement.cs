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
    public float tailHitWindow = 80f;
    public bool isBeingHeld = false;
    public Transform bodyChange;
    public Transform tailChange;
    public Transform headChange;
    public UnityEngine.UI.Image sr;

    public GameObject headObject;
    public GameObject tailObject;

    public longNoteManager data;

    public float OriginalHeight = 1f;
    public float initialBodyLocalPos;
    public float initialLocalScaleY;
    private float judgeLineY = -400;
    private bool hasInitialized = false;
    public float bodySpriteUnitHeight;
    private float myScale = 1f;
    public KeyCode Key;
    public GameObject Hit;
    private float startY;
    private float startTime;
    private float originalLength;

    public void SetMyScale(float s)
    {
        myScale = s;
    }
    void Start()
    {
        startY = transform.localPosition.y;
        startTime = Time.time;
        //Hit = GameObject.FindWithTag("hit");
        judgeLineY = -400f;
        myScale = data.defaultHeight;
        sr = bodyChange.GetComponent<Image>();
        float myX = transform.localPosition.x;
        if (Mathf.Abs(myX - (-225f)) < 20f)
        {
            Key = KeyCode.D;
        }
        else if(Mathf.Abs(myX - (-75f)) < 20f)
        {
            Key = KeyCode.F;
        }
        else if (Mathf.Abs(myX - 75f) < 20f)
        {
            Key = KeyCode.J;
        }
        else
        {
            Key = KeyCode.K;
        }
        //Debug.Log(gameObject.name + " 的起跑點是：" + startY + "，速度是：" + speed);
        //bodySpriteUnitHeight = sr.sprite.bounds.size.y;
        if (bodyChange != null) 
        {
            originalLength = bodyChange.GetComponent<RectTransform>().sizeDelta.y;
        }
    }

    void Update()
    {
        

        Color colors = sr.color;
        colors.a = 1f;
        sr.color = colors;

        float timeAlive = Time.time - startTime;
        float currentY = startY - (timeAlive * speed);
        transform.localPosition = new Vector3(transform.localPosition.x, currentY, transform.localPosition.z);
        float tailAbsoluteY = currentY;
        if (tailChange != null) 
        {
            tailAbsoluteY = currentY + tailChange.localPosition.y;
        }
        //if (!hasInitialized && headChange.transform.position.y <= judgeLineY)
        //{
        //    initialBodyLocalPos = tailChange.position.y - judgeLineY;
        //    initialLocalScaleY = bodyChange.localScale.y;
        //    hasInitialized = true;
        //    //bodyChange.position = headChange.position;
        //}

        //if (hasInitialized)
        //{
        //Debug.Log(isBeingHeld);
        if(Key != KeyCode.None)
        {
            if (Input.GetKeyUp(Key))
            {
                if (isBeingHeld && Mathf.Abs(tailAbsoluteY - judgeLineY) <= tailHitWindow)
                {
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    isBeingHeld = false;
                }
            }
            else if (!Input.GetKey(Key))
            {
                isBeingHeld = false;
            }
            if (isBeingHeld && tailAbsoluteY < (judgeLineY - tailHitWindow))
            {
                isBeingHeld = false;
            }
        }
        if (isBeingHeld && currentY <= judgeLineY)
        {
            float depth = judgeLineY - currentY;
            if (headChange != null)
            {
                headChange.localPosition = new Vector3(headChange.localPosition.x, depth, headChange.localPosition.z);
            }
            if (bodyChange != null && tailChange != null)
            {
                bodyChange.localPosition = new Vector3(bodyChange.localPosition.x, depth, bodyChange.localPosition.z);
                float originalHeight = tailChange.localPosition.y;
                float currentBodyHeight = originalHeight - depth;
                if (currentBodyHeight < 0) currentBodyHeight = 0;

                RectTransform bodyRect = bodyChange.GetComponent<RectTransform>();
                bodyRect.sizeDelta = new Vector2(bodyRect.sizeDelta.x, currentBodyHeight);
            }
            else
            {
                if (headChange != null) headChange.localPosition = new Vector3(headChange.localPosition.x, 0, headChange.localPosition.z);
                if (bodyChange != null) bodyChange.localPosition = new Vector3(bodyChange.localPosition.x, 0, bodyChange.localPosition.z);
                if (bodyChange != null && tailChange != null) 
                {
                    RectTransform bodyRect = bodyChange.GetComponent<RectTransform>();
                    bodyRect.sizeDelta = new Vector2(bodyRect.sizeDelta.x, tailChange.localPosition.y);
                }
            }
            if (headChange.localPosition.y <= judgeLineY)
            {
                //Debug.Log("change color");
                colors.a = 0.7f;
                sr.color = colors;
            }
            
        }
            if (tailChange != null && (currentY + tailChange.localPosition.y) <= judgeLineY)
            {
                //Debug.Log("long finished");
                Destroy(gameObject);
            }

            else if (transform.position.y < -1000f) 
            { 
                Destroy(gameObject); 
            }
        }
    //}
}