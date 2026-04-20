using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;
public class MorseCodeManager : MonoBehaviour
{
    public class MorseCodeData 
    {
        public string morseString;
    }

    bool pressed = false;
    int timeSinceChange;

    [SerializeField] MorseCodeDisplay codeDisplay;

    bool morseActive = false;

    MorseCodeData currentData;

    [SerializeField] Metronome metronome;

    MorseVerbs currentMorseVerb;

    public delegate void SendMorse(string morseString);
    public SendMorse onMorseSent;

    [SerializeField] LogPaper logPaper;

    private void FixedUpdate()
    {
        if (!morseActive)
            return;

        timeSinceChange++;

        if(!pressed)
            if(timeSinceChange / metronome.timeLength > 10) 
            {
                SubmitMorseString();
                currentData = null;
                morseActive = false;
                timeSinceChange = 0;
                codeDisplay.ClearMorseCode();
            }
       
    }

    public void PressMorseCode() 
    {
        if (!morseActive) 
        {
            morseActive = true;
            currentData = new MorseCodeData();
            codeDisplay.morseCode = currentData;
        }

        pressed = true;

        int timeUnits = timeSinceChange / metronome.timeLength;


        if(timeUnits >= 2)
            if (timeUnits >= 6)
                currentData.morseString += "/";
            else
                currentData.morseString += " ";

        timeSinceChange = 0;
    }

    public void ReleaseMorseCode() 
    {
        pressed = false;

        int timeUnits = timeSinceChange / metronome.timeLength;

        if(timeUnits >= 2)
            currentData.morseString += "-";
        else
            currentData.morseString += ".";

        timeSinceChange = 0;
    }

    public void SubmitMorseString() 
    {
        logPaper.AddNewCode(currentData.morseString);

        if(onMorseSent != null) 
        {
            onMorseSent(currentData.morseString);
        }
        
    }

    public void AssignMorseVerb(MorseVerbs morse) 
    {
        currentMorseVerb = morse;
    }
}
