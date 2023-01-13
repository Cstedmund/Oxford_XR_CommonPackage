using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HumanBodyTracker : MonoBehaviour {
    //private static HumanBodyTracker instance;
    //public static HumanBodyTracker Instance {
    //    get { return instance; }
    //}

    private EthnicDatabase ethnicDatabase;

    private void Awake() {
        //if (instance != null && instance != this) {
        //    Destroy(this.gameObject);
        //} else {
        //    instance = this;
        //}
        ethnicDatabase = FindObjectOfType<EthnicDatabase>();
    }

    [SerializeField]
    [Tooltip("The Skeleton prefab to be controlled.")]
    private GameObject m_SkeletonPrefab;
    public GameObject skeletonPrefab {
        get { return m_SkeletonPrefab; }
        set { m_SkeletonPrefab = value; }
    }

    [SerializeField]
    [Tooltip("The ARHumanBodyManager which will produce body tracking events.")]
    private ARHumanBodyManager m_HumanBodyManager;
    public ARHumanBodyManager humanBodyManager {
        get { return m_HumanBodyManager; }
        set { m_HumanBodyManager = value; }
    }

    private Dictionary<TrackableId,BoneController> m_SkeletonTracker = new Dictionary<TrackableId,BoneController>();
    private Dictionary<TrackableId,ARHumanBody> m_BodyTracker = new Dictionary<TrackableId,ARHumanBody>();

    private bool _inital = true;

    private void OnEnable() {
        Debug.Assert(m_HumanBodyManager != null,"Human body manager is required.");
        m_HumanBodyManager.humanBodiesChanged += OnHumanBodiesChanged;
    }

    private void OnDisable() {
        if(m_HumanBodyManager != null)
            m_HumanBodyManager.humanBodiesChanged -= OnHumanBodiesChanged;
    }

    private void OnHumanBodiesChanged(ARHumanBodiesChangedEventArgs eventArgs) {
        BoneController boneController;
        ARHumanBody _humanBody = new ARHumanBody();

        //Debug.Log("eventArgs.added: " + eventArgs.added.Count + "||eventArgs.Update: "+ eventArgs.updated.Count+ "||eventArgs.Removed: "+ eventArgs.removed.Count);

        foreach(var humanBody in eventArgs.added) {
            _humanBody = humanBody;
            if(!m_SkeletonTracker.TryGetValue(humanBody.trackableId,out boneController)) {
                Debug.Log($"Adding a new skeleton [{humanBody.trackableId}].");
                var newSkeletonGO = Instantiate(ethnicDatabase.SelectedEthnicGroup.ModelPrefab == null ? m_SkeletonPrefab : ethnicDatabase.SelectedEthnicGroup.ModelPrefab,humanBody.transform);
                boneController = newSkeletonGO.GetComponent<BoneController>();

                m_SkeletonTracker.Add(humanBody.trackableId,boneController);
                m_BodyTracker.Add(humanBody.trackableId,humanBody);
            }

            SetUpBoneController(boneController,humanBody);
        }

        foreach(var humanBody in eventArgs.updated) {
            _humanBody = humanBody;
            if(_inital) {
                if(!m_SkeletonTracker.TryGetValue(humanBody.trackableId,out boneController)) {
                    Debug.Log($"Adding a new skeleton [{humanBody.trackableId}].");
                    var newSkeletonGO = Instantiate(ethnicDatabase.SelectedEthnicGroup.ModelPrefab == null ? m_SkeletonPrefab : ethnicDatabase.SelectedEthnicGroup.ModelPrefab,humanBody.transform);
                    boneController = newSkeletonGO.GetComponent<BoneController>();

                    m_SkeletonTracker.Add(humanBody.trackableId,boneController);
                    m_BodyTracker.Add(humanBody.trackableId,humanBody);
                }
                SetUpBoneController(boneController,humanBody);
                _inital = false;
            }

            if(m_SkeletonTracker.TryGetValue(humanBody.trackableId,out boneController)) {
                boneController.ApplyBodyPose(humanBody);
            }
        }

        foreach(var humanBody in eventArgs.removed) {
            _humanBody = humanBody;
            Debug.Log($"Removing a skeleton [{humanBody.trackableId}].");
            if(m_SkeletonTracker.TryGetValue(humanBody.trackableId,out boneController)) {
                Destroy(boneController.gameObject);
                m_SkeletonTracker.Remove(humanBody.trackableId);
                m_BodyTracker.Remove(humanBody.trackableId);
            }
        }
        //Debug.Log("Tracking Status: "+ _humanBody.trackingState);

        //if (_humanBody.trackingState != TrackingState.Tracking) {
        //    if (_humanBody != null) {
        //        Debug.Log("Human Tracking Lost and Destory Human Body Skeleton Mesh" + _humanBody.trackingState);
        //        ClearHumanBoddyPrefab();
        //    }
        //}
    }

    public void ClearHumanBoddyPrefab() {
        Debug.Log("ClearHumanBoddyPrefab");
        Debug.Log("m_SkeletonTracker.Keys " + m_SkeletonTracker.Keys.Count);
        foreach(TrackableId trackableId in m_SkeletonTracker.Keys) {
            if(m_BodyTracker.ContainsKey(trackableId)) {
                ARHumanBody humanBody = m_BodyTracker[trackableId];
                if(m_SkeletonTracker[trackableId].gameObject != null) {
                    Destroy(m_SkeletonTracker[trackableId].gameObject);
                    m_SkeletonTracker.Remove(humanBody.trackableId);
                    m_BodyTracker.Remove(humanBody.trackableId);
                }
            }
        }
        _inital = true;
    }

    public void ChangeModel() {
        Dictionary<TrackableId,BoneController> tempTracker = new Dictionary<TrackableId,BoneController>();
        foreach(TrackableId trackableId in m_SkeletonTracker.Keys) {
            if(m_BodyTracker.ContainsKey(trackableId)) {
                ARHumanBody humanBody = m_BodyTracker[trackableId];

                GameObject newSkeletonGO = Instantiate(ethnicDatabase.SelectedEthnicGroup.ModelPrefab == null ?
                    m_SkeletonPrefab : ethnicDatabase.SelectedEthnicGroup.ModelPrefab,humanBody.transform);
                BoneController boneController = newSkeletonGO.GetComponent<BoneController>();
                SetUpBoneController(boneController,humanBody);

                Destroy(m_SkeletonTracker[trackableId].gameObject);
                tempTracker.Add(trackableId,boneController);
            }
        }

        foreach(TrackableId trackableId in tempTracker.Keys) {
            m_SkeletonTracker[trackableId] = tempTracker[trackableId];
        }
    }

    private void SetUpBoneController(BoneController boneController,ARHumanBody humanBody) {
        boneController.InitializeSkeletonJoints();
        boneController.ApplyBodyPose(humanBody);
        boneController.ChangeTexture(ethnicDatabase.SelectedEthnicGroup.ClothTexture);
    }
}
