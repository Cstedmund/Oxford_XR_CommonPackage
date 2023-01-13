using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpAnim : MonoBehaviour {

    [SerializeField]
    private GameObject animTarget;

    private void OnEnable() {
        animTarget = animTarget == null ? gameObject : animTarget;
        animTarget.transform.localScale = new Vector3(0,0,0);
        LeanTween.scale(animTarget,Vector3.one,0.5f).setEase(LeanTweenType.easeOutBack).setDelay(0.5f);
        //StartCoroutine(SpawnAnimTimeLine());
    }

    //private IEnumerator SpawnAnimTimeLine() {
    //    var scale = 1f;
    //    yield return StartCoroutine(SpawnAnimation(5,new Vector3(scale,scale,scale)));
    //    //scale = 0.9f;
    //    //yield return StartCoroutine(SpawnAnimation(4,new Vector3(scale,scale,scale)));
    //    //scale = 1.1f;
    //    //yield return StartCoroutine(SpawnAnimation(3,new Vector3(scale,scale,scale)));
    //    //scale = 1f;
    //    //yield return StartCoroutine(SpawnAnimation(2,new Vector3(scale,scale,scale)));
    //}

    //private IEnumerator SpawnAnimation(float duration,Vector3 endSize) {
    //    var startTime = Time.time;
    //    var initialSize = animTarget.transform.localScale;
    //    var size = initialSize;
    //    while(size != endSize) {
    //        float t = (Time.time - startTime) / duration;
    //        size = Vector3.Lerp(size,endSize,t);
    //        animTarget.transform.localScale = size;
    //        yield return null;
    //    }
    //}
}

