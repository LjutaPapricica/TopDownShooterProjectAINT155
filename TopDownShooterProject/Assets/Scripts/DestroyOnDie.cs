using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDie : MonoBehaviour {

	public void Die()
    {
        Destroy(gameObject);
    }
}
