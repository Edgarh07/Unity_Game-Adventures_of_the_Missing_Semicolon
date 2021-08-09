using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject Target;
    public void openPanel(bool isHighlighted)
    {
        Target.SetActive(isHighlighted);
    }
}
