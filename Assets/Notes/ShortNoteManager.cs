using UnityEngine;
using System.Collections;
using NUnit.Framework.Constraints;

public class ShortNoteManager : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform spawnPoint;
    public KeyCode trackKey;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(trackKey))
        {
            //Debug.Log("1 pressed");
            SpawnNote();
            StartCoroutine(RealTimeWait(1f));
        }
    }
    public void SpawnNote()
    {
        Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator RealTimeWait(float second)
    {
        yield return new WaitForSeconds(second);
        SpawnNote();
    }
}
