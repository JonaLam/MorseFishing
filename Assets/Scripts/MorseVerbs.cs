using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class MorseVerbs : ScriptableObject
{
    public MorseVerbAction[] morseVerbActions;

    public List<string> code;

    [SerializeField] AudioClip audioClip;

    public void AssignMorseVerb(MorseCodeManager morseCodeManager) 
    {
        morseCodeManager.onMorseSent += OnMorseCall;
    }

    public virtual void OnMorseCall(string morse) 
    {
        if (!code.Contains( MorseTranslator.TranslateString(morse))) 
        {
            GameManager.gameManager.audioManager.PlayAudio(audioClip);
            return;
        }

        foreach (var item in morseVerbActions)
        {
            item.DoAction();
        }
    }

    public void UnassignMorseVerb(MorseCodeManager morseCodeManager) 
    {
        morseCodeManager.onMorseSent -= OnMorseCall;
    }
}
