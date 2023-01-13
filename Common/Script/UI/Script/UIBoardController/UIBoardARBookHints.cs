using System.Collections;
using UnityEngine;

public class UIBoardARBookHints : UiBoardController {

    [SerializeField]
    private GameObject aRHints;
    private void Awake() {
        if (aRHints == null) aRHints = gameObject;
    }

    protected override void Start() {
        _animator = GetComponent<Animator>();
        transform.localScale = new Vector3(0, 0, 0);
        _open = false;
        ToggleBoard();
        StartCoroutine(HintsTimmer());
    }

    private IEnumerator HintsTimmer() {
        Debug.Log("HintsTimmer");
        aRHints.SetActive(true);
        aRHints.GetComponent<Animator>().Play("CircleCountDown");
        yield return new WaitForSeconds(3f);
        ToogleBoardSwitch(false);
    }

    private void OnEnable() {
        ToogleBoardSwitch(false);
        if (aRHints.active) {
            aRHints.GetComponent<Animator>().Play("CircleCountDownIdle");
            aRHints.SetActive(false);
        }
    }

    private void OnDisable() {
        if (aRHints.active) {
            aRHints.GetComponent<Animator>().Play("CircleCountDownIdle");
            aRHints.SetActive(false);
        }
    }

}
