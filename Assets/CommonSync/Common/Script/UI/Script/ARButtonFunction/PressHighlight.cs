using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressHighlight : MonoBehaviour {
    [SerializeField]
    private Transform modelContainer;
    //[SerializeField]
    //private Material[] hightlightMaterial;

    private int brightLevel;
    private bool lightening;
    //private Material[] normalMaterial;

    private void Start() {
        brightLevel = 0;
    }
    private void FixedUpdate() {
        if (lightening) {
            brightLevel++;
            if (brightLevel == 100) { }
        }
    }
    public void Lightening(bool lighteningToggle) {
        this.lightening = lighteningToggle;
        if (lightening) {
            foreach (Transform child in modelContainer) {
                if (child.gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    meshRenderer.material.EnableKeyword("_EMISSION");
                    meshRenderer.material.SetColor("_EmissionColor", Color.yellow);
                    DynamicGI.UpdateEnvironment();
                    //meshRenderer.materials = hightlightMaterial;
                }
                //child.gameObject.GetComponent<MeshRenderer>().materials = hightlightMaterial;
            }
            //Debug.Log("PointerDown");
        }
        if (!lightening) {
            foreach (Transform child in modelContainer) {
                if (child.gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    meshRenderer.material.EnableKeyword("_EMISSION");
                    meshRenderer.material.SetColor("_EmissionColor", new Color(0,0,0));
                    DynamicGI.UpdateEnvironment();
                    //meshRenderer.materials = normalMaterial;
                }
                //child.gameObject.GetComponent<MeshRenderer>().materials = normalMaterial;
            }
            //Debug.Log("PointerUp");
        }
    }
}
