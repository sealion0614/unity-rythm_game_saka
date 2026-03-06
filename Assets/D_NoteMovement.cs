using UnityEngine;

public class D_NoteMovement : MonoBehaviour
{
    public float speed = 5f;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -6f) { Destroy(gameObject); }
    }
}
