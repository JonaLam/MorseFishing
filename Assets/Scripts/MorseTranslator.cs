using UnityEngine;

public static class MorseTranslator
{
    public static string TranslateString(string morseString) 
    {
        string translatedString = "";

        string morseWord = "";

        foreach (char item in morseString)
        {
            if(item == '/') 
            {
                translatedString += GetLetterFromMorse(morseWord);
                morseWord = "";
                translatedString += " ";
                continue;
            }

            if (item == ' ') 
            {
                translatedString += GetLetterFromMorse(morseWord);
                morseWord = "";
                continue;
            }

            morseWord += item;
        }
        translatedString += GetLetterFromMorse(morseWord);

        return translatedString;
    }

    public static char GetLetterFromMorse(string morse) 
    {

        switch (morse) 
        {
            default:
                return '?';

            case ".-":
                return 'A';

            case "-...":
                return 'B';

            case "-.-.":
                return 'C';

            case "-..":
                return 'D';

            case ".":
                return 'E';

            case "..-.":
                return 'F';

            case "--.":
                return 'G';

            case "....":
                return 'H';

            case "..":
                return 'I';

            case ".---":
                return 'J';

            case "-.-":
                return 'K';

            case ".-..":
                return 'L';

            case "--":
                return 'M';

            case "-.":
                return 'N';

            case "---":
                return 'O';

            case ".--.":
                return 'P';

            case "--.-":
                return 'Q';

            case ".-.":
                return 'R';

            case "...":
                return 'S';

            case "-":
                return 'T';

            case "..-":
                return 'U';

            case "...-":
                return 'V';

            case ".--":
                return 'W';

            case "-..-":
                return 'X';

            case "-.--":
                return 'Y';

            case "--..":
                return 'Z';
        }
    }
}
