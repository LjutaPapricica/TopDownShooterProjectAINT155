using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    private AudioSource myAudioSource;

    public AudioClip[] music;

    // Use this for initialization
    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        while (true)
        {
            myAudioSource.clip = music[Random.Range(0, music.Length)];
            myAudioSource.Play();
            yield return new WaitForSeconds(myAudioSource.clip.length);
        }
    }


}
