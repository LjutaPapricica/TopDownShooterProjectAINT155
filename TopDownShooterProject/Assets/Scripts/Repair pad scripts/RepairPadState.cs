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

        //gets sprite renderer component of the repair pad and displays the default blue repair pad sprite at the start
        theSpriteRenderer = GetComponent<SpriteRenderer>();
        theSpriteRenderer.sprite = bluePad;
    }
    
    //when the player has been repaired the repair pad turns green and a repaired sound is played
    public void PlayerRepairedState()
    {
        theSpriteRenderer.sprite = greenPad;
        myAudioSource.clip = repairedSound;
        myAudioSource.Play();
    }

    //when the player is in the process of repairing the repair pad turns yellow
    public void PlayerRepairingState()
    {
        theSpriteRenderer.sprite = yellowPad;
    }

    //when player leaves the repair pad it changes colour to its default blue state
    public void DefaultState()
    {
        theSpriteRenderer.sprite = bluePad;
    }
    
}
