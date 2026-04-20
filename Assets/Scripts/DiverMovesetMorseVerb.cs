using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DiverMovesetMorseVerb", menuName = "Scriptable Objects/DiverMovesetMorseVerb")]
public class DiverMovesetMorseVerb : MorseVerbs
{
    public MorseVerbs[] morseVerbs;

    public override void OnMorseCall(string morse)
    {
        string word = "";

        Queue<MorseVerbs> verbList = new Queue<MorseVerbs>();

        foreach (var item in morse)
        {
            if(item == '/') 
            {
                verbList.Enqueue( GetVerbFromString(word));
                word = "";
                continue;
            }
            word += item;
        }
        verbList.Enqueue( GetVerbFromString(word));

        GameManager.gameManager.divingManager.diverBehaviour.SetMorseVerbs(  verbList);
    }

    MorseVerbs GetVerbFromString(string morse) 
    {
        foreach (var item in morseVerbs)
        {
            if (item.code.Contains(MorseTranslator.TranslateString( morse)))
                return item;
        }
        return null;
    }
}
