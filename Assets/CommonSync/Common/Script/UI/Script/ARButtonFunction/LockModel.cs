using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(Button))]
public class LockModel : MonoBehaviour {
    private TrackedImageManager trackedImageManager;
    private ARTrackedImageManager aRTrackedImageManager;
    private UIBoardLockModeHints hintBoard;
    [SerializeField]
    private bool lockModeButStillTackImage = false;
    [SerializeField]
    private GameObject parent;
    private bool trigger;

    void Start() {
        trigger = false;
        GetComponent<Button>().onClick.AddListener(delegate { LockARModel(); });
        hintBoard = GetComponentInChildren<UIBoardLockModeHints>();
    }

    private void OnEnable() {
        //GetComponent<Button>().interactable = false;
        //internalLockARmodel(true);
        //StartCoroutine(delateUnlock());
    }

    private IEnumerator delateUnlock() {
        yield return new WaitForSeconds(2f);
        //GetComponent<Button>().interactable = true;
        internalLockARmodel(false);
        Debug.Log("Timeup internal lock ar model == false");
    }

    private void internalLockARmodel(bool setLock) {
        Debug.Log("Internal lock ar model == " + setLock);
        trackedImageManager = FindObjectOfType<TrackedImageManager>();
        if (trackedImageManager == null) {
            Debug.Log("Cannot find TrackedImageManager");
            return;
        }
        if (setLock) {
            //Lock Model
            trackedImageManager.enabled = false;
        } else {
            //Unlock Model
            trackedImageManager.enabled = true;
        }
    }

    public void LockARModel() {
        Debug.Log("LockARModel == " + trigger);
        if (hintBoard.firstLoad) {
            hintBoard.CloseHints();
            Debug.Log("Close Hints");
        }
        StopCoroutine(delateUnlock());
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
            trackedImageManager.enabled = false;
            if (aRTrackedImageManager != null && !lockModeButStillTackImage)
                aRTrackedImageManager.enabled = false;
            //transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = null? "Unlock" : "";
        } else {
            trackedImageManager.enabled = true;
            aRTrackedImageManager.enabled = true;
            //Destroy(parent);
            parent.SetActive(false);
            //transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Lock";
        }
        trigger = !trigger;
    }
}
