using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoillerSound : MonoBehaviour {
    [SerializeField]
    private AudioClip sfx;
    [TagSelector]
    public string tagFilter = "";

    private AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start() {
        audioPlayer = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        ToggleSound(collision.gameObject);
    }
    private void OnTriggerEnter(Collider collision) {
        ToggleSound(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        ToggleSound(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        ToggleSound(collision.gameObject);
    }
    public void ToggleSound(GameObject collision) {
        if (tagFilter != "") {
            if (collision.gameObject.tag != tagFilter) return;
        }
        audioPlayer.PlayOneShot(sfx);
    }
    //private void OnTriggerExit(Collider other) {
    //    audioPlayer.PlayOneShot(sfx);
    //}
}
