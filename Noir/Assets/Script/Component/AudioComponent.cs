using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioComponent : MonoBehaviour {

    public AudioData PlayerAudioInfo;
    private AudioSource audioSource;
    
    private Dictionary<int, AudioClip> AudioClipCollection;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        CreateAudioClipCollection();

    }

    private void CreateAudioClipCollection()
    {
        AudioClipCollection = new Dictionary<int, AudioClip>();

        for (int i = 0; i < PlayerAudioInfo.audioInfo.Length; i++)
        {
            AudioClipCollection[PlayerAudioInfo.audioInfo[i].ID] = PlayerAudioInfo.audioInfo[i].audioClip;
        }
    }

    public void AudioPlay(int ID)
    {

        audioSource.Stop();
        audioSource.clip = AudioClipCollection[ID];
        audioSource.Play();

    }

}
