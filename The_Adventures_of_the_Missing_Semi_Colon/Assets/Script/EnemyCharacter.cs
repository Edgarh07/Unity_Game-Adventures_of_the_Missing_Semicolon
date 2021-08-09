using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharacter : MonoBehaviour
{
    public Enemy Mob;
    public Text Name;
    public Text Health;
    void Start()
    {
        Mob.maxHealth = Mob.Health[Mob.isBoss];
        Mob.currentHealth = Mob.maxHealth;
        Mob.currentInt = Mob.Intellect[Mob.isBoss];
        Mob.currentStr = Mob.Strength[Mob.isBoss];
        //Debug.Log(Mob.EnemyName + " Health " + Mob.currentHealth);
        //Debug.Log(Mob.EnemyName + " Strength " + Mob.currentStr);
        //Debug.Log(Mob.EnemyName + " Intellect " + Mob.currentInt);
        Name.text = Mob.EnemyName;
        Health.text = "HP " + Mob.currentHealth + "/" + Mob.maxHealth;
    }

    private void Update()
    {
        Health.text = "HP " + Mob.currentHealth + "/" + Mob.maxHealth;
    }

    public int GetHealth()
    {
        return Mob.currentHealth;
    }

    public int GetStr()
    {
        return Mob.currentStr;
    }

    public int GetInt()
    {
        return Mob.currentInt;
    }


    public bool causeDamage(int dmg)     //returns true if target is killed by this action
    {
        Mob.currentHealth = Mob.currentHealth - dmg;
        if (Mob.currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
