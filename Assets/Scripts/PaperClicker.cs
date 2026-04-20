using UnityEngine;

public class PaperClicker : MonoBehaviour, Interactable
{

    [SerializeField] bool mainPile;
    [SerializeField] PaperManager paperManager;
    public void OnClick()
    {
        if (mainPile)
            paperManager.MoveTopPaper();
        else
            paperManager.MoveDiscardedPaper();
    }
}
