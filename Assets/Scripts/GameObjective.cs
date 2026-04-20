using UnityEngine;

public abstract class GameObjective : ScriptableObject
{
    public abstract void SetUpObjective();

    public abstract void CompleteObjective();

    public GameObjective nextObjective;
}
