using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ChangeARView : MonoBehaviour {
    private TrackedImageManager trackedImageManager;
    private ARTrackedImageManager aRTrackedImageManager;
    private bool trigger;
    [SerializeField]
    private GameObject parent, spawnObj, uiNeedHide;
    [SerializeField]
    private Vector3 spawnModelScale = new Vector3(1,1,1);
    [SerializeField]
    private Vector3 transOffset = new Vector3(0,0,0);

    private GameObject spawnedObj;
    //Start is called before the first frame update
    void Start() {
        trigger = false;
        GetComponent<Button>().onClick.AddListener(delegate { ChangeARViewMode(); });
    }

    // Update is called once per frame
    void Update() {

    }

    public void ChangeARViewMode() {
        trackedImageManager = FindObjectOfType<TrackedImageManager>();
        aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        if (trackedImageManager == null) {
            Debug.Log("Cannot find TrackedImageManager");
            return;
        }
        if (aRTrackedImageManager == null) {
            Debug.Log("Cannot find ARTrackedImageManager");
        }

        if (!trigger) {
            spawnedObj = Instantiate(spawnObj,new Vector3(transOffset.x,transOffset.y,transOffset.z), Quaternion.Euler(0,0,0),parent.transform);
            spawnedObj.transform.localScale = spawnModelScale;
            spawnObj.SetActive(false);
            trackedImageManager.enabled = false;
            aRTrackedImageManager.enabled = false;
            uiNeedHide.active = false;
        } else {
            Destroy(spawnedObj);
            spawnObj.SetActive(true);
            uiNeedHide.active = true;
            trackedImageManager.enabled = true;
            aRTrackedImageManager.enabled = true;
        }
        trigger = !trigger;
    }
}
