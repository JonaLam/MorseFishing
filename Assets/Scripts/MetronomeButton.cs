using UnityEngine;

public class MetronomeButton : MonoBehaviour, Interactable
{
    [SerializeField] int change;
    [SerializeField] Metronome metronome;

    public void OnClick()
    {
        metronome.EditTimeLength(change);
    }
}
