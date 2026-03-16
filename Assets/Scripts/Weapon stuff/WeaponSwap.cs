using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    [Header("Inventory")]
    public List<GameObject> allWeapons = new List<GameObject>();
    [SerializeField] private List<GameObject> ownedWeapons = new List<GameObject>();
    private int currentIndex = -1;
    private int previousIndex = -1;

    [Header("Input")]
    public KeyCode previousWeapon = KeyCode.Q;

    [Header("Between scene utility")]
    public static WeaponSwap Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // avoid duplicates
            return;
        }

        Instance = this;

        foreach (Transform child in transform)
        {
            //skips the cross hair over cause I'm to lazy to move it
            if (child.CompareTag("CrossHair"))
            {
                continue;
            }

            allWeapons.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        if (allWeapons.Count == 0)
        {
            return;
        }

        GameObject startingWeapon = allWeapons[0];

        if (!ownedWeapons.Contains(startingWeapon))
        {
            ownedWeapons.Add(startingWeapon);
        }

        EquipWeapon(0);
    }

    private void Start()
    {

    }

    private void Update()
    {
        WeaponSwitching();
    }

    private void WeaponSwitching()
    {
        //get scroll wheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            NextWeapon();
        }
        else if (scroll < 0)
        {
            PreviousWeapon();
        }

        //handles number input
        for (int i = 0; i < ownedWeapons.Count; i++)
        {
            //alpha is an enum, so I can add to it, which saves me having to make a new if statement for each key
            if(Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                EquipWeaponByReference(ownedWeapons[i]);
            }
        }

        //switch to the previously equiped weapon
        if (Input.GetKeyDown(previousWeapon))
        {
            if (previousIndex != -1)
            {
                EquipWeapon(previousIndex);
            }
        }
    }

    public void WeaponUnlock(GameObject weapon)
    {
        if (!ownedWeapons.Contains(weapon))
        {
            ownedWeapons.Add(weapon);
            Debug.Log("Weapon added: " + weapon.name);
        }
    }

    private void NextWeapon()
    {
        if (ownedWeapons.Count < 2)
        {
            return;
        }

        int nextIndex = (currentIndex + 1) % ownedWeapons.Count;
        EquipWeapon(nextIndex);
    }

    private void PreviousWeapon()
    {
        if (ownedWeapons.Count < 2)
        {
            return;
        }

        int prevIndex = (currentIndex - 1 + ownedWeapons.Count) % ownedWeapons.Count;
            EquipWeapon(prevIndex);
    }

    private void EquipWeaponByReference(GameObject weapon)
    {
        int index = ownedWeapons.IndexOf(weapon);
        if (index != -1)
        {
            EquipWeapon(index);
        }
    }

    private void EquipWeapon(int index)
    {
        if (index < 0 || index >= ownedWeapons.Count || index == currentIndex)
        {
            return;
        }

        //deactivate current weapon
        if (currentIndex >= 0 && currentIndex < ownedWeapons.Count)
        {
            ownedWeapons[currentIndex].SetActive(false);
        }

        previousIndex = currentIndex;
        currentIndex = index;
        ownedWeapons[currentIndex].SetActive(true);
    }

    public bool IsUnlocked(GameObject weapon)
    {
        return ownedWeapons.Contains(weapon);
    }
}
