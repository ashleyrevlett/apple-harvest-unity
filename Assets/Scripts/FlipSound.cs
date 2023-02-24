using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSound : MonoBehaviour
{

    AudioSource sound;
    float lastYRotation = 0f;
    private bool canPlaySound = true;

    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
        lastYRotation = gameObject.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newYRot = gameObject.transform.rotation.eulerAngles.y;
        float diff = Mathf.Abs(newYRot - lastYRotation);
        // if (newYRot != lastYRotation) {
        //     Debug.Log($"{newYRot} - {lastYRotation} = {diff}, {sound.isPlaying}");
        // }
        if (diff > .1f && canPlaySound) {
            StartCoroutine(PlayOnFlip());
        }
        lastYRotation = newYRot;
    }

    IEnumerator PlayOnFlip()
    {

        sound.Play();
        canPlaySound = false;
        yield return new WaitForSeconds(2);
        canPlaySound = true;
    }
}
