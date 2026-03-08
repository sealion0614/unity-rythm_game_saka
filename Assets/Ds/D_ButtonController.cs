using UnityEngine;
using UnityEngine.Rendering;

public class D_ButtonController : MonoBehaviour
{
    public KeyCode D;
    bool canBePressed = false;
    bool isPressing = false;
    bool headTouched = false;
    bool tailTouched = false;
    GameObject D_short;
    GameObject D_longHead;
    GameObject D_longTail;
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(D))
        {
            
            if (headTouched)
            {
                Debug.Log("longHead+1");
                headTouched = false;
                isPressing = true;
                if (D_longHead != null)
                {
                    D_LongNoteMovement moveScript=D_longHead.GetComponentInParent<D_LongNoteMovement>();
                    if (moveScript != null)
                    {
                        moveScript.isBeingHeld = true;
                    }
                }
            }
            else if (canBePressed)
            {
                Destroy(D_short);
                canBePressed = false;
                Debug.Log("short+1");

            }
        }
        if (Input.GetKeyUp(D))
        {
            if (isPressing)
            {
                if (tailTouched)
                {
                    Debug.Log("tailHead+1");
                }
                else
                {
                    Debug.Log("miss");
                }
                isPressing= false;
                if (D_longTail != null)
                {
                    Destroy(D_longTail.transform.parent.gameObject);
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
            D_short= other.gameObject;
            Debug.Log("short touched");
        }
        else if (other.CompareTag("D_Head"))
        {
            headTouched = true;
            D_longHead = other.gameObject;
            Debug.Log("longHead touched");
        }
        else if (other.CompareTag("D_Tail"))
        {
            tailTouched = true;
            D_longTail = other.gameObject;
            Debug.Log("longTail touched");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("D_Key"))
        {
            canBePressed = false;
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
