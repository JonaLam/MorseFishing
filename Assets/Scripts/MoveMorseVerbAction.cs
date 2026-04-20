using UnityEngine;

[CreateAssetMenu(fileName = "MoveMorseVerbAction", menuName = "Scriptable Objects/MoveMorseVerbAction")]
public class MoveMorseVerbAction : MorseVerbAction
{
    public Vector2 dir;

    public override void DoAction()
    {
        GameManager.gameManager.divingManager.diverBehaviour.MoveNormal(dir);
    }
}
