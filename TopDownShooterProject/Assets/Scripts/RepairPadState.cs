using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPadState : MonoBehaviour {

    private SpriteRenderer theSpriteRenderer;
    public Sprite bluePad;
    public Sprite greenPad;
    public Sprite yellowPad;

    private AudioSource myAudioSource;
    public AudioClip repairedSound, backgroudnoise;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        theSpriteRenderer = GetComponent<SpriteRenderer>();
        theSpriteRenderer.sprite = bluePad;
    }
    
    public void PlayerRepairedState()
    {
        theSpriteRenderer.sprite = greenPad;
        myAudioSource.clip = repairedSound;
        myAudioSource.Play();
    }

    public void PlayerRepairingState()
    {
        theSpriteRenderer.sprite = yellowPad;
    }

    public void DefaultState()
    {
        theSpriteRenderer.sprite = bluePad;
    }
    
}
