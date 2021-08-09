using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanics : MonoBehaviour
{
    public BattleManager BattleMan;
    public EnemyCharacter Enemy;
    public bool myTurn; //just to check we are using true
    public int Target;
    public bool actionperformed = false;
    private float moveSpeed = 4.0f;
    public GameObject CharacterToAttack;

    public Vector2 startingPosition;
    public float targetPositionX;
    public float targetPositionY;
    public Vector2 targetPosition;

    public enum PlayerActionState
    {
        SelectTarget,
        Idle,
        QueueAction,
        Waiting,
        Action,
        Dead,
    }

    public PlayerActionState currentState;

    void Start()
    {
        currentState = PlayerActionState.SelectTarget;
        startingPosition = transform.position;
        myTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (PlayerActionState.SelectTarget):
                {
                    Target = Random.Range(0, BattleMan.CharactersInGame.Count);
                    Debug.Log("targetPosition" + targetPosition);
                    currentState = PlayerActionState.Idle;
                    break;
                }
            case (PlayerActionState.Idle):
                {
                    transform.position = startingPosition;
                    if (myTurn)
                    {
                        currentState = PlayerActionState.QueueAction;
                    }
                    break;
                }
            case (PlayerActionState.QueueAction):
                {
                    SelectAction();
                    myTurn = false;
                    currentState = PlayerActionState.Waiting;
                    break;
                }
            case (PlayerActionState.Waiting):
                {
                    //waiting for battle manager to make actions and jump to Action State
                    break;
                }
            case (PlayerActionState.Action):
                {
                    StartCoroutine(MakeAction());
                    moveToAttack(targetPosition);
                    damageTarget();
                    currentState = PlayerActionState.SelectTarget;
                    myTurn = true;
                    break;
                }
            case (PlayerActionState.Dead):
                {
                    break;
                }
        }
    }

    public void SelectAction()
    {
        BattleTurn UnitAttack = new BattleTurn();
        UnitAttack.Attacker = Enemy.name;
        //UnitAttack.Type = "Enemy";
        UnitAttack.AttackingGameObject = this.gameObject;
        UnitAttack.AttackTarget = BattleMan.CharactersInGame[Target];
        BattleMan.AttackQueue(UnitAttack);
        Debug.Log(Target);
    }

    private IEnumerator MakeAction()
    {
        if (actionperformed)
        {
            yield break;
        }

        actionperformed = true;

        targetPositionX = BattleMan.CharactersInGame[Target].gameObject.transform.position.x;
        targetPositionY = BattleMan.CharactersInGame[Target].gameObject.transform.position.y;
        targetPosition = new Vector2(targetPositionX, targetPositionY);

        moveToAttack(targetPosition);

        //Vector2 TargetCharacter = new Vector2(CharacterToAttack.transform.position.x + 1.5f, CharacterToAttack.transform.position.y);


        //Vector2 TargetCharacter = new Vector2()

    }

    public void moveToAttack(Vector2 location)
    {
        transform.position = Vector2.MoveTowards(startingPosition, location, moveSpeed * Time.deltaTime);
        Debug.Log("location after move" + gameObject.transform.position);
    }
    public void damageTarget()
    {
        Debug.Log(BattleMan.CharactersInGame[Target]);
        if (BattleMan.CharactersInGame[Target].GetComponent<Character>().causeDamage(Enemy.GetStr()))
        {
            //do something if target is dead
        }
    }
}
