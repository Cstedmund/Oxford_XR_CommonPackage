using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizeLanguage : MonoBehaviour {
    [SerializeField]
    private string m_EnglishContent;
    [SerializeField]
    private string m_ChineseContent;

    private TextMeshProUGUI textMeshProUGUI;
    private TextMeshPro textMeshPro;

    private void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshPro = GetComponent<TextMeshPro>();
    }

    private void Start() {
        if(textMeshProUGUI != null)
            textMeshProUGUI.text = GameManager.Instance.currentLanguage == GameManager.Language.en ? m_EnglishContent : m_ChineseContent;
        if(textMeshPro != null)
            textMeshPro.text = GameManager.Instance.currentLanguage == GameManager.Language.en ? m_EnglishContent : m_ChineseContent;
    }

    private void OnValidate() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshPro = GetComponent<TextMeshPro>();
        if(textMeshProUGUI != null)
            m_EnglishContent = textMeshProUGUI.text;
        if(textMeshPro != null)
            m_EnglishContent = textMeshPro.text;
    }
}
