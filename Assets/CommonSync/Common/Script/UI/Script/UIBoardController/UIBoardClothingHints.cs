using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoardClothingHints : UiBoardController {
    // Start is called before the first frame update

    [SerializeField]
    private GameObject bodyHints;
    private bool isAnimationPlaying = false;

    protected override void Start() {
        _animator = GetComponent<Animator>();
        transform.localScale = new Vector3(0, 0, 0);
        _open = false;
        ToggleBoard();
        StartCoroutine(HintsTimmer());
    }

    public void OpenHints() {
        Debug.Log("OpenHints");
        ToggleBoard();
        Debug.Log(_open);
        if (!_open) {
            bodyHints.GetComponent<Animator>().Play("FlashLightIdle");
            bodyHints.SetActive(false);
            return;
        }
        if (isAnimationPlaying) return;
        StartCoroutine(HintsTimmer());
    }

    private IEnumerator HintsTimmer() {
        Debug.Log("HintsTimmer");
        isAnimationPlaying = true;
        bodyHints.SetActive(true);
        bodyHints.GetComponent<Animator>().Play("UIFlickering");
        yield return new WaitForSeconds(5f);
        ToogleBoardSwitch(false);
        bodyHints.SetActive(false);
        isAnimationPlaying = false;
    }

    private void OnEnable() {
        ToogleBoardSwitch(false);
        if (bodyHints.active) {
            bodyHints.GetComponent<Animator>().Play("FlashLightIdle");
            bodyHints.SetActive(false);
        }
    }

    private void OnDisable() {
        isAnimationPlaying = false;
        if (bodyHints.active) {
            bodyHints.GetComponent<Animator>().Play("FlashLightIdle");
            bodyHints.SetActive(false);
        }
    }
}
