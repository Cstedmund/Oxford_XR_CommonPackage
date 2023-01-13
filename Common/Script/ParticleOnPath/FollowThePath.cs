using UnityEngine;
using System.Collections.Generic;

public class FollowThePath : MonoBehaviour {

    // Walk speed that can be set in Inspector

    [HideInInspector]
    public Transform m_PathParent;
    [HideInInspector]
    //public float m_moveSpeed;
    private float originalScal = 0f;
    [HideInInspector]
    public ParticlePathManager pathManager;

    // Array of waypoints to walk from one to the next one
    private List<Transform> _waypoints = new List<Transform>();

    // Index of current waypoint from which Enemy walks
    // to the next one
    [HideInInspector]
    public int _waypointIndex = 0;
    [HideInInspector]
    public bool instanceSpwan = false;

    // Use this for initialization
    private void Start() {
        foreach(Transform child in m_PathParent) {
            _waypoints.Add(child);
        }
        // Set position of Enemy as position of the first waypoint
        if(instanceSpwan == false)
            transform.position = _waypoints[_waypointIndex].transform.position;
    }

    // Update is called once per frame
    private void Update() {
        // Move Enemy
        Move();
    }

    // Method that actually make Enemy walk
    private void Move() {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if(_waypointIndex <= _waypoints.Count - 1) {
            // Move Enemy from current waypoint to the next one using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
                _waypoints[_waypointIndex].transform.position,
                pathManager.m_readonlySpeed * Time.deltaTime);

            transform.LookAt(_waypoints[_waypointIndex]);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if(transform.position == _waypoints[_waypointIndex].transform.position) {
                _waypointIndex += 1;
            }
        } else {
            Destroy(gameObject);
        }
    }
}
