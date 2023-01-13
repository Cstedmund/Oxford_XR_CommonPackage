using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnPointerClick() {
        for (int i = 0; i < this.transform.childCount; i++) {
            if (this.transform.GetChild(i).gameObject.GetComponent<UiBoardController>() != null) {
                this.transform.GetChild(i).gameObject.GetComponentInChildren<UiBoardController>().ToggleBoard();
            }
        }
    }
}
