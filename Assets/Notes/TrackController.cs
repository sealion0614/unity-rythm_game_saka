using UnityEngine;
using UnityEngine.Rendering;

public class TrackController : MonoBehaviour
{
    public static int shortperfectCount = 0;
    public static int shortmissCount = 0;
    public static int longperfectCount = 0;
    public static int longmissCount = 0;
    public KeyCode keyToPress;
    public float hitWindow = 80f;
    bool canBePressed = true;
    bool isPressing = false;
    bool headTouched = false;
    bool tailTouched = false;
    public GameObject Short;
    public GameObject currentNote;
    GameObject longHead;
    GameObject longTail;
    void Start()
    {
        shortperfectCount = 0;
        shortmissCount = 0;
        longperfectCount = 0;
        longmissCount = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        { 
            if (headTouched && longHead != null)
            {
                headTouched = false;
                isPressing = true;
                if (longHead != null)
                {
                    LongNoteMovement moveScript=longHead.GetComponentInParent<LongNoteMovement>();
                    if (moveScript != null)
                    {
                        moveScript.isBeingHeld = true;
                        //Debug.Log("movescript設定了");
                    }
                    else
                    {
                        
                        //Debug.LogError("找不到 LongNoteMovement 腳本！碰撞到的物件名稱是: " + longHead.name);
                    }
                }
            }
            if (canBePressed && Short != null)
            {
                float distance = Mathf.Abs(Short.transform.localPosition.y - transform.localPosition.y);
                if(distance <= hitWindow)
                {
                    Destroy(Short);
                    Short = null;
                    canBePressed = false;
                    Debug.Log("short perfect");
                    shortperfectCount++;
                }

            }
        }
        // if (Input.GetKeyUp(keyToPress))
        // {
        //     if (isPressing)
        //     {
        //         if (tailTouched)
        //         {
        //             Destroy(longTail.transform.parent.gameObject);
        //             //Debug.Log("tailHead+1");
        //         }
        //         isPressing= false;
        //         if (longTail != null)
        //         {
        //             Destroy(longTail.transform.parent.gameObject);
        //             tailTouched = false;
        //         }

        //     }

        // }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(keyToPress == KeyCode.D)
            {
                Debug.Log($"========== 遊戲結算 ==========\nshortPerfect 總數: {shortperfectCount}\nshortMiss 總數: {shortmissCount}\nlongPerfect 總數: {longperfectCount}\nlongMiss 總數: {longmissCount}\n==============================");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("D_Key"))
        {
            canBePressed = true;
            Short= other.gameObject;
            //Debug.Log("short touched");
        }
        else if (other.CompareTag("D_Head"))
        {
            headTouched = true;
            longHead = other.gameObject;
            //Debug.Log("longHead touched");
        }
        else if (other.CompareTag("D_Tail"))
        {
            tailTouched = true;
            longTail = other.gameObject;
            //Debug.Log("longTail touched");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("D_Key"))
        {
            //canBePressed = false;
            //Debug.Log("short out");
        }
        if (other.CompareTag("D_Head"))
        {
            headTouched = false;
            //Debug.Log("Head out");
        }
        if (other.CompareTag("D_Tail"))
        {
            tailTouched = false;
            //Debug.Log("Tail out");
        }

    }
}
