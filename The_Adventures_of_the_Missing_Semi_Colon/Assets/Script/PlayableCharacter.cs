using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayableCharacter
{
    public string CharacterName;

    public int[] Health = { 50, 100, 150, 200, 250 };
    public int healthMux;
    public int maxHealth;
    public int currentHealth;

    public int[] Strength = { 5, 9, 14, 17, 18 };
    public int strMux;
    public int currentStr;

    public int[] Intellect = { 2, 4, 6, 8, 10 };
    public int intMux;
    public int currentInt;

    public int[] Speed = { 1, 1, 1, 2, 2 };
    public int spdMux;
    public int statSpd;
    public int currentSpd;

    public int[] Exp = { 0, 10, 20, 30, 40 };
    public int levelExp;
    public int currentExp;
    public int currentLevel;

    public List<Skill> SkillList = new List<Skill>();
}