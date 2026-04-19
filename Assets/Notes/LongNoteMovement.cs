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
    public float HitWindow = 80f;
    public bool isBeingHeld = false;
    public Transform bodyChange;
    public Transform tailChange;
    public Transform headChange;
    //public UnityEngine.UI.Image sr;
    private CanvasGroup cg;
    public bool missing;

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
    private bool judged = false;
    private bool isMissed = false;
    private bool lengthCalculated = false;

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
        // if (bodyChange != null) {
        //     sr = bodyChange.GetComponent<Image>();
        // }
        cg = GetComponent<CanvasGroup>();
        if (cg == null) {
            cg = gameObject.AddComponent<CanvasGroup>();
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
        if (!lengthCalculated && tailChange != null)
        {
            originalLength = Mathf.Abs(tailChange.localPosition.y * transform.localScale.y);
            if (originalLength > 10f) lengthCalculated = true;
        }
        float tailAbsoluteY = currentY + originalLength;
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
        if(Key != KeyCode.None && !isMissed)
        {
            if (!isBeingHeld)
            {
                if (Input.GetKeyDown(Key))
                {
                    float headDistance = Mathf.Abs(currentY - judgeLineY);
                    if (headDistance <= HitWindow) 
                    {
                        isBeingHeld = true;
                    }
                }
                else if (currentY < (judgeLineY - HitWindow))
                {
                    isMissed = true;
                    if (!judged)
                    {
                        Debug.Log("long miss");
                        judged = true;
                        TrackController.longmissCount++;
                    }
                }
            }
            else
            {
                if (Input.GetKeyUp(Key))
                {
                    float distance = Mathf.Abs(tailAbsoluteY - judgeLineY);
                    float tailDistance = Mathf.Abs(tailAbsoluteY - judgeLineY);
                    if(tailDistance <= HitWindow)
                    {
                        if (!judged)
                        {
                            Debug.Log("long perfact");
                            judged = true;
                            TrackController.longperfectCount++;
                        }
                        Destroy(gameObject);
                        return;
                    }
                    else
                    {
                        isMissed = true;
                        isBeingHeld = false;
                        if (!judged)
                        {
                            Debug.Log("long miss");
                            judged = true;
                            TrackController.longmissCount++;
                        }
                    }
                }
                else if (!Input.GetKey(Key))
                {
                    isMissed = true;
                    isBeingHeld = false;
                    if (!judged)
                    {
                        Debug.Log("long miss");
                        judged = true;
                        TrackController.longmissCount++;
                    }
                }
                else if (tailAbsoluteY < (judgeLineY - HitWindow))
                {
                    isMissed = true;
                    if (!judged)
                    {
                        Debug.Log("long miss");
                        judged = true;
                        TrackController.longmissCount++;
                    }
                    Destroy(gameObject);
                }
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