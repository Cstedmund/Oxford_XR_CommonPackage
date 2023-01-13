using System;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenWrapper {
    public LTDescr Animation { get; }

    public List<Action> OnCompleteCallBacks { get; }

    public bool Executed { get; private set; }

    public LeanTweenWrapper(LTDescr anim) {
        Animation = anim;
        anim.setOnComplete(OnComplete);

        OnCompleteCallBacks = new List<Action>();

        Executed = false;
    }

    private void OnComplete() {
        if(Executed) {
            Debug.LogError("Tried to OnComplete more than once.");
            return;
        }

        OnCompleteCallBacks.ForEach(c => c.Invoke());
        Executed = true;
    }

    public void SetOnComplete(Action callback) {
        if(Executed) {
            Debug.LogError("Tried to assign callback to already completed anim.");
            return;
        }

        OnCompleteCallBacks.Add(callback);
    }
}