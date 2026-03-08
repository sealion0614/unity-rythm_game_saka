using UnityEngine;
using System.Collections;
using NUnit.Framework.Constraints;

public class D_shortNoteManager : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform spawnPoint;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 pressed");
            SpawnNote();
            StartCoroutine(RealTimeWait(1f));
        }
    }
    public void SpawnNote()
    {
        Instantiate(notePrefab,new Vector3(-7.11f,5f,0f), Quaternion.identity);
    }

    IEnumerator RealTimeWait(float second)
    {
        yield return new WaitForSeconds(second);
        SpawnNote();
    }
}
