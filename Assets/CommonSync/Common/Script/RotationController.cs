using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTT.ScreenRotationManagement;
using UnityEngine.UI;

public class RotationController : MonoBehaviour {
  // Start is called before the first frame update
  private bool _autoRotate = false;

  //private bool useDTT = true;
  [SerializeField]
  private ScreenOrientation _desiredOrientation;
  [SerializeField]
  private Image _image;

  private void Start() {
    if (_desiredOrientation == null) {
      ScreenRotationManager.SetAutoRotation(true);
      Debug.Log("DTT Set AutoRotate");
      return;
    }

    if (_desiredOrientation == ScreenOrientation.Portrait) {
      ScreenRotationManager.SetOrientation(ScreenOrientation.LandscapeLeft);
      _ = StartCoroutine(DelayChangeRotaion());
    } else {
      ScreenRotationManager.SetOrientation(ScreenOrientation.Portrait);
      _ = StartCoroutine(DelayChangeRotaion());
    }
  }

  private IEnumerator DelayChangeRotaion() {
    yield return new WaitForSeconds(0.1f);
    ScreenRotationManager.SetOrientation(_desiredOrientation);
    Debug.Log("DTT Set Orientation " + _desiredOrientation);
  }

  public void SetOrientation_P() {
    ScreenRotationManager.SetOrientation(ScreenOrientation.Portrait);
  }
  public void SetOrientation_L() {
    ScreenRotationManager.SetOrientation(ScreenOrientation.LandscapeLeft);
  }

  public void SetAutoRotation() {
    _autoRotate = !_autoRotate;
    ScreenRotationManager.SetAutoRotation(_autoRotate);
    if (_image == null) return;
    if (_autoRotate) {
      _image.color = Color.yellow;
    } else {
      _image.color = Color.white;
    }
  }

  public void LockRotaton_P() {
    ScreenRotationManager.LockOrientations(ScreenOrientation.Portrait);
  }

  public void LockRotaton_L() {
    ScreenRotationManager.LockOrientations(ScreenOrientation.LandscapeLeft);
  }

  public void UnlockAll() {
    ScreenRotationManager.UnLockOrientations();
  }
}
