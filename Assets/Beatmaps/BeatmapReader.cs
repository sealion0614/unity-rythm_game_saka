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

    private class NoteData
    {
        public float time;
        public int track;
        public string type;
    }

    private List<NoteData> notes = new List<NoteData>();
    private int currentIndex = 0;

    void Start()
    {
        ReadCSV();
        if (music != null) music.Play(); 
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
                float timeInSeconds = ParseTime(cols[0]);
                for (int track = 0; track < 4; track++)
                {
                    if (track + 1 < cols.Length)
                    {
                        string cell = cols[track + 1].Trim().ToLower();
                        if (cell == "short" || cell == "long")
                        {
                            NoteData newNote = new NoteData();
                            newNote.time = timeInSeconds;
                            newNote.track = track;
                            newNote.type = cell;
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
        t = t.Replace("\"", ":").Replace("'", ":");
        string[] parts = t.Split(':');
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
        if (music == null) return;
        while (currentIndex < notes.Count && music.time >= notes[currentIndex].time)
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
        GameObject newNote = Instantiate(prefabToSpawn, trackSpawns[data.track].position, Quaternion.identity);
        newNote.transform.SetParent(noteContainer, false);
        newNote.transform.position = trackSpawns[data.track].position;
        newNote.transform.localScale = Vector3.one;
    }
}
