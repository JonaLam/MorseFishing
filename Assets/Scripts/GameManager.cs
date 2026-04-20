using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    GameObjective currentObjective;
    public MorseCodeManager morseCodeManager;
    public DivingManager divingManager;
    public CurrentObjectivePaper currentObjectivePaper;
    public AudioManager audioManager;

    [SerializeField] GameObjective testObjective;

    private void Awake()
    {
        gameManager = this;
        currentObjective = testObjective;
        currentObjective.SetUpObjective();
    }

    public void CompleteObejctive() 
    {
        currentObjective.CompleteObjective();

        currentObjective = currentObjective.nextObjective;
        currentObjective.SetUpObjective();
    }

}
