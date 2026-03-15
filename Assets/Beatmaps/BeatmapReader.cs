using System.Collections.Generic;
using UnityEngine;

public class BeatmapReader : MonoBehaviour 
{
    [Header("檔案與音樂設定")]
    public TextAsset beatmapCSV;
    public AudioSource music;

    [Header("軌道起點 (依序放 D, F, J, K)")]
    public Transform[] trackSpawns = new Transform[4];

    [Header("音符 Prefab")]
    public GameObject shortNotePrefab;
    public GameObject longNotePrefab;
    public Transform noteContainer;

    [Header("時間設定")]
    public float fallTime = 2f;
    private float gameTimer = 0f;
    private bool isPlaying = false;

    private class NoteData
    {
        public float time;
        public int track;
        public string type;
        public float duration;
    }

    private List<NoteData> notes = new List<NoteData>();
    private int currentIndex = 0;

    void Start()
    {
        ReadCSV();
        //Debug.Log(notes.Count + " 個音符！");
        if (music != null)
        {
            music.PlayDelayed(fallTime); 
            isPlaying = true;
        }
        if (shortNotePrefab != null)
        {
            GameObject dummyShort = Instantiate(shortNotePrefab, new Vector3(0, 10000, 0), Quaternion.identity);
            dummyShort.SetActive(false);
            Destroy(dummyShort, 0.1f);
        }
        if (longNotePrefab != null) 
        {
            GameObject dummyLong = Instantiate(longNotePrefab, new Vector3(0, 10000, 0), Quaternion.identity);
            dummyLong.SetActive(false); 
            Destroy(dummyLong, 0.1f);   
        }
    }

    void ReadCSV()
    {
        if (beatmapCSV == null) return;
        string[] lines = beatmapCSV.text.Split('\n');
        for (int i = 1; i < lines.Length; i++) 
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;
            string[] cols = line.Split(',');
            if (cols.Length >= 1)
            {
                if (string.IsNullOrEmpty(cols[0].Trim())) continue;
                float timeInSeconds = ParseTime(cols[0].Trim());
                for (int track = 0; track < 4; track++)
                {
                    if (track + 1 < cols.Length)
                    {
                        string cell = cols[track + 1].Trim().ToLower();
                        if (cell == "short" || cell.StartsWith("long_"))
                        {
                            NoteData newNote = new NoteData();
                            newNote.time = timeInSeconds;
                            newNote.track = track;
                            if (cell == "short")
                            {
                                newNote.type = "short";
                                newNote.duration = 0f;
                            }
                            else if (cell.StartsWith("long_"))
                            {
                                newNote.type = "long";
                                string[] parts = cell.Split('_');
                                if (parts.Length > 1) 
                                {
                                    newNote.duration = float.Parse(parts[1]);
                                    //Debug.Log("成功讀取到長魚，長度是：" + newNote.duration);
                                } 
                                else 
                                {
                                    newNote.duration = 1f;
                                }
                                //Debug.Log($"長音符！時間:{newNote.time}秒, 軌道:{newNote.track}, 長度:{newNote.duration}");
                            }
                            notes.Add(newNote);
                        }
                    }
                }
            }
        }
        notes.Sort((a, b) => a.time.CompareTo(b.time));
    }

    float ParseTime(string t)
    {
        string[] parts = t.Split(new char[] { '"', '\'', ':' }, System.StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 3)
        {
            float m = float.Parse(parts[0]);
            float s = float.Parse(parts[1]);
            float ms = float.Parse(parts[2]);
            return (m * 60f) + s + (ms / 1000f);
        }
        return 0f;
    }
    
    void Update()
    {
        if (!isPlaying || currentIndex >= notes.Count) return;
        if (music == null) return;
        if (gameTimer < fallTime)
        {
            gameTimer += Time.deltaTime;
        }
        else
        {
            gameTimer = music.time + fallTime;
        }
        while (currentIndex < notes.Count && gameTimer >= notes[currentIndex].time)
        {
            SpawnNote(notes[currentIndex]);
            currentIndex++;
        }
    }

    void SpawnNote(NoteData data)
    {
        GameObject prefabToSpawn = shortNotePrefab;
        if (data.type == "long" && longNotePrefab != null)
        {
            prefabToSpawn = longNotePrefab;
        }
        if (prefabToSpawn == null) return;
        Transform spawnPoint = trackSpawns[data.track];
        GameObject newNote = Instantiate(prefabToSpawn, noteContainer);
        newNote.transform.localPosition = spawnPoint.position;
        newNote.transform.localScale = Vector3.one;
        if (data.type == "long")
        {
            longNoteManager longScript = newNote.GetComponent<longNoteManager>();
            if (longScript != null)
            {
                longScript.SetDuration(data.duration);
            }
        }
        newNote.transform.position = spawnPoint.position;
        newNote.transform.SetParent(noteContainer, false);
        newNote.transform.localScale = Vector3.one;
    }
}
