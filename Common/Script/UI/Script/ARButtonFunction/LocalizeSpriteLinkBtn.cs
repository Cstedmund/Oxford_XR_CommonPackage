using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizeSpriteLinkBtn : MonoBehaviour {
    [SerializeField]
    private GameObject targetButton;
    [SerializeField]
    private Sprite engImg, zhImg;
    [SerializeField]
    private string engURL, zhURL;
    private Button button;
    private Image image;
    private SceneJumpManager sceneJumpManager;
    // Start is called before the first frame update
    void Start() {
        sceneJumpManager = FindObjectOfType<SceneJumpManager>();
        targetButton = targetButton == null ? gameObject : targetButton;
        if(targetButton.TryGetComponent<Button>(out var btn)) {
            button = btn;
        } else {
            button = targetButton.AddComponent<Button>();
        }
        if(targetButton.TryGetComponent<Image>(out var img)) {
            image = img;
        } else {
            image = targetButton.AddComponent<Image>();
        }

        image.sprite = GameManager.Instance.currentLanguage == GameManager.Language.en ? engImg : zhImg;
        button.onClick.AddListener(() => {
            sceneJumpManager.CallURL(GameManager.Instance.currentLanguage == GameManager.Language.en ? engURL : zhURL);
        });
    }

}
