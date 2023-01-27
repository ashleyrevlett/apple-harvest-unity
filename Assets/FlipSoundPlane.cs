using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSoundPlane : MonoBehaviour
{

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    void PlayFlipSound() {

        if (sound.isPlaying) return;

        sound.Play();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SequinEdge") {
            PlayFlipSound();
        }
    }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.tag == "SequinEdge") {
    //         PlayFlipSound();
    //     }
    // }

}
