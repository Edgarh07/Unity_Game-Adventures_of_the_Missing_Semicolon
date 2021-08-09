using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public PlayableCharacter PC;
    public Text Name;
    public Text Health;
    void Start()
    {
        PC.currentStr = PC.Strength[PC.currentLevel - 1] * PC.strMux;
        PC.currentInt = PC.Intellect[PC.currentLevel - 1] * PC.intMux;
        PC.statSpd = PC.Speed[PC.currentLevel - 1] * PC.spdMux;
        PC.maxHealth = (PC.healthMux * (PC.currentStr)) + PC.Health[PC.currentLevel - 1];
        PC.currentHealth = PC.maxHealth;
        Name.text = PC.CharacterName;
        Health.text = "HP " + PC.currentHealth + "/" + PC.maxHealth;
        PC.currentSpd = PC.statSpd;
        //Debug.Log(PC.CharacterName + " Health " +PC.maxHealth);
        //Debug.Log(PC.CharacterName + " Strength " + PC.currentStr);
        //Debug.Log(PC.CharacterName + " Intellect " + PC.currentInt);
        //Debug.Log(PC.CharacterName + " Speed " + PC.currentSpd);
    }

    private void Update()
    {
        Health.text = "HP " + PC.currentHealth + "/" + PC.maxHealth;
    }

    public int GetHealth()
    {
        return PC.currentHealth;
    }

    public int GetStr()
    {
        return PC.currentStr;
    }

    public int GetInt()
    {
        return PC.currentInt;
    }

    public int GetSpeed()
    {
        return PC.statSpd;
    }

    public int GetLvl()
    {
        return PC.currentLevel;
    }

    public bool useSpeed()      //used to subtract speed and return if there is any speed
    {
        if (PC.currentSpd <= 0)
        {
            return false;
        }
        else
        {
            PC.currentSpd--;
            return true;
        }
    }
    public bool causeDamage(int dmg)     //returns true if target is killed by this action
    {
        PC.currentHealth = PC.currentHealth - dmg;
        if (PC.currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //current turn update;
}
