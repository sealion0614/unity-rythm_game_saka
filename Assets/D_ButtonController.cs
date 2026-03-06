using UnityEngine;

public class D_ButtonController : MonoBehaviour
{
    public KeyCode D;
    bool canBePressed = false;
    GameObject currentNote;
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(D))
        {
            if (canBePressed)
            {
                Destroy(currentNote);
                Debug.Log("+1");

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("D_Key"))
        {
            canBePressed = true;
            currentNote= other.gameObject;
            Debug.Log("touched");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canBePressed = false;
        Debug.Log("out");
    }
}
