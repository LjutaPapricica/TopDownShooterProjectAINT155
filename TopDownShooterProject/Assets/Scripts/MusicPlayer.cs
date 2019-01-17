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
        //at the start finds all objects of the same type in the scene
        //if there is another music player in the scene then destroy this game object
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        //if the only music player then the object is not destroyed when transitioning to a new scene
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();

        //starts coroutine
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        //endless loop
        while (true)
        {
            //picks random music clip out of a selection of music tracks from an AudioClip array
            myAudioSource.clip = music[Random.Range(0, music.Length)];
            //the selected audioclip is played
            myAudioSource.Play();
            //corutine waits until the whole song has been played before selecting a new clip at random to be played
            yield return new WaitForSeconds(myAudioSource.clip.length);
        }
    }


}
