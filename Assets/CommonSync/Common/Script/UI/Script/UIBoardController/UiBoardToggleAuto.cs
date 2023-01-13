using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBoardToggleAuto : UiBoardController {
    // Start is called before the first frame update

    private void OnEnable() {
        ToggleBoard();
    }

    private void OnDisable() {
        ToggleBoard();
    }
}
