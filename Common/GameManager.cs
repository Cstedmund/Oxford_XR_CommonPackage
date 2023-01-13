using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Start is called before the first frame update
    private static GameManager _instance;
    [SerializeField]
    public bool debugOn, readyRelease, standaloneBuild;
    [SerializeField]
    private Canvas debugIndicator;

    public static Action OnNativeAppDidDisconnect;
    public static Action OnNativeAppDidBecomeActive;
    public static Action OnNativeAppWillResignActive;
    public static Action OnNativeAppWillEnterForeground;
    public static Action OnNativeAppDidEnterBackground;

    public static GameManager Instance {
        get { return _instance; }
    }

    private void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    [SerializeField]
    public Language currentLanguage;
    [SerializeField]
    public BookItem currentBook;
    [HideInInspector]
    [SerializeField]
    public string currentURL, previousScene;
    public Stack<string> previousScenes = new();

    public enum Book {
        B1,
        B2,
        B3,
        B4,
        B5,
        B6,
        B7,
        B8,
        B9,
        B10,
        B11,
        B12
    }
    public enum Language {
        en,
        zh,
    }

    private void Start() {
        Debug.Log(FunctionLibrary.GetCurrentDevice());
        Debug.Log(SystemInfo.deviceName);

        if(debugOn) {
            debugIndicator.enabled = true;
        } else {
            debugIndicator.enabled = false;
        }
    }

    private void Update() {
        DebugControl();
    }

    private void DebugControl() {
        if(readyRelease) return;
        if(Input.touchCount == 2 && Input.touchCount < 3) {
            debugOn = false;
            debugIndicator.enabled = false;
        }
        if(Input.touchCount == 3) {
            debugOn = true;
            debugIndicator.enabled = true;
        }
    }

    public void PromotionDebugKey() {
        InternalSceneJump(currentBook.sceneName);
    }

    public void InternalSceneJump(string m_SceneName) {
        GetComponent<SceneJumpManager>().InternalLoadScene(m_SceneName);
    }

    public virtual void CallURL(string url) {
        GetComponent<SceneJumpManager>().CallURL(url);
    }

    public virtual void UnloadUnity() {
        GetComponent<SceneJumpManager>().UnloadUnity();
    }

    public virtual void QuitUnity() {
        GetComponent<SceneJumpManager>().QuitUnity();
    }

    //Android lifecycle here
    public void OnApplicationFocus(bool focus) {
        Debug.Log("OnApplicationFocus: " + focus);
        if(focus) {
            if(OnNativeAppDidBecomeActive != null)
                OnNativeAppDidBecomeActive.Invoke();
            if(OnNativeAppWillEnterForeground != null)
                OnNativeAppWillEnterForeground.Invoke();
            if(OnNativeAppWillResignActive != null)
                OnNativeAppWillResignActive.Invoke();
        }
    }
    public void OnApplicationPause(bool pause) {
        Debug.Log("OnApplicationPause: " + pause);
        if(pause)
            if(OnNativeAppDidEnterBackground != null)
                OnNativeAppDidEnterBackground.Invoke();
    }
    public void OnApplicationQuit() {
        Debug.Log("OnApplicationQuit");
        if(OnNativeAppDidDisconnect != null)
            OnNativeAppDidDisconnect.Invoke();
    }

    //iOS LifeCycle
    public void SceneDidDisconnect() {
        Debug.Log("SceneDidDisconnect");
        if(OnNativeAppDidDisconnect != null)
            OnNativeAppDidDisconnect.Invoke();
    }
    public void SceneDidBecomeActive() {
        Debug.Log("SceneDidBecomeActive");
        if(OnNativeAppDidBecomeActive != null)
            OnNativeAppDidBecomeActive.Invoke();
    }
    public void SceneWillResignActive() {
        Debug.Log("SceneWillResignActive");
        if(OnNativeAppWillResignActive != null)
            OnNativeAppWillResignActive.Invoke();
    }
    public void SceneWillEnterForeground() {
        Debug.Log("SceneWillEnterForeground");
        if(OnNativeAppWillEnterForeground != null)
            OnNativeAppWillEnterForeground.Invoke();
    }
    public void SceneDidEnterBackground() {
        Debug.Log("SceneDidEnterBackground");
        if(OnNativeAppDidEnterBackground != null)
            OnNativeAppDidEnterBackground.Invoke();
    }
}
