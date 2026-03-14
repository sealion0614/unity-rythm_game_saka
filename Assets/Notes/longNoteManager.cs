using System.Collections;
using UnityEngine;

public class longNoteManager : MonoBehaviour
{
    [Header("變形設定")]
    public RectTransform bodyRect;
    public RectTransform tailRect;
    public float speed = 500f;
    public GameObject notePrefab;
    public Transform spawnPoint;
    public Transform bodyChange;
    public Transform tailChange;
    public float defaultHeight;
    public float bodyHeight;
    public float bodySpriteUnitHeight;
    public float InitScale;
    void Start()
    {

    }

    public void SetDuration(float duration)
    {
        float newHeight = duration * speed;
        float tailHeight = 0f;
        if (tailRect != null)
        {
            tailHeight = tailRect.rect.height;
        }
        float bodyHeight = newHeight - tailHeight;
        if (bodyHeight < 0f) bodyHeight = 0f;
        if (bodyRect != null)
        {
            bodyRect.sizeDelta = new Vector2(bodyRect.sizeDelta.x, bodyHeight);
        }
        if (tailRect != null)
        {
            tailRect.anchoredPosition = new Vector2(tailRect.anchoredPosition.x, bodyHeight);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Debug.Log("2 pressed");
            SpawnNote(1f);
            StartCoroutine(RealTimeWait(2f,5f));

        }
    }
    public void SpawnNote(float shrink)
    {
        defaultHeight = shrink;
        GameObject note=Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
        LongNoteMovement movement=note.GetComponent<LongNoteMovement>();

        movement.data = this;
        movement.SetMyScale(shrink);

        InitScale = movement.bodyChange.localScale.y;
        SpriteRenderer sr = movement.bodyChange.GetComponent<SpriteRenderer>();
        bodySpriteUnitHeight = sr.sprite.rect.height / sr.sprite.pixelsPerUnit;
        
        Vector3 newScale = movement.bodyChange.localScale;
        newScale.y = shrink * InitScale;
        movement.bodyChange.localScale= newScale;
        //float finalLength = bodySpriteUnitHeight * newScale.y;

        movement.tailChange.localPosition = new Vector3(
            movement.tailChange.localPosition.x,
            movement.bodyChange.localPosition.y+bodySpriteUnitHeight* InitScale * shrink ,
            movement.tailChange.localPosition.z
            );
        //Debug.Log($"生成音符: 原始Scale={bodySpriteUnitHeight}, 最終Scale={newScale.y}, Tail位置={movement.bodyChange.localPosition.y + bodySpriteUnitHeight * movement.bodyChange.localScale.y}");

    }

    IEnumerator RealTimeWait(float second,float shrink)
    {
        yield return new WaitForSeconds(second);
        SpawnNote(shrink);
    }
}
