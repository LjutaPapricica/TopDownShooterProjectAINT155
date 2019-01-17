/**********************************************************
 * 
 * WeaponManager.cs
 * - controls the weapon GameObjects belonging to the player
 * - weapons have to be children of the WeaponManager's GameObject
 * - the WeaponManager will activate one weapon at a time
 *  
 *   
 * public methods
 * - ChangeWeapon
 *   - changes the current weapon to the weapon specified by index
 *   - index refers to the weapon in the Hierarchy, from top to bottom
 *   - e.g. index 0 is at the top, index 1 is below etc
 *   - the current weapon's GameObject will be active, the rest will be deactivated
 *   - if the index is not present, the weapon will not be changed
 *   
 * - AddWeapon
 *   - adds a new weapon as a child of the WeaponManager from the provided prefab
 *   - the new weapon will be selected when added
 *   
 * 
 * private methods
 * - Start
 *   - changes the weapon to the first weapon in the WeaponManager
 *   
 * - Update
 *   - changes weapon by pressing number keys from 1-3
 * 
 * 
 **********************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    /*
     * START
     * see link: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
     * change weapon to the first weapon (index zero)
     */
     
    public bool isShootMultiple = false;
    private int currentWeaponIdex = 0;
    public Transform[] weaponHardpoints;
    public GameObject[] weapons;
    public Text weaponUI;


    void Start()
    {
         //at the start the first weapon is selected and the other weapons are disabled
        ChangeWeapon(0);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);
        PrintWeaponUI();
    }


    /*
     * ChangeWeapon
     * changes the weapon to the one specificed by the "index" parameter
     * if the weapon is not present, don't change weapon
     */ 
    public void ChangeWeapon(int index)
    {
        /*
         * CHECK THE WEAPON EXISTS
         * if the index is less than the total weapons we have, we can select it
         * see link: https://docs.unity3d.com/ScriptReference/Transform-childCount.html
         */
        
        //if the next weapon idex is outside the range of the array then
        //cycle back to select the first weapon
        if (index >= weapons.Length)
        {
            index = 0;
        }

        currentWeaponIdex = index;

            /*
             * LOOP THROUGH ALL WEAPONS AND ACTIVATE THE ONE AT index AND DEACTIVATE THE REST
             * we will loop through all of the weapons we have, activate the GAmeObject on the one at "index"
             * and deactivate the rest of the weapons
             * NOTE: we are looping through all of the child GameObjects inside of the WeaponManager
             */
            for (int i = 0; i < weapons.Length; i++)
            {
                /*
                 * SELECT THE WEAPON AT index
                 * if we find the weapon at index (our selected weapon) we activate it's GameObject
                 */
                if (i == index)
                {
                    /*
                     * ACTIVATE THE WEAPON GAMEOBJECT
                     * we use SetActive to activate the weapon GameObject
                     * see link: https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
                     */
                    weapons[i].SetActive(true);
                }
                //if shift is not held down then the weapon is disabled if its not the one selected
                else if (!isShootMultiple) 
                {
                    weapons[i].SetActive(false);
                }
                //if the shift button is held down then previously selected weapons are not disabled and kept selected
            }

            //weapon UI updated as new weapons selected / deselected
        PrintWeaponUI();

    }


    /*
     * AddWeapon
     * adds a new weapon as a child of the WeaponManager from the "prefab" parameter
     * the new weapon will be selected when added
     */
    public void AddWeapon(GameObject prefab)
    {
        /*
         * CREATE THE NEW WEAPON FROM THE prefab PARAMETER
         * we use Instantiate to create our new weapon
         * the weapon will be in the same position and roation as the WeaponManager
         * the WeaponManager's transform will be the parent of the weapon in the Hierarchy
         * see link: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
         */
        GameObject weapon = Instantiate(prefab, weaponHardpoints[0].position, weaponHardpoints[0].rotation, transform);

        /*
         * CHANGE WEAPON TO THE NEW WEAPON
         * here, we get the current sibling index of the new weapon (where the weapon is in the Hierachy)
         * we change weapon to the current weapon's sibling index
         * NOTE: the sibling index get be obtined from its transform component
         * see link: https://docs.unity3d.com/ScriptReference/Transform.GetSiblingIndex.html
         */
        ChangeWeapon(weapons.Length - 1);

        PrintWeaponUI();
    }


    /*
     * Update
     * see link: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
     * we will get keyboard input to change weapons here
     * key 1 = weapon 0
     * key 2 = weapon 1
     * key 3 = weapon 2
     * see link: https://docs.unity3d.com/ScriptReference/Input.GetKeyDown.html
     * NOTE: the keyboard number keys are called "Alpha" + the keyboard number
     */
    private void Update()
    {

        /*
         * CHANGE TO WEAPON 0 WHEN KEY 1 IS PRESSED
         * when the keyboard key 1 is pressed, change to the first weapon (wepaon 0)
         */ 
        if (Input.GetKeyDown(KeyCode.Alpha1)) // if key 1 is pressed
        {
            ChangeWeapon(0); // change to weapon 0
        }

        /*
         * CHANGE TO WEAPON 1 WHEN KEY 2 IS PRESSED
         * when the keyboard key 2 is pressed, change to the second weapon (weapon 1)
         */
        if (Input.GetKeyDown(KeyCode.Alpha2)) // if key 2 is pressed
        {
            ChangeWeapon(1); // change to weapon 1
        }

        /*
         * CHANGE TO WEAPON 2 WHEN KEY 3 IS PRESSED
         * when the keyboard key 3 is pressed, change to the third weapon (weapon 2)
         */
        if (Input.GetKeyDown(KeyCode.Alpha3)) // if key 3 is pressed
        {
            ChangeWeapon(2); // change to weapon 2
        }

        //if right mouse button clicked then the next weapon is selected
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeWeapon(currentWeaponIdex + 1);
        }

        //when the shift is selected then the player can choose multiple weapons
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isShootMultiple = true;
        }
        else
        {
            isShootMultiple = false;
        }        

    }

    //returns an array of gameobjects (all weapons the play has)
    public GameObject[] GetWeapons()
    {
        return weapons;
    }

    //prints weapon UI
    public void PrintWeaponUI()
    {
        weaponUI.text = "Weapons:\n";
        for (int i = 0; i < weapons.Length; i++)
        {
            //gets the player name as a string
            string weaponName = weapons[i].name;
            //gets the number of shots left calculated by the weapon
            string shotsLeft = weapons[i].GetComponent<Weapon>().GetShotsLeft();

            //if the weapon is acitve (selected) then highlight the text a different colour to show it's selected
            if (weapons[i].active)
                weaponUI.text += "<color=#ffff00ff>" + weaponName + shotsLeft + "</color>\n";
            else
                weaponUI.text += weapons[i].name + "\n";
        }
    }

}
