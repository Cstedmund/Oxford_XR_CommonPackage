using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PressHighlight3 : MonoBehaviour {
    [SerializeField]
    [Tooltip("Leave null to reference self")]
    private List<Transform> modelContainer;
    [SerializeField]
    private Color highlightColor = Color.yellow;

    private int brightLevel;
    private bool lightening;
    private Color oriEmColor;
    private List<Color> oriChildEmColor = new List<Color>();

    private void Start() {
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
            foreach (Transform child in modelContainer) {
                if (child.gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    HightlightMat(meshRenderer, highlightColor);
                }
            }
        }
        if (!lightening) {
            var counter = 0;
            foreach (Transform child in modelContainer) {
                if (child.gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    HightlightMat(meshRenderer, oriChildEmColor[counter]);
                    counter++;
                }
            }
        }
    }

    private void HightlightMat(MeshRenderer meshRendererParent, Color color) {
        var temp = new List<Material>();
        temp.AddRange(meshRendererParent.materials);
        temp.ForEach(mat => {
            mat.SetColor("_EmissionColor", color);
            mat.EnableKeyword("_EMISSION");
        });
        DynamicGI.UpdateEnvironment();
    }
}
