using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    public PlayableCharacter PC;
    public Character Player;
    public BattleManager BattleMan;
    public GameObject TargetEnemy;
    public int target;
    public Animator animator;
    //public List<int> targetList = new List<int>();

    public enum PlayerActionState
    {
        Wait,
        Action,
        Attack,
        Dead,
    }

    public PlayerActionState currentState;

    void Start()
    {
        BattleMan = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        currentState = PlayerActionState.Wait;
        animator = GetComponent<Animator>();
        hitAttack();
        SomeSkill();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (PlayerActionState.Wait):
                {
                    animator.SetBool("Idle", true);
                    animator.SetBool("Attack", false);
                    break;
                }
            case (PlayerActionState.Action):
                {
                    //Debug.Log("checker");
                    attackTarget();
                    animator.SetBool("Idle", false);
                    animator.SetBool("Attack", true);
                    currentState = PlayerActionState.Attack;
                    break;
                }
            case (PlayerActionState.Attack):
                {
                    animator.SetBool("Idle", false);
                    animator.SetBool("Attack", true);
                    currentState = PlayerActionState.Wait;
                    break;
                }
        }
    }

    public void doAttack(List<int> targetList)
    {
        Debug.Log("targetList size " + targetList.Count);   
        for (int i = 0; i < targetList.Count; i++)
        {
            Debug.Log("target is " + targetList[i]);
            Debug.Log("damage dealth" + Player.GetStr());
            BattleMan.EnemyInGame[targetList[i]].GetComponent<EnemyCharacter>().causeDamage(Player.GetStr());
        }
    }

    public void hitAttack()
    {
        Skill hit = new Skill();
        hit.name = "hit";
        hit.damage = 2 * PC.currentStr;
        hit.speedcost = 1;
        LoadSkill(hit);
    }

    public void SomeSkill()
    {
        Skill magic = new Skill();
        magic.name = "Special Skill";
        magic.damage = PC.currentInt / 5;
        magic.speedcost = 2;
        LoadSkill(magic);
    }

    public void LoadSkill(Skill value)
    {
        PC.SkillList.Add(value);
    }

    public void playerQueue(int target)
    {
        BattleTurn attacker = new BattleTurn();
        attacker.Attacker = Player.name;
        attacker.AttackingGameObject = Player.gameObject;
        attacker.AttackTarget = BattleMan.EnemyInGame[target];
        BattleMan.AttackQueue(attacker);
    }

    public void attackTarget()
    {
        //Debug.Log("Reached AttackTarget");
        TargetEnemy.GetComponent<EnemyCharacter>().causeDamage(Player.GetStr());
    }

}
