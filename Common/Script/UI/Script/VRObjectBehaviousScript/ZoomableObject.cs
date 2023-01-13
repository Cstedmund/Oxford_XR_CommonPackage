using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomableObject : MonoBehaviour {
    [SerializeField]
    private string _animaitonName;
    private GameObject _CMStateDrivenCamera;
    private bool _triggerOn = true;

    private void Start() {
        _CMStateDrivenCamera = GameObject.Find("CM StateDrivenCamera");
    }

    protected virtual void OnPointerClick() {
        if (_animaitonName == "") {
            Debug.Log("_AnimaitonName Field == null");
            return;
        }

        if(_triggerOn)
            _CMStateDrivenCamera.GetComponent<Animator>().Play(_animaitonName);
        else
            _CMStateDrivenCamera.GetComponent<Animator>().Play("MainCamera");

        _triggerOn = !_triggerOn;
    }
}
