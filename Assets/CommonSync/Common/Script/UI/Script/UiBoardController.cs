using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiBoardController : MonoBehaviour {

    protected Animator _animator;
    protected bool _open;

    protected virtual void Start() {
        _animator = GetComponent<Animator>();
        _animator.Play("3DUIBoardOpen_Reversed");
        _open = false;
    }

    protected virtual void Update() {

    }

    public virtual void ToggleBoard() {
        if(!_open) {
            _animator.Play("3DUIBoardOpen");
            _open = true;
        } else {
            _animator.Play("3DUIBoardOpen_Reversed");
            _open = false;
        }
        Debug.Log(transform.parent.name + " ToggleBoard " + _open);
    }

    public virtual void FinishedOpenAnimation() {

    }

    public virtual void ToogleBoardSwitch(bool m_open) {
        if(_animator == null) return;
        if(m_open) {
            _animator.Play("3DUIBoardOpen");
            _open = true;
        } else {
            _animator.Play("3DUIBoardOpen_Reversed");
            _open = false;
        }
        //Debug.Log(transform.parent.name + " ToogleBoardSwitch " + _open);
    }
}
