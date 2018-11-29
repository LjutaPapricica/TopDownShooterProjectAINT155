using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDie : MonoBehaviour {

    public AudioClip destroyedSound;

    public void Die()
    {
        AudioSource.PlayClipAtPoint(destroyedSound, transform.position);
        Destroy(gameObject);
    }
}
