using UnityEditor;
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
    GameObject longBody;
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
        LongNoteMovement moveScript = null;
        LongNoteMovement colorScript = null;
        if (longHead != null)
        {
            moveScript = longHead.GetComponentInParent<LongNoteMovement>();
        }
        if (longBody != null)
        {
            colorScript = longBody.GetComponentInParent<LongNoteMovement>();
        }
        
        if (Input.GetKeyDown(keyToPress))
        { 
            if (headTouched && longHead != null)
            {
                
                //float distance = Mathf.Abs(longHead.transform.position.y - transform.position.y);
                //Debug.Log("longHead+1");
                //if (distance <= 0.5f) Debug.Log("longHead: Perfect!!");
                //else if (distance <= 1.2f) Debug.Log("longHead: Great!");
                //else Debug.Log("longHead: Good");
                headTouched = false;
                isPressing = true;
                if (longHead != null)
                {
                    
                    if (moveScript != null)
                    {
                        moveScript.isBeingHeld = true;
                        //Debug.Log("movescript設定了");
                    }
                    else
                    {
                        
                        
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
        else if (other.CompareTag("D_Body"))
        {
            tailTouched = true;
            longBody = other.gameObject;
            Debug.Log("longBody touched");
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
