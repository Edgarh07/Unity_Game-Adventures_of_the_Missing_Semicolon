using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public string EnemyName;
    public enum Typing
    {
        Fire,
        Grass,
        Water,
    }

    public enum Ranking
    {
        Normal = 0,
        Boss = 1,
    }

    public int[] Health = { 70, 300 };
    public int maxHealth;
    public int currentHealth;

    public int[] Strength = { 5, 20 };
    public int currentStr;

    public int[] Intellect = { 3, 5 };
    public int currentInt;

    public int isBoss;

    //Enemy only attacks one per turn   
}
