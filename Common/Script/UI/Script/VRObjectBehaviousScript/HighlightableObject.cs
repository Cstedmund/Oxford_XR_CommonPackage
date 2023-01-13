using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class HighlightableObject : MonoBehaviour {
    private Outline outline;

    protected virtual void Awake() {
        outline = GetComponent<Outline>();
    }

    protected virtual IEnumerator Start() {
        yield return new WaitForSeconds(1f);
        outline.enabled = false;
    }

    // Start is called before the first frame update
    protected virtual void OnPointerEnter() {
        outline.enabled = true;
    }

    protected virtual void OnPointerClick() {
    }

    protected virtual void OnPointerExit() {
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }
}
