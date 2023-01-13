using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRotate : MonoBehaviour {
    [SerializeField]
    private GameObject targetObject;
    public void UpdateModelRotation() {
        //targetObject.transform.Rotate(transform.up,GetComponent<Slider>().value);
        targetObject.GetComponent<Transform>().eulerAngles = new Vector3(
            targetObject.transform.eulerAngles.x,
            GetComponent<Slider>().value,
            targetObject.transform.eulerAngles.z);
    }
}
