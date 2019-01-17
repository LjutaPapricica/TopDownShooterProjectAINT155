using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {

    public Image healthBar;
    private bool isVisible;
    private float yOffset;
    public float timeHealthBarIsDisplayed = 3f;
    public int maxHealth;

    public Transform target;

    RectTransform theRectTransform;

    // Use this for initialization
    void Start()
    {
        //gets rect transform of the health bar
        theRectTransform = GetComponent<RectTransform>();

        //yoffset caluclated is the amount of vertical space between the gameobject position and the health bar position
        yOffset = theRectTransform.position.y - target.transform.position.y;

        //gets max health from parent gameobject from healthsystem component
        maxHealth = transform.parent.GetComponent<HealthSystem>().GetMaxHealth();

        //health bar at the start is hidden
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //follows position of the parent object and adds the y offset to appear slightly above
        theRectTransform.position = new Vector3(target.transform.position.x, target.transform.position.y + yOffset, target.transform.position.z);
        //the rotation remains unchanged
        theRectTransform.rotation = Quaternion.identity;
    }

    public void ChangeHealthBar(int newHealth)
    {
        //when the gameobject health changes the health bar is set to active
        gameObject.SetActive(true);
        //the amount the health bar is filled is calculated by the new health value divided by the total max health
        healthBar.fillAmount = newHealth / (float)maxHealth;

        //health value determines colour of the health bar
        ChangeBarColour(newHealth);

        //resets timer until health bar is hidden again
        StopAllCoroutines();
        StartCoroutine(ChangeVisiblity());
    }

    private void ChangeBarColour(int health)
    {
        //if the gameobject has more that 70% health the health bar is displayed green
        if (health >= (maxHealth * 0.7f))
        {
            healthBar.color = Color.green;
        }
        //if the gameobject has more that 30% health the health bar is displayed yellow
        else if (health >= (maxHealth * 0.3f))
        {
            healthBar.color = Color.yellow;
        }
        //if the gameobject less that 30% health the health bar is displayed red
        else
        {
            healthBar.color = Color.red;
        }
    }

    //health bar is hidden after set time period if the coroutine is not stopped
    private IEnumerator ChangeVisiblity()
    {
        yield return new WaitForSeconds(timeHealthBarIsDisplayed);
        gameObject.SetActive(false);
    }
}
