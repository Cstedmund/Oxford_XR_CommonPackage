using System;
using UnityEngine;

public class StopWatch : MonoBehaviour {
    private float elapsedRunningTime = 0f;
    private float runningStartTime = 0f;
    private float pauseStartTime = 0f;
    private float elapsedPausedTime = 0f;
    private float totalElapsedPausedTime = 0f;
    public bool running = false;
    private bool paused = false;

    public Action OnStopWatchBegin, OnStopWatchPause, OnStopWatchReset, OnStopWatchResume, OnStopWatchRun;

    void Update() {
        if (running) {
            elapsedRunningTime = Time.time - runningStartTime - totalElapsedPausedTime;
            //Debug.Log(GetMinutes()+":"+GetSeconds());
        } else if (paused) {
        } else if (paused) {
            elapsedPausedTime = Time.time - pauseStartTime;
        }
    }

    public void Begin() {
        if (!running && !paused) {
            runningStartTime = Time.time;
            running = true;
            if(OnStopWatchRun != null)
                OnStopWatchRun.Invoke();
            if (OnStopWatchBegin != null)
                OnStopWatchBegin.Invoke();
        }
    }

    public void Pause() {
        if (running && !paused) {
            running = false;
            pauseStartTime = Time.time;
            paused = true;
            if (OnStopWatchPause != null)
                OnStopWatchPause.Invoke();
        }
    }

    public void Unpause() {
        if (!running && paused) {
            totalElapsedPausedTime += elapsedPausedTime;
            running = true;
            paused = false;
            if(OnStopWatchRun != null)
                OnStopWatchRun.Invoke();
            if (OnStopWatchResume != null)
                OnStopWatchResume.Invoke();
        }
    }

    public void Reset() {
        elapsedRunningTime = 0f;
        runningStartTime = 0f;
        pauseStartTime = 0f;
        elapsedPausedTime = 0f;
        totalElapsedPausedTime = 0f;
        running = false;
        paused = false;
        if (OnStopWatchReset != null)
            OnStopWatchReset.Invoke();
    }

    public int GetMinutes() {
        return (int)(elapsedRunningTime / 60f);
    }

    public int GetSeconds() {
        return (int)(elapsedRunningTime);
    }

    public float GetMilliseconds() {
        return (float)(elapsedRunningTime - System.Math.Truncate(elapsedRunningTime));
    }

    public float GetRawElapsedTime() {
        return elapsedRunningTime;
    }
}