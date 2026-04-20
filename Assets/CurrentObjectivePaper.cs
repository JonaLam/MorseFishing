using UnityEngine;
using TMPro;

public class CurrentObjectivePaper : MonoBehaviour
{
    [SerializeField] TextMeshPro text; 

    public void SetObjective(string objective) 
    {
        text.text = "CURRENT OBJECTIVE : <br> <br>" + objective;
    }
}
