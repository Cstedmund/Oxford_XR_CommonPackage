using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUpAnim : MonoBehaviour {
    [SerializeField]
    protected GameObject board;
    [SerializeField]
    protected float timespend = 0.5f;
    [SerializeField]
    protected LeanTweenType easeType = LeanTweenType.easeInOutBack;
    [SerializeField]
    protected bool startupOpen = false;

    protected bool isOpen = false;
    protected Vector3 oriScale;

    protected virtual void Awake() {
        if(board == null) {
            board = gameObject;
        }
    }

    protected virtual void Start() {
        oriScale = board.transform.localScale;
        board.transform.localScale = Vector3.zero;
        if(startupOpen) SwitchBoard(true);
    }

    public virtual void ToggleBoard() {
        if(isOpen) {
            LeanTween.scale(board,Vector3.zero,timespend).setEase(easeType);
        } else {
            LeanTween.scale(board,oriScale,timespend).setEase(easeType);
        }
        isOpen = !isOpen;
    }
    public virtual void SwitchBoard(bool option) {
        if(board == null) return;
        if(option == false) {
            LeanTween.scale(board,Vector3.zero,timespend).setEase(easeType);
            isOpen = false;
        } else {
            LeanTween.scale(board,oriScale,timespend).setEase(easeType);
            isOpen = true;
        }
    }

}
