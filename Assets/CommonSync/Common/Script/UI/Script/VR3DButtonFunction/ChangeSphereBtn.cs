using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeSphereBtn : ClickableObject
{
    [SerializeField]
    private Transform _Destination;

    protected override void OnPointerClick() {
        _Destination.gameObject.active = true;
        Camera.main.GetComponent<VRFader>().ChangeSphere(_Destination,()=> {CallBack();});
    }
    protected void CallBack() {
        transform.parent.gameObject.active = false;
    }
}
