using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetector : MonoBehaviour {
    [SerializeField]
    [Tooltip("Leave it null if it put under the target")]
    private GameObject detectionTarget;
    [HideInInspector]
    public bool isActive;
    //Raycast LayerMask
    [SerializeField]
    private LayerMask mask = ~(1 << 4) | (7 << 0);

    private void Start() {
        if(detectionTarget == null) detectionTarget = gameObject;

        detectionTarget.layer = LayerMask.NameToLayer("TouchControl");
        if(!detectionTarget.TryGetComponent<MeshCollider>(out var meshCollider)) detectionTarget.AddComponent<BoxCollider>();
        if(!Camera.main.gameObject.TryGetComponent<PhysicsRaycaster>(out var ray)) Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        Camera.main.gameObject.GetComponent<PhysicsRaycaster>().eventMask = (1 << 5) | (7 << 0);
    }
    // Update is called once per frame
    void Update() {
        TouchRaycast();
    }

    private void TouchRaycast() {
        if(Input.touchCount == 0) { DetectSelected(false); return; };

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hitInfo;

        if(EventSystem.current.IsPointerOverGameObject()) return;
        if(Physics.Raycast(ray,out hitInfo,Mathf.Infinity,mask)) {
            Debug.DrawLine(ray.origin,hitInfo.point,Color.green);
            if(isActive) return;
            DetectSelected(true);
        } //else {
        //    if(!isActive) return;
        //    DetectSelected(false);
        //}
    }

    public void DetectSelected(bool isActive) {
        this.isActive = isActive;
    }
}
