using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnimSFXEvent : MonoBehaviour {
    [SerializeField]
    private List<AudioClip> sfxs = new List<AudioClip>();

    private int pt = 0;
    private AudioSource audioSource;

    protected virtual void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void PlaySFX() {
        audioSource.PlayOneShot(sfxs[pt]);
        pt++;
        pt %= sfxs.Count;
    }
}
