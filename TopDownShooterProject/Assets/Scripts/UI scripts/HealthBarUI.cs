using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {

    public Image healthBar;
    private bool isVisible;
    private float yOffset;
    public int maxHealth;

    public Transform target;

    RectTransform theRectTransform;

    // Use this for initialization
    void Start()
    {
        theRectTransform = GetComponent<RectTransform>();

        yOffset = theRectTransform.position.y - target.transform.position.y;

        maxHealth = transform.parent.GetComponent<HealthSystem>().GetMaxHealth();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        theRectTransform.position = new Vector3(target.transform.position.x, target.transform.position.y + yOffset, target.transform.position.z);
        theRectTransform.rotation = Quaternion.identity;
    }

    public void ChangeHealthBar(int newHealth)
    {
        gameObject.SetActive(true);
        healthBar.fillAmount = newHealth / (float)maxHealth;

        ChangeBarColour(newHealth);

        StopAllCoroutines();
        StartCoroutine(ChangeVisiblity());
    }

    private void ChangeBarColour(int health)
    {
        if (health >= (maxHealth * 0.7f))
        {
            healthBar.color = Color.green;
        }
        else if (health >= (maxHealth * 0.3f))
        {
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.red;
        }
    }

    private IEnumerator ChangeVisiblity()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
