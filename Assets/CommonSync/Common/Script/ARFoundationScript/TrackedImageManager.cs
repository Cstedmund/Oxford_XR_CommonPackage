using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageManager : MonoBehaviour
{
    //private static TrackedImageManager _instance;

    //public static TrackedImageManager Instance {
    //    get { return _instance; }
    //}

    [HideInInspector]
    public string currentImageName;

    private SpawnManager spawnManager;
    private ARTrackedImageManager m_TrackedImageManager;
    private GameManager gameManager;

    private void Awake() {
        //if (_instance != null && _instance != this) {
        //    Destroy(gameObject);
        //} else {
        //    _instance = this;
        //}

        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        gameManager = GameManager.Instance;
    }

    private List<GameObject> activeObjects;
    //private List<string> activeObjectName;

    private void Start() {
        activeObjects = new List<GameObject>();
        spawnManager = FindObjectOfType<SpawnManager>();
        //activeObjectName = new List<string>();
        //if (gameManager.currentLanguage == GameManager.Language.en) {
        //    m_TrackedImageManager.referenceLibrary = gameManager.currentBook.aRImageLibrary;
        //} else {
        //    m_TrackedImageManager.referenceLibrary = gameManager.currentBook.aRImageLibraryC;
        //}
    }

    //public void UpdateARLibrary() {
    //    if (gameManager.currentLanguage == GameManager.Language.en) {
    //        m_TrackedImageManager.referenceLibrary = gameManager.currentBook.aRImageLibrary;
    //    } else {
    //        m_TrackedImageManager.referenceLibrary = gameManager.currentBook.aRImageLibraryC;
    //    }
    //}

    private void OnEnable() {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable() {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) {
        foreach (var trackedImage in eventArgs.added) {
            spawnManager.AddedTrackedImage(trackedImage.referenceImage.name, trackedImage.transform);
            UpdateInfo(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated) {
            if (!spawnManager.IsObjectSpawned(trackedImage.referenceImage.name) && trackedImage.trackingState == TrackingState.Tracking) {
                spawnManager.AddedTrackedImage(trackedImage.referenceImage.name, trackedImage.transform);
            }
            UpdateInfo(trackedImage);
        }
    }
    private void UpdateInfo(ARTrackedImage trackedImage) {
        //if (trackedImage.trackingState == TrackingState.Tracking) {
        //    localPlayer.ShowClueText(trackedImage.referenceImage.name);
        //} else {
        //    if (localPlayer.previousClueItem == trackedImage.referenceImage.name) {
        //        localPlayer.ShowClueText(string.Empty);
        //    }
        //}
        currentImageName = trackedImage.referenceImage.name;
        if (trackedImage.transform.childCount <= 0) { return; }
        if (!spawnManager.IsObjectSpawned(trackedImage.referenceImage.name)) { return; }

        if (trackedImage.trackingState == TrackingState.Tracking) {
            //if (activeObjectName.Contains(trackedImage.referenceImage.name)) {
            //    return;
            //} activeObjectName.Add(trackedImage.referenceImage.name);

            if (!activeObjects.Contains(trackedImage.transform.GetChild(0).gameObject)) {
                activeObjects.Add(trackedImage.transform.GetChild(0).gameObject);
            }

            trackedImage.transform.GetChild(0).gameObject.SetActive(true);
            float size = Math.Min(trackedImage.size.x, trackedImage.size.y);
            trackedImage.transform.localScale = new Vector3(size, size, size);
        } else {
            //if (activeObjectName.Contains(trackedImage.referenceImage.name)) {
            //    activeObjectName.Remove(trackedImage.referenceImage.name);
            //}

            if (activeObjects.Contains(trackedImage.transform.GetChild(0).gameObject)) {
                activeObjects.Remove(trackedImage.transform.GetChild(0).gameObject);
            }
            trackedImage.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
