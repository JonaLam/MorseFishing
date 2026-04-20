using UnityEngine;

[CreateAssetMenu(fileName = "MorseSignalGameObjective", menuName = "Scriptable Objects/MorseSignalGameObjective")]
public class MorseSignalGameObjective : GameObjective
{
    public MorseVerbs[] morseVerbs;
    [SerializeField] AudioClip audioClip;

    [SerializeField] string objectiveText;

    public override void CompleteObjective()
    {
        {
            foreach (var item in morseVerbs)
            {
                item.UnassignMorseVerb(GameManager.gameManager.morseCodeManager);
            }
        }
    }

    public override void SetUpObjective()
    {
        if(audioClip != null)
            GameManager.gameManager.audioManager.PlayAudio(audioClip);

        GameManager.gameManager.currentObjectivePaper.SetObjective(objectiveText);

        foreach (var item in morseVerbs)
        {
            item.AssignMorseVerb(GameManager.gameManager.morseCodeManager);
        }
    }
}
