using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BtnFadeOut : MonoBehaviour {
    [SerializeField] float fadeOutTimer = 3f;
    [SerializeField] [Range(0,1)] float fadeOutLevel = 0.5f;
    private Coroutine wakeBtn;
    private Button fOButton = null;
    private Image fOImg = null;
    private Text fOText = null;
    private TextMeshProUGUI fOTmp = null;
    private Color imgColor, btnColor, textColor, tmpColor;


    private ColorBlock cb;

    private void Start() {
        fOButton = GetComponent<Button>();
        fOImg = GetComponent<Image>();
        fOText = GetComponentInChildren<Text>();
        fOTmp = GetComponentInChildren<TextMeshProUGUI>();
        if(fOButton == null) { fOButton = gameObject.AddComponent<Button>(); }
        btnColor = fOButton.colors.normalColor;
        if(fOImg != null) { imgColor = fOImg.color; }
        if(fOText != null) { textColor = fOText.color; }
        if(fOTmp != null) { tmpColor = fOTmp.color; }
        fOButton.onClick.AddListener(() => WakeBtn());
        WakeBtn();
    }

    public void WakeBtn() {
        if(wakeBtn != null)
            StopCoroutine(IWakeBtn());
        wakeBtn = StartCoroutine(IWakeBtn());
    }
    private IEnumerator IWakeBtn() {
        //cb = fOButton.colors;
        //cb.normalColor = new Color(btnColor.r,btnColor.g,btnColor.b,btnColor.a);
        //fOButton.colors = cb;
        if(fOImg != null) { fOImg.color = new Color(imgColor.r,imgColor.g,imgColor.b,imgColor.a); }
        if(fOText != null) { fOText.color = new Color(textColor.r,textColor.g,textColor.b,textColor.a); }
        if(fOTmp != null) { fOTmp.color = new Color(tmpColor.r,tmpColor.g,tmpColor.b,tmpColor.a); }
        yield return new WaitForSeconds(fadeOutTimer);
        //cb = fOButton.colors;
        //cb.normalColor = new Color(btnColor.r,btnColor.g,btnColor.b,btnColor.a * fadeOutLevel);
        //fOButton.colors = cb;
        if(fOImg != null) { fOImg.color = new Color(imgColor.r,imgColor.g,imgColor.b,imgColor.a * fadeOutLevel); }
        if(fOText != null) { fOText.color = new Color(textColor.r,textColor.g,textColor.b,textColor.a * fadeOutLevel); }
        if(fOTmp != null) { fOTmp.color = new Color(tmpColor.r,tmpColor.g,tmpColor.b,tmpColor.a * fadeOutLevel); }
        wakeBtn = null;
    }
}
