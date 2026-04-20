using UnityEngine;

[CreateAssetMenu(fileName = "FishingGameObjective", menuName = "Scriptable Objects/FishingGameObjective")]
public class FishingGameObjective : GameObjective
{
    [System.Serializable]
    public struct FishBundle 
    {
        public Fish fish;
        public int amount;
    }

    public FishBundle[] fish;
    public MorseVerbs[] morseVerbs;

    [SerializeField] bool autoAttack;
    [SerializeField] bool spawnFish;

    [SerializeField] AudioClip audioClip;

    [SerializeField] int starFishAmount;

    [SerializeField] bool finalFish;

    public override void CompleteObjective()
    {
        GameManager.gameManager.divingManager.FinishDive();
        GameManager.gameManager.divingManager.diverBehaviour.onGetFish -= CheckFish;

        foreach (var item in morseVerbs)
        {
            item.UnassignMorseVerb(GameManager.gameManager.morseCodeManager);
        }
    }

    public override void SetUpObjective()
    {
        if (audioClip != null)
            GameManager.gameManager.audioManager.PlayAudio(audioClip);

        GameManager.gameManager.divingManager.initalStarFish = starFishAmount;
        GameManager.gameManager.divingManager.StartDive();
        GameManager.gameManager.divingManager.diveing = true;
        GameManager.gameManager.divingManager.diverBehaviour.autoAttack = autoAttack;
        GameManager.gameManager.divingManager.spawnFish = spawnFish;
        GameManager.gameManager.divingManager.initalStarFish = starFishAmount;

        if (finalFish) 
        {
            GameManager.gameManager.divingManager.FinalFish();
        }

       GameManager.gameManager.divingManager.diverBehaviour.onGetFish += CheckFish;

        UpdateObjectiveText();

        foreach (var item in morseVerbs)
        {
            item.AssignMorseVerb(GameManager.gameManager.morseCodeManager);
        }
    }

    

    void UpdateObjectiveText() 
    {
        string objectiveText = "";

        foreach (var item in fish)
        {
            int fishAmount = 0;

            if (GameManager.gameManager.divingManager.diverBehaviour.fishInventory.ContainsKey(item.fish)) 
            {
                fishAmount = GameManager.gameManager.divingManager.diverBehaviour.fishInventory[item.fish];
            }

            objectiveText += "Catch " + item.amount + " " + item.fish.name + "(" + fishAmount + ")";
            objectiveText += "<br>";
        }

        GameManager.gameManager.currentObjectivePaper.SetObjective(objectiveText);
    }

    public void CheckFish() 
    {
        DiverBehaviour diverBehaviour = GameManager.gameManager.divingManager.diverBehaviour;

        UpdateObjectiveText();
        foreach (var item in fish)
        {
            if (diverBehaviour.fishInventory.ContainsKey(item.fish)) 
            {
               if( diverBehaviour.fishInventory[item.fish] >= item.amount) 
                {
                    continue;
                }
            }

            return;
        }

        CompleteObjective();
    }
}
