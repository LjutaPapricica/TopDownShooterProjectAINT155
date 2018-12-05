using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDie : MonoBehaviour {

    public AudioClip destroyedSound;

    public void Die()
    {
        if (destroyedSound != null)
        {
            AudioSource.PlayClipAtPoint(destroyedSound, transform.position);
        }

        Destroy(gameObject);
    }
}
