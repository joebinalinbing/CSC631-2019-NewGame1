﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterClasses {
    public const int Warrior = 1;
    public const int Hunter = 2;
    public const int Mage = 3;
    public const int Orc = 4;
    public const int Skeleton = 5;
    public const int Boss = 6;
};

public class CharacterClassDefiner : MonoBehaviour
{
    #region Variables
    // Referenced conponents.    
    Player character;
    Animator animator;
    Transform characterAvatar;

    // Enemy variation variables. (Do not change)
    int minOrcRange = 4;
    int maxOrcRange = 10;
    int minSkeletonRange = 12;
    int maxSkeletonRange = 17;

    // Weapon variation variables.
    int weaponNum;

    // Weapon objects.
    public GameObject sword;
    public GameObject bow;
    public GameObject staff;
    public GameObject axe;
    public GameObject club;
    #endregion

    void Start()
    {
        // Get required components.
        character = GetComponent<Player>();
        animator = GetComponent<Animator>();

        // Hide all weapon objects.
        hideAllWeapons();
    }

    void Update()
    {
        // For testing.
        if (Input.GetKeyDown("a")) SetCharacterClass(CharacterClasses.Warrior);
        if (Input.GetKeyDown("s")) SetCharacterClass(CharacterClasses.Hunter);
        if (Input.GetKeyDown("d")) SetCharacterClass(CharacterClasses.Mage);
        if (Input.GetKeyDown("f")) SetCharacterClass(CharacterClasses.Orc);
        if (Input.GetKeyDown("g")) SetCharacterClass(CharacterClasses.Skeleton);
        if (Input.GetKeyDown("h")) SetCharacterClass(CharacterClasses.Boss);
    }

    // SetCharacterClass(int characterID), SetCharacterModel(int modelID), SetCharacterWeapon(int weaponID)
    #region Main Methods
    public void SetCharacterClass(int characterID)
    {
        if (characterID == CharacterClasses.Warrior) // Warrior
        {
            SetCharacterModel(CharacterClasses.Warrior);
            SetCharacterWeapon(CharacterClasses.Warrior);
        }
        else if (characterID == CharacterClasses.Hunter) // Hunter
        {
            SetCharacterModel(CharacterClasses.Hunter);
            SetCharacterWeapon(CharacterClasses.Hunter);
        }
        else if (characterID == CharacterClasses.Mage) // Mage
        {
            SetCharacterModel(CharacterClasses.Mage);
            SetCharacterWeapon(CharacterClasses.Mage);
        }
        else if (characterID == CharacterClasses.Orc) // Orc
        {
            SetCharacterModel(CharacterClasses.Orc);
            //weaponNum = Random.Range(4, 6);
            //SetCharacterWeapon(weaponNum);
        }
        else if (characterID == CharacterClasses.Skeleton) // Skeleton
        {
            SetCharacterModel(CharacterClasses.Skeleton);
            //weaponNum = Random.Range(4, 6);
            //SetCharacterWeapon(weaponNum);
        }
        else // Boss
        {
            SetCharacterModel(CharacterClasses.Boss);
            SetCharacterWeapon(CharacterClasses.Boss);
        }
    }

    public void SetCharacterModel(int modelID)
    {
        Transform currentCharacterAvatar = transform.GetChild(GetActiveChracterModel());
        currentCharacterAvatar.gameObject.SetActive(false);

        if (modelID == 1) characterAvatar = transform.Find("Chr_Dungeon_KnightMale_01");
        else if (modelID == 2) characterAvatar = transform.Find("Chr_Vikings_ShieldMaiden_01");
        else if (modelID == 3) characterAvatar = transform.Find("Chr_Fantasy_Wizard_01");
        else if (modelID == 4) characterAvatar = transform.GetChild(GetRandomOrc());
        else if (modelID == 5) characterAvatar = transform.GetChild(GetRandomSkeleton());
        else characterAvatar = transform.Find("Chr_Western_Woman_01");

        characterAvatar.gameObject.SetActive(true);
    }

    public void SetCharacterWeapon(int weaponID)
    {
        hideAllWeapons();

        if (weaponID == 1) // Sword
        {
            animator.SetInteger("Weapon", 1);
            sword.SetActive(true);
        }
        else if (weaponID == 2) // Bow
        {
            animator.SetInteger("Weapon", 4);
            bow.SetActive(true);
        }
        else if (weaponID == 3) // Staff
        {
            animator.SetInteger("Weapon", 6);
            staff.SetActive(true);
        }
        else if (weaponID == 4) // Axe
        {
            animator.SetInteger("Weapon", 3);
            axe.SetActive(true);
        }
        else if (weaponID == 5) // Club
        {
            animator.SetInteger("Weapon", 9);
            club.SetActive(true);
        }
        else // Unarmed
        {
            animator.SetInteger("Weapon", 0);
        }

        animator.SetTrigger("InstantSwitchTrigger");
    }
    #endregion

    // GetActiveChracterModel(), GetRandomOrc(), GetRandomSkeleton(), hideAllWeapons()
    #region Helper Methods
    int GetActiveChracterModel()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.active) return i;
        }
        return -1;
    }

    int GetRandomOrc()
    {
        return Random.Range(minOrcRange, maxOrcRange);
    }

    int GetRandomSkeleton()
    {
        return Random.Range(minSkeletonRange, maxSkeletonRange);
    }

    void hideAllWeapons()
    {
        if (sword != null)
        {
            sword.SetActive(false);
        }
        if (bow != null)
        {
            bow.SetActive(false);
        }
        if (staff != null)
        {
            staff.SetActive(false);
        }
        if (axe != null)
        {
            axe.SetActive(false);
        }
        if (club != null)
        {
            club.SetActive(false);
        }
    }
    #endregion
}
