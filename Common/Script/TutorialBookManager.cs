using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialBookManager : MonoBehaviour {
    [SerializeField]
    private List<TutorialContent> tutContents = new List<TutorialContent>();
    [SerializeField]
    private Button cloesBtn;
    private Button prevBtn, nextBtn;
    private int pagePointer = 0;
    [SerializeField]
    private TextMeshProUGUI contentBox;
    [SerializeField]
    private Image imageDisplaySceen;
    private GameManager gameManager;

    [System.Serializable]
    public struct TutorialContent {
        [Multiline(2)] public string enContent;
        [Multiline(2)] public string zhContent;
        public Sprite tutImage;
    }

    private void Start() {
        gameManager = GameManager.Instance;
        foreach(Transform child in transform) {
            if(child.name.Contains("PrevBtn")) prevBtn = child.GetComponent<Button>();
            if(child.name.Contains("NextBtn")) nextBtn = child.GetComponent<Button>();
        }
        prevBtn.onClick.AddListener(() => PrevPage());
        nextBtn.onClick.AddListener(() => NextPage());
        cloesBtn.onClick.AddListener(() => ToggleTut());
        gameObject.SetActive(false);
        UpdateContent();
    }
    private void UpdateBtn() {
        prevBtn.interactable = pagePointer == 0 ? false : true;
        nextBtn.interactable = pagePointer >= tutContents.Count - 1 ? false : true;
    }

    private void UpdateContent() {
        if(tutContents[pagePointer].tutImage != null) {
            imageDisplaySceen.sprite = tutContents[pagePointer].tutImage;
            LeanTween.value(gameObject,65f,150f,0.3f).setOnUpdate((float val) => {
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(170,val);
            }).setEaseOutBack().setOnComplete(() => imageDisplaySceen.gameObject.SetActive(tutContents[pagePointer].tutImage != null));

        } else {
            imageDisplaySceen.gameObject.SetActive(tutContents[pagePointer].tutImage != null);
            LeanTween.value(gameObject,150f,65f,0.3f).setOnUpdate((float val) => {
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(170,val);
            }).setEaseOutBack();
            imageDisplaySceen.sprite = null;
        }
        contentBox.text = gameManager.currentLanguage == GameManager.Language.en ? tutContents[pagePointer].enContent : tutContents[pagePointer].zhContent;
        UpdateBtn();
    }
    public void PrevPage() {
        if(pagePointer <= 0) return;
        pagePointer--;
        UpdateContent();
    }
    public void NextPage() {
        if(pagePointer >= tutContents.Count - 1) return;
        pagePointer++;
        UpdateContent();
    }
    public void ToggleTut() {
        gameObject.SetActive(!gameObject.active);
    }

}
