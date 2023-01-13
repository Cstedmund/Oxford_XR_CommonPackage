using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PressHighlight3 : MonoBehaviour {
    [SerializeField]
    [Tooltip("Leave null to reference self")]
    private Transform modelContainer;
    [SerializeField]
    private Color highlightColor = Color.yellow;

    private int brightLevel;
    private bool lightening;
    private Color oriEmColor;
    private List<Color> oriChildEmColor = new List<Color>();

    private void Start() {
        if(modelContainer == null) modelContainer = transform;
        brightLevel = 0;

        if(!TryGetComponent<EventTrigger>(out var triggerTemp)) gameObject.AddComponent<EventTrigger>();
        var trigger = gameObject.GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener(data => { this.Lightening(false); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerDown;
        entry2.callback.AddListener((eventData) => { this.Lightening(true); });
        trigger.triggers.Add(entry2);

        if (modelContainer.gameObject.TryGetComponent<MeshRenderer>(out var meshRendererParent)) {
            oriEmColor = meshRendererParent.material.GetColor("_EmissionColor");
            meshRendererParent.material.EnableKeyword("_EMISSION");
        }
        foreach (Transform child in modelContainer) {
            if (child.gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                oriChildEmColor.Add(meshRenderer.material.GetColor("_EmissionColor"));
                meshRenderer.material.EnableKeyword("_EMISSION");
            }
        }
    }
    private void FixedUpdate() {
        if(lightening) {
            brightLevel++;
            if(brightLevel == 100) { }
        }
    }

    public void Lightening(bool lighteningToggle) {
        this.lightening = lighteningToggle;
        if (lightening) {
            if (modelContainer.gameObject.TryGetComponent<MeshRenderer>(out var meshRendererParent)) {
                meshRendererParent.material.SetColor("_EmissionColor", highlightColor);
                meshRendererParent.material.EnableKeyword("_EMISSION");
                DynamicGI.UpdateEnvironment();
            }
            foreach (Transform child in modelContainer) {
                if (child.gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    meshRenderer.material.SetColor("_EmissionColor", highlightColor);
                    meshRenderer.material.EnableKeyword("_EMISSION");
                    DynamicGI.UpdateEnvironment();
                }
            }
        }
        if (!lightening) {
            if (modelContainer.gameObject.TryGetComponent<MeshRenderer>(out var meshRendererParent)) {
                meshRendererParent.material.SetColor("_EmissionColor", oriEmColor);
                meshRendererParent.material.EnableKeyword("_EMISSION");
                DynamicGI.UpdateEnvironment();
            }
            var counter = 0;
            foreach (Transform child in modelContainer) {
                if (child.gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    meshRenderer.material.SetColor("_EmissionColor", oriChildEmColor[counter]);
                    meshRenderer.material.EnableKeyword("_EMISSION");
                    DynamicGI.UpdateEnvironment();
                    counter++;
                }
            }
        }
    }
}
