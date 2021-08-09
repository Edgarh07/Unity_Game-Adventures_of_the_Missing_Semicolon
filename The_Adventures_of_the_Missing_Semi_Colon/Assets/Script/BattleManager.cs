using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private int PlayerTotalSpeed;
    PlayerMechanics PlayMec;
    //public List<Character> Players;
    public int speedValue;
    public List<int> targetList = new List<int>();

    public enum Actions
    {
        Wait,
        ReadyAction,
        ExecuteAction,
    }

    public Actions ActionState;

    public List<BattleTurn> QueueList = new List<BattleTurn>();
    public List<GameObject> CharactersInGame = new List<GameObject>();
    public List<GameObject> EnemyInGame = new List<GameObject>();

    public enum CharacterOption
    {
        Initiate,
        Waiting,
        AttackInput,
        TargetInput,
        Execute
    }

    public CharacterOption CharacterInput;

    public List<GameObject> CharactersInPlay = new List<GameObject>();
    private BattleTurn PlayerTurn;

    // Start is called before the first frame update
    void Start()
    {
        ActionState = Actions.Wait;
        EnemyInGame.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        CharactersInGame.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        switch (ActionState)
        {
            case (Actions.Wait):
                {
                    speedValue = speedCheck();
                    //Debug.Log("SpeedValue " + speedValue);
                    //ActionState = Actions.ReadyAction;
                    break;
                }
            case (Actions.ReadyAction):
                {
                    GameObject performer = GameObject.Find(QueueList[0].Attacker);
                    for (int i = 0; i <= speedValue - 1; i++)
                    {
                        PlayerMechanics PlayMec = performer.GetComponent<PlayerMechanics>();
                        PlayMec.TargetEnemy = QueueList[i].AttackTarget;
                        PlayMec.currentState = PlayerMechanics.PlayerActionState.Action;
                    }
                    for (int i = speedValue; i <= QueueList.Count; i++)
                    {
                        EnemyMechanics EnemMec = performer.GetComponent<EnemyMechanics>();
                        EnemMec.CharacterToAttack = QueueList[i].AttackTarget;
                        EnemMec.currentState = EnemyMechanics.PlayerActionState.Action;
                    }
                    break;
                }
            case (Actions.ExecuteAction):
                {
                    for (int i = 3; i <= QueueList.Count-1; i++) //used for the players to attack first as they are always 3rd+ attackers
                    {
                        GameObject performer = GameObject.Find(QueueList[i].Attacker);
                        PlayerMechanics PlayMec = performer.GetComponent<PlayerMechanics>();
                        PlayMec.TargetEnemy = QueueList[i].AttackTarget;
                        //PlayMec.doAttack(targetList);
                        PlayMec.currentState = PlayerMechanics.PlayerActionState.Action;
                    }
                    for (int i = 0; i <= 2; i++) //Used to determine enemy attacks
                    {
                        GameObject performer = GameObject.Find(QueueList[i].Attacker);
                        EnemyMechanics EnemMec = performer.GetComponent<EnemyMechanics>();
                        EnemMec.currentState = EnemyMechanics.PlayerActionState.Action;
                    }
                    QueueList = new List<BattleTurn>();
                    targetList = new List<int>();
                    Debug.Log("number of elements in Queuelist" + QueueList.Count);
                    ActionState = Actions.Wait;
                    break;
                }
        }
    }

    public void executeAttack()
    {
        Debug.Log(QueueList.Count);
        ActionState = Actions.ExecuteAction;
    }
    public void AttackQueue(BattleTurn Queue)
    {
        QueueList.Add(Queue);
    }

    public int speedCheck()
    {
        int SpeedCount = 0;
        for (int i = 0; i < CharactersInGame.Count; i++)
        {
            SpeedCount += CharactersInGame[i].GetComponent<Character>().GetSpeed();
        }
        return SpeedCount;
    }

    public int selectedPlayer = -1; //used to determine the current player selected

    public void queueAttack(int enemy)
    {
        if (selectedPlayer != -1)   //only adds a new turn if there is a currently selected player
        {
            Debug.Log("target enemy" + enemy);
            PlayerMechanics PlayMec = CharactersInGame[selectedPlayer].GetComponent<PlayerMechanics>();
            PlayMec.playerQueue(enemy);
            targetList.Add(enemy);
            Debug.Log("the sadadadasdadada of playerMec target list " + targetList.Count);
            selectedPlayer = -1;
        }
    }

    public void selectPlayer(int player)
    {
        if (CharactersInGame[player].GetComponent<Character>().useSpeed())
        {
            selectedPlayer = player;    //only activates if the player has enough speed to make another turn
        }
        else
        {
            //alert player it failed
        }

    }

    public void PrepMove()
    {

    }
}
