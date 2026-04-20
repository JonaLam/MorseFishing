using UnityEngine;

public class MorseButton : MonoBehaviour, Interactable
{
    bool pressed;

    [SerializeField] MorseCodeManager morseCodeManager;

    public void OnClick()
    {
        pressed = true;
        transform.position += Vector3.down * 0.1f;
        morseCodeManager.PressMorseCode();
    }

    private void Update()
    {
        if (pressed) 
        {
            if (Input.GetMouseButtonUp(0)) 
            {
                pressed = false;
                transform.position += Vector3.up * 0.1f;
                morseCodeManager.ReleaseMorseCode();
            }
        }
    }


}
