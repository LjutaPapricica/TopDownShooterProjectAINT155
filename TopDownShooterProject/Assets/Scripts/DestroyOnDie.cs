using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDie : MonoBehaviour {

    public AudioClip[] destroyedSounds;

    [Range(0f, 1f)]
    public float destroyedSoundVol = 1f;

    public void Die()
    {
        if (destroyedSounds.Length > 0)
        {
            AudioClip randomDeathSound = destroyedSounds[Random.Range(0, destroyedSounds.Length)];
            AudioSource.PlayClipAtPoint(randomDeathSound, transform.position, destroyedSoundVol);
        }

        Destroy(gameObject);
    }
}
