using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class PhoneButton : MonoBehaviour, Interactable
{
    bool Clicking = false;
    [SerializeField] AudioClip phoneSFX;

    bool Used = true;

    private void Start()
    {
        StartCoroutine(Call());
    }

    IEnumerator Call() 
    {
        yield return new WaitForSeconds(30);
        GameManager.gameManager.audioManager.PlayAudio(phoneSFX, true);
        Used = false;
    }

    public void OnClick()
    {
        if(!Clicking)
            StartCoroutine(ClickEffect());
        if (!Used)
        {
            GameManager.gameManager.CompleteObejctive();
            Used = true;
        }
    }

    IEnumerator ClickEffect() 
    {
        Clicking = true;
        transform.position += Vector3.left * 0.05f;
        yield return new WaitForSeconds(0.05f);
        transform.position += Vector3.right * 0.05f;
        Clicking = false;
    }
}
