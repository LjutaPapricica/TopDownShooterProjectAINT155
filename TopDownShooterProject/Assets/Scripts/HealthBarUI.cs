using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {

    public Image healthBar;
    private bool isVisible;
    public GameObject entity;
    private float yOffset;
    public int maxHealth;

    RectTransform theRectTransform;

    // Use this for initialization
    void Start()
    {
        theRectTransform = GetComponent<RectTransform>();

        yOffset = theRectTransform.position.y - entity.transform.position.y;

        maxHealth = entity.GetComponent<HealthSystem>().GetMaxHealth();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        theRectTransform.position = new Vector3(entity.transform.position.x, entity.transform.position.y + yOffset, entity.transform.position.z);
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
