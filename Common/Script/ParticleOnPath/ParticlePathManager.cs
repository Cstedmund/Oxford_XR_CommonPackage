using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePathManager : MonoBehaviour {
    [SerializeField]
    private GameObject m_ParticleToSpawn;
    [SerializeField]
    private float m_IntervalInSec, m_MovingSpeed;// m_ParticleScale;
    [SerializeField]
    private bool distrubuteBegin = false;
    // Start is called before the first frame update
    //private float originalScal = 0f;
    [HideInInspector]
    public float m_readonlySpeed;

    private void Start() {
        //originalScal = transform.root.GetChild(0).localScale.x;
    }

    private void OnEnable() {
        if(transform.parent.parent.parent != null)
            Debug.Log(transform.parent.parent.parent.name + transform.parent.parent.parent.localScale.x);
        //float distance = Vector3.Distance(transform.GetChild(0).position,transform.GetChild(1).transform.position);

        try {
            m_ParticleToSpawn.GetComponent<FollowThePath>().m_PathParent = transform;
            m_ParticleToSpawn.GetComponent<FollowThePath>().pathManager = this;
            //m_ParticleToSpawn.transform.localScale = Vector3.one * m_ParticleScale;

            _waypoints.Clear();
            foreach(Transform child in transform) {
                _waypoints.Add(child);
            }
            _waypointIndex = 1;
            if(distrubuteBegin)
                StartCoroutine(StartPawnAllPartile());
            StartCoroutine(StartSpawnPartile());
        } catch {
            Debug.LogError("ParticleToSpawn is require FollowThePath component");
            throw;
        }
    }

    List<Transform> _waypoints = new List<Transform>();
    int _waypointIndex = 1;
    private readonly Quaternion particleSpawnOri = new Quaternion(0,0,0,0);
    IEnumerator StartPawnAllPartile() {
        while(_waypointIndex <= _waypoints.Count - 1) {
            var prewaypointPos = _waypoints[_waypointIndex - 1].transform.position;
            var currentwatpointPos = _waypoints[_waypointIndex].transform.position;
            float distanceInterval = m_MovingSpeed * m_IntervalInSec;
            int res = (int)(Vector3.Distance(prewaypointPos,currentwatpointPos) / distanceInterval);
            if(res > 0) {
                for(int i = 0; i < res; i++) {
                    float idx = i; //never use i drectly!!!!
                    //Debug.Log(transform.name + _waypointIndex + " One Parti");
                    var pos = Vector3.Lerp(prewaypointPos,currentwatpointPos,(float)(idx / res));
                    //Debug.Log($"{i}|{pos}|{(float)(idx / res)}|{Vector3.Distance(prewaypointPos,currentwatpointPos)}");
                    var particle = Instantiate(m_ParticleToSpawn,pos,particleSpawnOri,transform.parent);
                    particle.GetComponent<FollowThePath>()._waypointIndex = _waypointIndex;
                    particle.GetComponent<FollowThePath>().instanceSpwan = true;
                    particle.transform.LookAt(_waypoints[_waypointIndex]);
                    //Debug.Log("particle pos: " + particle.transform.position);
                }
            } else {
                //Debug.Log(transform.name + _waypointIndex + " Mid Parti");
                var pos = Vector3.Lerp(prewaypointPos,currentwatpointPos,0.5f);
                var particle = Instantiate(m_ParticleToSpawn,pos,particleSpawnOri,transform.parent);
                particle.GetComponent<FollowThePath>()._waypointIndex = _waypointIndex;
                particle.GetComponent<FollowThePath>().instanceSpwan = true;
                particle.transform.LookAt(_waypoints[_waypointIndex]);
            }
            _waypointIndex += 1;
            yield return null;
        }
    }

    IEnumerator StartSpawnPartile() {
        while(true) {
            var ins = Instantiate(m_ParticleToSpawn,transform.GetChild(0).position,transform.GetChild(0).rotation,transform.parent);
            yield return new WaitForSeconds(m_IntervalInSec);
        }
    }

    // Update is called once per frame
    private void Update() {
#if UNITY_EDITOR
        m_readonlySpeed = m_MovingSpeed * transform.root.GetChild(0).localScale.x;//(Mathf.Pow(1.3f,((transform.root.GetChild(0).localScale.x - originalScal) + 1)) - 1.2f);
                                                                                  //m_ParticleToSpawn.GetComponent<FollowThePath>().m_moveSpeed = m_readonlySpeed;
#else
        m_readonlySpeed = m_MovingSpeed * transform.root.GetChild(0).localScale.x;
        //m_ParticleToSpawn.GetComponent<FollowThePath>().m_moveSpeed = m_readonlySpeed;
#endif
    }
}
