using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GetNameHierarchy : MonoBehaviour {
    Text text = null;
    TextMeshProUGUI tmp = null;
    [SerializeField]
    GameObject parent = null;
    [SerializeField] bool onValideUpdate = true;

#if UNITY_EDITOR
    [InspectorButton("UpdateName",ButtonWidth = 100)]
    public bool updateName;
    private void OnValidate() {
        if(!onValideUpdate) return;
        UpdateName();
    }

    private void UpdateName() {
        //Debug.Log("NameUpdate");
        parent = transform.parent.gameObject == null ? null : transform.parent.gameObject;
        //gameObject.name = parent.gameObject.name;
        if(parent.TryGetComponent(out Button btn)) {
            if(TryGetComponent(out text)) {
                text.text = parent.name;
            }
            if(TryGetComponent(out tmp)) {
                tmp.text = parent.name;
            };
        };
    }
#endif
}
