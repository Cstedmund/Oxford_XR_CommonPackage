//using Photon.Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    private static AudioManager _instance;

    public static AudioManager Instance {
        get { return _instance; }
    }

    private AudioSource audioSource;

    private void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
        }

        audioSource = this.GetComponent<AudioSource>();

        audioClipDict = new Dictionary<string,AudioClip>();

        foreach(AudioClip clip in audioClips) {
            audioClipDict.Add(clip.name,clip);
        }
    }

    public enum SoundEffects {
        ClipsSFX,
        EyepieceSFX_Close,
        EyepieceSFX_Open,
        ObjectiveSFX,
        PowerButtonSFX,
        CardFlip,
        CardClose
        //ClickHintButton,
        //UsingHammer,
        //UsingTowel,
        //UsingUVLight,
        //Bleep,
        //Wrong,
        //ClickToolButton,
        //TimeUpWarning,
        //OxygenLevelWarning,
        //OxygenRefilling,
        //Poisoned,
        //SwitchOnTV,
        //DrawOutPaperFromBook,
        //DrawOutCasFromBook,
        //OpenIronBox,
        //ClickingJammer,
        //Jammer,
        //BrokenStone,
        //ClickingPoisonousGasFaucet,
        //TVCharging,
        //SelectMC,
        //SystemAnnouncement,
        //SpawnedARObject,
        //RotateObject,
        //CompleteStep,
    }

    [SerializeField]
    private List<AudioClip> audioClips;

    private Dictionary<string,AudioClip> audioClipDict;

    private void Start() {
        //audioSource.volume = BoltNetwork.IsServer ? 0 : 0.2f;
    }

    public void PlaySoundEffect(SoundEffects effect,float volume = 1f,bool loop = false) {
        if(!audioClipDict.ContainsKey(effect.ToString())) { return; }
        audioSource.volume = volume;
        audioSource.clip = audioClipDict[effect.ToString()];
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void StopSoundEffect() {
        audioSource.Stop();
    }
}