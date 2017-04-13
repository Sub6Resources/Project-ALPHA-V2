using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour {

    public static int ALARM_SOUND = 0;
    public static int CRASH_SOUND = 1;
    public static int MUSIC = 2;
    

    // Use this for initialization
    void Start () {
	}
	

    public void startSound(int sound)
    {
        AudioSource theSound = GetComponents<AudioSource>()[sound];
        theSound.loop = true;
        theSound.Play();
    }
    public void playSound(int sound)
    {
        AudioSource theSound = GetComponents<AudioSource>()[sound];
        
        theSound.loop = false;
        theSound.Play();
    }
    public void stopSound(int sound)
    {
        AudioSource theSound = GetComponents<AudioSource>()[sound];
        theSound.playOnAwake = false;
        theSound.loop = false;
        theSound.Stop();
    }
    public void pauseSound(int sound)
    {
        AudioSource theSound = GetComponents<AudioSource>()[sound];
        theSound.Pause();
    }
    public void fadeOut(int sound, float duration)
    {
        StartCoroutine(FadeOut(GetComponents<AudioSource>()[sound], 2.0f));
    }
    public void unpauseSound(int sound)
    {
        AudioSource theSound = GetComponents<AudioSource>()[sound];
        theSound.UnPause();
    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
