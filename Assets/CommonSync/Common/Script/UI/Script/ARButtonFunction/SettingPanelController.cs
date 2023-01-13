using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelController : MonoBehaviour {
    [SerializeField]
    protected GameObject settingPanel;
    [SerializeField]
    protected GameObject controlPanel;
    [SerializeField]
    protected Vector3 desPos = new Vector3(), oriPos = new Vector3();
    [SerializeField]
    protected float timespend = 0.5f;
    [SerializeField]
    protected bool enableSFX = true;
    [SerializeField]
    protected LeanTweenType easeType = LeanTweenType.easeInOutBack;
    [SerializeField]
    protected bool peek = false;
    [SerializeField]
    [Min(0.5f)]
    protected float peekTime = 5f;


    protected bool isOpen = false;

    private void Awake() {
        if(settingPanel == null) {
            settingPanel = gameObject;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start() {
        if(controlPanel != null)
            controlPanel.SetActive(isOpen);
        if(settingPanel == null) {
            settingPanel = gameObject;
        }
        settingPanel.GetComponent<RectTransform>().anchoredPosition = oriPos;
        if(peek) {
            Peek();
        }
    }

    public void Peek() {
        StartCoroutine(IPeek());
    }

    private IEnumerator IPeek() {
        SwitchPanel(true);
        yield return new WaitForSeconds(peekTime);
        SwitchPanel(false);
    }

    // Update is called once per frame
    public virtual void TogglePanel() {
        Debug.Log("Call Panel + " + isOpen);
        if(AudioManager.Instance != null && enableSFX)
            AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffects.CardClose,0.5f);
        if(isOpen) {
            LeanTween.move(settingPanel.GetComponent<RectTransform>(),oriPos,timespend).setEase(easeType);
        } else {
            LeanTween.move(settingPanel.GetComponent<RectTransform>(),desPos,timespend).setEase(easeType);
        }
        isOpen = !isOpen;
        if(controlPanel != null)
            controlPanel.SetActive(isOpen);
    }

    public virtual void SwitchPanel(bool option) {
        if(settingPanel == null) return;
        if(AudioManager.Instance != null && enableSFX)
            AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffects.CardClose,0.5f);
        if(!option) {
            LeanTween.move(settingPanel.GetComponent<RectTransform>(),oriPos,timespend).setEase(easeType);
            isOpen = false;
        } else {
            LeanTween.move(settingPanel.GetComponent<RectTransform>(),desPos,timespend).setEase(easeType);
            isOpen = true;
        }
        if(controlPanel != null)
            controlPanel.SetActive(option);
    }
}
