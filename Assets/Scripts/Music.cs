using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        RegisterController.OnWorkshiftStart += PlayMusic;
        Timer.OnTimerEnded += StopMusic;
    }

    private void PlayMusic()
    {
        audioSource.Play();
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }

}
