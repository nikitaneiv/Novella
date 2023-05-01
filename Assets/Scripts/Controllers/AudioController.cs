using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource musicSourse;
    [SerializeField] private AudioSource soundSourse;

    public void PlayAudio(AudioClip music, AudioClip sound)
    {
        if (sound != null)
        {
            soundSourse.clip = sound;
            soundSourse.Play();
        }

        if (music != null && musicSourse.clip != music)
        {
            StartCoroutine(SwitchMusic(music));
        }
    }

    private IEnumerator SwitchMusic(AudioClip music)
    {
        if (musicSourse.clip != null)
        {
            while (musicSourse.volume > 0)
            {
                musicSourse.volume -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            musicSourse.volume = 0;
        }
        musicSourse.clip = music;
        musicSourse.Play();
        
        while (musicSourse.volume < 0.5)
        {
            musicSourse.volume += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
