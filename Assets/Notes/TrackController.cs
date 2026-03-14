using UnityEngine;
using UnityEngine.Rendering;

public class TrackController : MonoBehaviour
{
    public KeyCode keyToPress;
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
        
    }


    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        { 
            Debug.Log("press D");
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
                     LongNoteMovement moveScript=longHead.GetComponentInParent<LongNoteMovement>();
                     if (moveScript != null)
                     {
                         moveScript.isBeingHeld = true;
                        Debug.Log("movescript設定了");
                     }
                    else
                    {
                        
                        Debug.LogError("找不到 LongNoteMovement 腳本！碰撞到的物件名稱是: " + longHead.name);
                    }
                }
             }
            if (canBePressed && Short != null)
            {
                /*float distance = Mathf.Abs(Short.transform.position.y - transform.position.y);
                if (distance <= 0.5f) Debug.Log("short: Perfect!!");
                else if (distance <= 1.2f) Debug.Log("short: Great!");
                else Debug.Log("short: Good");*/
                
                Destroy(Short);
                Short = null;
                // canBePressed = false;
                Debug.Log("short+1");

            }
        }
        if (Input.GetKeyUp(keyToPress))
        {
            if (isPressing)
            {
                if (tailTouched)
                {
                    /*float distance = Mathf.Abs(longHead.transform.position.y - transform.position.y);
                    if (distance <= 0.5f) Debug.Log("longTail: Perfect!!");
                    else if (distance <= 1.2f) Debug.Log("longTail: Great!");
                    else Debug.Log("longTail: Good");*/
                    Destroy(longTail.transform.parent.gameObject);
                    Debug.Log("tailHead+1");
                }
                else
                {
                    Debug.Log("miss");
                }
                isPressing= false;
                if (longTail != null)
                {
                    Destroy(longTail.transform.parent.gameObject);
                    tailTouched = false;
                }

            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("D_Key"))
        {
            canBePressed = true;
            Short= other.gameObject;
            Debug.Log("short touched");
        }
        else if (other.CompareTag("D_Head"))
        {
            headTouched = true;
            longHead = other.gameObject;
            Debug.Log("longHead touched");
        }
        else if (other.CompareTag("D_Tail"))
        {
            tailTouched = true;
            longTail = other.gameObject;
            Debug.Log("longTail touched");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("D_Key"))
        {
            //canBePressed = false;
            Debug.Log("short out");
        }
        if (other.CompareTag("D_Head"))
        {
            headTouched = false;
            Debug.Log("Head out");
        }
        if (other.CompareTag("D_Tail"))
        {
            tailTouched = false;
            Debug.Log("Tail out");
        }

    }
}
