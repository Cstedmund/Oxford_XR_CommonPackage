using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button),typeof(Image))]
public class SwapSprite : MonoBehaviour {
    [SerializeField]
    private Sprite selectedImage;
    [SerializeField]
    private Color tintColor = Color.white;
    private Color _oriTintColor;
    private Sprite _normalImage;
    private Button _button;
    private Image _buttonImage;

    private bool _pressedFlag = false;

    private void Start() {
        _buttonImage = GetComponent<Image>();
        _normalImage = _buttonImage.sprite;
        _oriTintColor = _buttonImage.color;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { ToggleButton(); });
    }

    public void ToggleButton() {
        _pressedFlag = !_pressedFlag;
        if(_pressedFlag) {
            _buttonImage.sprite = selectedImage;
            _buttonImage.color = tintColor;
        } else {
            _buttonImage.sprite = _normalImage;
            _buttonImage.color = _oriTintColor;
        }
    }

    public void SwitchButton(bool opt) {
        if(_pressedFlag == opt) return;
        _pressedFlag = opt;
        if(_pressedFlag) {
            _buttonImage.sprite = selectedImage;
            _buttonImage.color = tintColor;
        } else {
            _buttonImage.sprite = _normalImage;
            _buttonImage.color = _oriTintColor;
        }
    }

}
