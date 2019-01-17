using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDie : MonoBehaviour {

    public AudioClip[] destroyedSounds;

    [Range(0f, 1f)]
    public float destroyedSoundVol = 1f;

    public void Die()
    {
        //if there are sounds in the AudioClip array then pick a random death sound
        //clip is played at transform of destroyed gameobject
        if (destroyedSounds.Length > 0)
        {
            AudioClip randomDeathSound = destroyedSounds[Random.Range(0, destroyedSounds.Length)];
            AudioSource.PlayClipAtPoint(randomDeathSound, transform.position, destroyedSoundVol);
        }

        //gameobject destroyed
        Destroy(gameObject);
    }
}
