using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HideShowLabel : MonoBehaviour {
    [SerializeField]
    protected Transform _LabelContainer;
    [SerializeField]
    protected List<GameObject> _GameObjectToHide = new List<GameObject>();
    protected bool labelshowing = true;

    virtual protected void Start() {
        GetComponent<Button>().onClick.AddListener(delegate { ToggleLabel(); });
    }

    virtual public void ToggleLabel() {
        if(labelshowing) {
            if(_LabelContainer != null) {
                foreach(Transform tran in _LabelContainer) {
                    if(tran.gameObject != null && tran.gameObject.active) {
                        tran.gameObject.SetActive(false);
                    }
                }
            } else if(_GameObjectToHide.Count != 0) {
                foreach(var obj in _GameObjectToHide) {
                    obj.SetActive(false);
                }
            }
        } else {
            if(_LabelContainer != null) {
                foreach(Transform tran in _LabelContainer) {
                    if(tran.gameObject != null && tran.gameObject.active) {
                        tran.gameObject.SetActive(true);
                    }
                }
            } else if(_GameObjectToHide.Count != 0) {
                foreach(var obj in _GameObjectToHide) {
                    obj.SetActive(true);
                }
            }
        }
        labelshowing = !labelshowing;
    }
}
