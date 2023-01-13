using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GD.MinMaxSlider;

//[RequireComponent(typeof(BoxCollider))]
public class TouchController : MonoBehaviour {
    [Header("Setting:")]
    [SerializeField]
    [Tooltip("Leave it null if it put under the target")]
    private GameObject tranformTarget;
    [SerializeField]
    [Tooltip("Reset Tranfrom Button")]
    private Button resetTranfromBtn;

    [Header("Rotation Setup:")]
    [SerializeField]
    [Tooltip("Smaller Value faster movement")]
    private float rotationSpeed = 10;

    [Header("Scaling Setup:")]
    [SerializeField]
    private bool enableScaling = true;
    [SerializeField]
    [Range(0.0001f,0.01f), Tooltip("Smaller Value smaller movement")]
    private float scaleSpeed = 0.003f;
    [SerializeField]
    [MinMaxSlider(0,10), Tooltip("Limit the scale")]
    private Vector2 scaleOutRang = new Vector2(0.1f,8f);

    [Header("Panning Setup:")]
    [SerializeField]
    [Tooltip("Pan mode with replace rotation control")]
    private bool panMode;
    [SerializeField]
    [Tooltip("Smaller Value faster movement")]
    private float panSpeed = 500;

    private Quaternion initialRotation;
    private Vector3 initialPosition, initialScale, initialParentPosition;
    private bool isActive;
    private TouchDetector touchDetector;

    //Raycast LayerMask
    [SerializeField]
    private LayerMask mask = ~(1 << 4) | (7 << 0);

    // Start is called before the first frame update
    void Start() {
        if(tranformTarget == null) tranformTarget = gameObject;

        initialRotation = tranformTarget.transform.rotation;
        initialScale = tranformTarget.transform.localScale;
        initialPosition = tranformTarget.transform.position;
        initialParentPosition = tranformTarget.transform.parent.position;

        if(resetTranfromBtn != null) resetTranfromBtn.onClick.AddListener(() => ResetAll());

        touchDetector = GetComponentInChildren<TouchDetector>();

        if(touchDetector != null) return; // End initialization
        tranformTarget.layer = LayerMask.NameToLayer("TouchControl");
        if(!tranformTarget.TryGetComponent<BoxCollider>(out var boxCollider)) tranformTarget.AddComponent<BoxCollider>();
        if(!Camera.main.gameObject.TryGetComponent<PhysicsRaycaster>(out var ray)) Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        Camera.main.gameObject.GetComponent<PhysicsRaycaster>().eventMask = (1 << 5) | (7 << 0);

        //if (!tranformTarget.TryGetComponent<EventTrigger>(out var eventTrigger)) tranformTarget.AddComponent<EventTrigger>();
        //var trigger = tranformTarget.gameObject.GetComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerUp;
        //entry.callback.AddListener(data => { this.DetectSelected(false); });
        //trigger.triggers.Add(entry);
        //EventTrigger.Entry entry2 = new EventTrigger.Entry();
        //entry2.eventID = EventTriggerType.PointerDown;
        //entry2.callback.AddListener((eventData) => { this.DetectSelected(true); });
        //trigger.triggers.Add(entry2);
    }

    public void ResetAll() {
        Debug.Log("initialScale: " + initialScale);
        Debug.Log("initialRotation: " + initialRotation);
        Debug.Log("initialPosition: " + initialPosition);
        Debug.Log("initialParentPosition: " + initialParentPosition);
        Debug.Log("tranformTarget.transform.localScale: " + tranformTarget.transform.localScale);
        Debug.Log("tranformTarget.transform.rotation: " + tranformTarget.transform.rotation);
        Debug.Log("tranformTarget.transform.position: " + tranformTarget.transform.position);
        Debug.Log("tranformTarget.transform.parent.position: " + tranformTarget.transform.parent.position);

        tranformTarget.transform.localScale = initialScale;
        tranformTarget.transform.rotation = initialRotation;
        //tranformTarget.transform.position = initialPosition;

        //tranformTarget.transform.parent.position = initialParentPosition;
    }

    public void DetectSelected(bool isActive) {
        if(touchDetector != null) return;
        this.isActive = isActive;
    }

    // Update is called once per frame
    void Update() {
        if(touchDetector == null)
            TouchRaycast();
        else
            this.isActive = touchDetector.isActive;
        TouchContolAction();
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

    private void TouchContolAction() {
        if(!isActive) return;

        if(Input.touchCount == 1) {
            if(Input.touchCount == 1) {
                Touch screenTouch = Input.GetTouch(0);
                if(panMode) {
                    if(screenTouch.phase == TouchPhase.Moved) {
                        tranformTarget.transform.Translate(new Vector3(screenTouch.deltaPosition.x / panSpeed,screenTouch.deltaPosition.y / panSpeed,0));
                    }
                } else {
                    if(screenTouch.phase == TouchPhase.Moved) {
                        tranformTarget.transform.Rotate(0f,-screenTouch.deltaPosition.x / rotationSpeed,0f);
                    }
                    if(screenTouch.phase == TouchPhase.Ended) {
                    }
                }
            }
        }

        if(!enableScaling) return;

        if(Input.touchCount == 2) {
            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);
            if(touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved) {
                var distance = Vector3.Distance(touch0.position,touch1.position);

                var touchZeroPrevPos = touch0.position - touch0.deltaPosition;
                var touchOnePrevPos = touch1.position - touch1.deltaPosition;

                var prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                var currentMagnitude = (touch0.position - touch1.position).magnitude;

                var difference = currentMagnitude - prevMagnitude;
                Scaling(difference * scaleSpeed);
            }
        }
        void Scaling(float increment) {
            var scale = Mathf.Clamp(tranformTarget.transform.localScale.x + increment,scaleOutRang.x,scaleOutRang.y);
            tranformTarget.transform.localScale = new Vector3(scale,scale,scale);
        }
    }
}
