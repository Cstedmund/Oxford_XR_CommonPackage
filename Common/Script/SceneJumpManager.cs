using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJumpManager : MonoBehaviour {
    public void InternalLoadScene(string sceneName) {
        if(Physics.defaultContactOffset != 0.01f) {
            Physics.defaultContactOffset = 0.01f;
            Time.fixedDeltaTime = 0.02f;
        }

        if(GameManager.Instance.currentBook != null && GameManager.Instance.currentBook.sceneName == sceneName) {
            if(GameManager.Instance.currentBook.highPerformPhySetting == true) {
                Physics.defaultContactOffset = 0.0002f;
                Time.fixedDeltaTime = 0.001f;
            }
        }

        if((sceneName == "") & (GameManager.Instance.previousScenes.Count != 0)) {
            GameManager.Instance.previousScene = GameManager.Instance.previousScenes.Pop();
            Debug.Log($"Scene load {GameManager.Instance.previousScene}");
            SceneManager.LoadScene(GameManager.Instance.previousScene);
            return;
        }

        Debug.Log($"Scene load {sceneName}");
        GameManager.Instance.previousScenes.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
        return;
    }

    public virtual void CallURL(string url) {
        Application.OpenURL(url);
    }

    public virtual void UnloadUnity() {
        Application.Unload();
    }

    public virtual void QuitUnity() {
        Application.Quit();
    }
}
