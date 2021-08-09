using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    public EnemyMechanics Enem;
    public GameObject Target;

    public void highlightEnemy(bool isHighlighted)
    {
        Target.SetActive(isHighlighted);
    }

}
