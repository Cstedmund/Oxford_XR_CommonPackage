using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeArea : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject smallTopBar;
    private RectTransform panelSafeArea;
    private Rect currentSafeArea = new Rect();
    private ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation;

    // Start is called before the first frame update
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();
        currentOrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;

        // for old version of iPhone
        if (smallTopBar != null) {
            if (Screen.safeArea == canvas.pixelRect) {
                smallTopBar.SetActive(true);
            } else { smallTopBar.SetActive(false); }
        }
        ApplySafeArea();
    }

    void ApplySafeArea() {
        if (panelSafeArea == null) {
            return;
        }
        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;

        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;

        currentOrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;

    }

    private void Update() {
        if ((currentOrientation != Screen.orientation) || (currentSafeArea != Screen.safeArea)) {
            ApplySafeArea();
        }
    }

}
