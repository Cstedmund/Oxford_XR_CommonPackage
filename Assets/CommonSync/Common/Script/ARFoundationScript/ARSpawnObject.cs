using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ARSpawnObject : System.Object
{
    [SerializeField]
    private string imageName;
    [SerializeField]
    private List<GameObject> prefabs;
    [SerializeField]
    private List<GameManager.Book> books;
    [SerializeField]
    private List<GameManager.Language> languages;
    [SerializeField]
    private bool triggerRepeatedly;
    private bool spawned = false;

    public string ImageName {
        get { return imageName; }
    }

    public bool ShouldSpawn(GameManager.Book targetBook, GameManager.Language targetLanguage) {
        return !spawned && books.Contains(targetBook) && languages.Contains(targetLanguage);
    }

    public GameObject GetPrefab(GameManager.Book currentBook) {
        int index = books.IndexOf(currentBook);
        Debug.Log("currentBook index "+ index);
        return index == -1 ? null : prefabs[index];
    }

    public bool Spawned {
        get { return spawned; }
        set {
            if (triggerRepeatedly) { return; }
            spawned = value;
        }
    }

    public bool TriggerRepeatedly {
        get { return triggerRepeatedly; }
    }
}
