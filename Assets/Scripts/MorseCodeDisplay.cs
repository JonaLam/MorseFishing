using UnityEngine;
using TMPro;

public class MorseCodeDisplay : MonoBehaviour
{
    public MorseCodeManager.MorseCodeData morseCode;
    public TextMeshPro text;
    string morseCodeText;

    public void FixedUpdate()
    {
        if (morseCode == null)
            return;

        text.text = morseCode.morseString;
    }

    public void ClearMorseCode() 
    {
        morseCode = null;
        text.text = "";
    }

}
