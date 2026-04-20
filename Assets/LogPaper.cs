using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class LogPaper : MonoBehaviour
{
    Queue<string> codes;

    [SerializeField] TextMeshPro text;

    private void Start()
    {
        codes = new Queue<string>();
    }
    public void AddNewCode(string newCode) 
    {
        if (codes.Count == 5)
            codes.Dequeue();

        codes.Enqueue(newCode);

        string fullPaperText = "<b>Code Log</b> <br> <br>";

        foreach (var item in codes)
        {
            fullPaperText += item + " = " + MorseTranslator.TranslateString(item);
            fullPaperText += "<br> <br>";
        }

        text.text = fullPaperText;
    }
}
