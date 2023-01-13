using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle3DUI : ClickableObject
{
    protected override void OnPointerClick() {
        GetComponentInChildren<UiBoardController>().ToggleBoard();
    }
}
