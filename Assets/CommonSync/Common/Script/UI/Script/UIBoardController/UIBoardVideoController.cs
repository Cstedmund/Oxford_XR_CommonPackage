using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIBoardVideoController : UiBoardController {
    [SerializeField]
    private RawImage _videoScreen;
    [SerializeField]
    private VideoPlayer _videoPlayer;
    [SerializeField]
    private Sprite[] _playButtonSprite = new Sprite[2];
    [SerializeField]
    private GameObject _pauseButton;
    // Start is called before the first frame update

    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public override void FinishedOpenAnimation() {
        if (_videoScreen == null) return;

        if (_open) {
            ToggleVideoPlay();
        } else {
            ToggleVideoPlay();
        }
    }

    public void ToggleVideoPlay() {
        _videoPlayer.playbackSpeed = 1f;
        if (_videoPlayer.isPlaying) {
            _videoPlayer.Pause();
            _pauseButton.GetComponent<Image>().sprite = _playButtonSprite[1];
        } else if(_open) {
            _videoPlayer.Play();
            _pauseButton.GetComponent<Image>().sprite = _playButtonSprite[0];
        }
    }

    public void ChangeSpeed(float speed) {
        if (_videoScreen == null) return;

        if (!_videoPlayer.isPlaying) 
            return;
        if (!_videoPlayer.canStep) 
            return;
        _videoPlayer.playbackSpeed = speed;
    }
}
