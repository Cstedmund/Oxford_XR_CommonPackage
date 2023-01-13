using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCallFunc : MonoBehaviour {
    [System.Serializable]
    public class CollisionEvent : UnityEvent<object> {
        public object value;
    }
    [SerializeField]
    [TagSelector]
    public string tagFilter = "";

    [SerializeField]
    private CollisionEvent collisionEvents = new();

    protected virtual void OnCollisionEnter(Collision collision) {
        CallFunction(collision.gameObject);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        CallFunction(collision.gameObject);
    }
    protected virtual void OnTriggerEnter(Collider collision) {
        CallFunction(collision.gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        CallFunction(collision.gameObject);
    }

    protected virtual void CallFunction(GameObject collision) {
        if (tagFilter != "") {
            if (collision.gameObject.tag != tagFilter) return;
        }
        try {
            collisionEvents.Invoke(collisionEvents.value);
        } catch (System.Exception exception) {
            Debug.LogWarning("Couldn't invoke action. Error:");
            Debug.LogWarning(exception.Message);
        }
    }
}
