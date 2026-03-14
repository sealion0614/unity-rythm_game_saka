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

    public void SetMyScale(float s)
    {
        myScale = s;
    }
    void Start()
    {
        Hit = GameObject.FindWithTag("hit");
        judgeLineY=Hit.transform.position.y;
        Debug.Log("judgeline" + judgeLineY);
        myScale = data.defaultHeight;
        sr = bodyChange.GetComponent<Image>();
        if (headChange.position.x == -225)
        {
            Key = KeyCode.D;
        }
        else if(headChange.position.x ==-75)
        {
            Key = KeyCode.F;
        }
        else if (headChange.position.x == 75)
        {
            Key = KeyCode.J;
        }
        else
        {
            Key = KeyCode.J;
        }
        //bodySpriteUnitHeight = sr.sprite.bounds.size.y;

    }

    void Update()
    {
        

        Color colors = sr.color;
        colors.a = 1f;
        sr.color = colors;
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        //if (!hasInitialized && headChange.transform.position.y <= judgeLineY)
        //{
        //    initialBodyLocalPos = tailChange.position.y - judgeLineY;
        //    initialLocalScaleY = bodyChange.localScale.y;
        //    hasInitialized = true;
        //    //bodyChange.position = headChange.position;
        //}

        //if (hasInitialized)
        //{
        Debug.Log(isBeingHeld);
        if (isBeingHeld)
            {
            Debug.Log("頭部實際 Y 座標: " + headChange.position.y);
            if (isBeingHeld && headChange.position.y <= judgeLineY)
            {
                Debug.Log("stuck");
                Vector3 headPos = headChange.position;
                headPos.y = judgeLineY;
                headChange.position = headPos;
                //LongNoteShrink();

            }
            else if (headChange.position.y <= judgeLineY)
            {
                Debug.Log("change color");
                colors.a = 0.7f;
                sr.color = colors;
            }
            if(!(Input.GetKey(Key))){
                Debug.Log("held");
                isBeingHeld = false;
            }
        }
            if (tailChange.position.y <= judgeLineY)
            {
                Debug.Log("long finished");
                Destroy(gameObject); return;
            }

            //if (transform.position.y < ) { Destroy(gameObject); }
        }
    //}
}