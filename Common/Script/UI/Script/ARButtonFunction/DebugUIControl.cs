using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUIControl : MonoBehaviour
{
    private void OnEnable() {
        if (GameManager.Instance == null) {
            return;
        }
        if (GameManager.Instance.debugOn) {
            gameObject.active = true;
        } else {
            gameObject.active = false;
        }
    }
}
