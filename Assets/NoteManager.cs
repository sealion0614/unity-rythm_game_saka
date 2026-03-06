using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform spawnPoint;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space pressed");
            SpawnNote();
        }
    }
    public void SpawnNote()
    {
        Instantiate(notePrefab, spawnPoint.position,Quaternion.identity);
    }
}
