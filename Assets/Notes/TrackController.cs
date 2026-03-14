using UnityEditor;
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
    GameObject longBody;
    GameObject longTail;
    void Start()
    {
        
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
                /*float distance = Mathf.Abs(Short.transform.position.y - transform.position.y);
                if (distance <= 0.5f) Debug.Log("short: Perfect!!");
                else if (distance <= 1.2f) Debug.Log("short: Great!");
                else Debug.Log("short: Good");*/
                
                Destroy(Short);
                Short = null;
                // canBePressed = false;
                //Debug.Log("short+1");

            }
        }
        else if (headTouched && !Input.GetKeyDown(keyToPress) && longHead != null)
        {
            Debug.Log("miss");
            if (colorScript != null)
            {
                colorScript.missing = true;
            }
        }
        if (Input.GetKeyUp(keyToPress))
        {
            if (isPressing)
            {
                if (tailTouched && longTail != null)
                {
                    /*float distance = Mathf.Abs(longHead.transform.position.y - transform.position.y);
                    if (distance <= 0.5f) Debug.Log("longTail: Perfect!!");
                    else if (distance <= 1.2f) Debug.Log("longTail: Great!");
                    else Debug.Log("longTail: Good");*/
                    GameObject parent = longTail.transform.parent.gameObject;
                    Destroy(parent);
                    longTail = null;
                    //Debug.Log("tailHead+1");
                }
                else
                {
                    Debug.Log("miss");
                    if (colorScript != null)
                    {
                        colorScript.missing = true;
                    }

                }
                isPressing = false;
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
            Debug.Log("short out");
        }
        if (other.CompareTag("D_Head"))
        {
            headTouched = false;
            Debug.Log("Head out");
            longHead = null;
        }
        if (other.CompareTag("D_Tail"))
        {
            tailTouched = false;
            Debug.Log("Tail out");
            longHead = null;
        }

    }
}
