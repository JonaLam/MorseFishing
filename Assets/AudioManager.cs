using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;

    public void PlayAudio(AudioClip audioClip, bool loop = false) 
    {
        if (audioClip == null)
            return;

        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = loop;
    }
}
