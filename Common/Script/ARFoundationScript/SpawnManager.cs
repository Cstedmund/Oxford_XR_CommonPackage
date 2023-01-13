using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //private static SpawnManager _instance;

    //public static SpawnManager Instance {
    //    get { return _instance; }
    //}

    //private void Awake() {
    //    if (_instance != null && _instance != this) {
    //        Destroy(gameObject);
    //    } else {
    //        _instance = this;
    //    }s
    //}

    [SerializeField]
    private ARSpawnObject[] ARSpawnObjects;

    public Dictionary<string, GameObject> spawnedObjs;
    public Dictionary<string, GameObject> spawnedRepeat;

    private Dictionary<string, ARSpawnObject> spawnObjDict;
    //private int tempIndex = 0;

    private GameManager gameManager;

    private void Start() {
        spawnedObjs = new Dictionary<string, GameObject>();
        spawnedRepeat = new Dictionary<string, GameObject>();
        spawnObjDict = new Dictionary<string, ARSpawnObject>();
        foreach (ARSpawnObject aRSpawnObject in ARSpawnObjects) {
            spawnObjDict.Add(aRSpawnObject.ImageName, aRSpawnObject);
        }
        gameManager = GameManager.Instance;
    }

    public void AddedTrackedImage(string objName, Transform parent) {
        if (!spawnObjDict.ContainsKey(objName)) {
            SpawnNormaPrefab(objName, parent);
        } else {
            SpawnOtherObject(objName, parent);
        }
    }

    private void SpawnNormaPrefab(string objName, Transform parent) {
        GameObject normaPrefab = Resources.Load<GameObject>("Content/MainApps/Prefab/" + objName);

        if (normaPrefab == null) { return; }

        GameObject normaPb = Instantiate(normaPrefab, parent);
        normaPb.name = objName;
        spawnedObjs.Add(objName, normaPb);
    }

    private void SpawnOtherObject(string objName, Transform parent) {
        ARSpawnObject objToSpawn = spawnObjDict[objName];

        if (!objToSpawn.ShouldSpawn(gameManager.currentBook.bookEnum, gameManager.currentLanguage)) { return; }

        GameObject prefab = objToSpawn.GetPrefab(gameManager.currentBook.bookEnum);
        if (!prefab || GameObject.Find(prefab.name) != null) { return; }

        GameObject newObject = Instantiate(prefab, parent);
        //newObject.name = objName;
        objToSpawn.Spawned = true;

        if (objToSpawn.TriggerRepeatedly) {
            spawnedRepeat.Add(objName, newObject);
        } else {
            spawnedObjs.Add(objName, newObject);
        }
    }

    public bool IsObjectSpawned(string objName) {
        return spawnedObjs.ContainsKey(objName) || spawnedRepeat.ContainsKey(objName);
    }

    public void Remove(string objName) {
        if (!spawnedObjs.ContainsKey(objName)) { return; }
        Destroy(spawnedObjs[objName]);

        spawnedObjs.Remove(objName);
    }

    public void RemovePreviousBookSpawnedPrefab() {
        foreach (KeyValuePair<string, GameObject> spawnedObj in spawnedRepeat) {
            Destroy(spawnedRepeat[spawnedObj.Key]);
        }
        spawnedRepeat.Clear();
    }

    public void RemoveAll() {
        foreach (KeyValuePair<string, GameObject> spawnedObj in spawnedObjs) {
            Destroy(spawnedObjs[spawnedObj.Key]);
        }
        spawnedObjs.Clear();
    }
}
