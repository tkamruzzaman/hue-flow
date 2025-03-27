using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource musicAudioSource;

    void Awake()
    {
     if(Instance == null){ Instance = this;}   
    }

    private void PlaySFXSound(AudioClip audioClip, float volume = 1f){
        sfxAudioSource.PlayOneShot(audioClip, volume);
    }
}
