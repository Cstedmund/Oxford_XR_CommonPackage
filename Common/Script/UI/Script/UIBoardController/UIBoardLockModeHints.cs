using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIBoardLockModeHints : UiBoardController
{
    [HideInInspector]
    public bool firstLoad = true;

    // Start is called before the first frame update
    protected override void Start() {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnEnable() {
        _animator = gameObject.GetComponent<Animator>();
        if (firstLoad) {
            _animator.Play("3DUIBoardOpen");
            _open = true;
        }
    }

    public void CloseHints() {
        _animator = gameObject.GetComponent<Animator>();
        _open = false;
        _animator.Play("3DUIBoardOpen_Reversed");
    }

    public override void FinishedOpenAnimation() {
        base.FinishedOpenAnimation();
        if(_open == false)
            firstLoad = false;
    }
}
