using UnityEngine;

[CreateAssetMenu(fileName = "CompleteObjectiveVerbAction", menuName = "Scriptable Objects/CompleteObjectiveVerbAction")]
public class CompleteObjectiveVerbAction : MorseVerbAction
{
    public override void DoAction()
    {
        GameManager.gameManager.CompleteObejctive();
    }
}
