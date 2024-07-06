using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip Button;
    public AudioClip Bossfight;
    public AudioClip Explosion;
    public AudioClip Fire;
    public AudioClip Highscore;
    public AudioClip Jump;
    public AudioClip Music;
    public AudioClip Rune;


    private GameObject singleSoundGameObject;
    private int NUMBER_OF_SOUND_SOURCES = 25;
    private int lastSourceIndex = 0;
    private List<AudioSource> audioSourcesPool;
    private GameObject musicGameObject;

    private void Start()
    {
        singleSoundGameObject = new GameObject("SingleSound");


        foreach (var item in FindObjectsOfType<Button>(true))
        {
            item.GetComponent<Button>().onClick.AddListener(delegate { PlaySingleSound(Button); });
        }
        audioSourcesPool = new List<AudioSource>();

        //fill pool with AudioSources
        for (int i = 0; i < NUMBER_OF_SOUND_SOURCES; i++)
        {
            var source = singleSoundGameObject.AddComponent<AudioSource>();
            audioSourcesPool.Add(source);
        }
    }

    public void PlaySingleSound(AudioClip sound)
    {
        AudioSource audioSource = audioSourcesPool[GetPoolIndex()];
        if (audioSource == null || sound == null)
        {
            return;
        }
        audioSource.PlayOneShot(sound);
    }

    public void PlaySingleSoundAfterDelay(AudioClip sound, float delayInSecs)
    {
        AudioSource audioSource = audioSourcesPool[GetPoolIndex()];
        audioSource.clip = sound;
        audioSource.PlayDelayed(delayInSecs);
    }

    public void PlayLoopedMusic(AudioClip sound)
    {
        if (musicGameObject == null)
        {
            musicGameObject = new GameObject("Sound");
        }
        AudioSource audioSource = musicGameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.volume = 0.35f;
        audioSource.Play();
        audioSource.loop = true;
    }

    private int GetPoolIndex()
    {
        lastSourceIndex++;
        if (lastSourceIndex >= NUMBER_OF_SOUND_SOURCES)
        {
            lastSourceIndex = 0;
        }
        return lastSourceIndex;
    }
}
