using UnityEngine;
using TMPro;

public class Metronome : MonoBehaviour
{
    int tickTimer;
    public int timeLength;
    [SerializeField] Transform hand;
    bool left;

    [SerializeField] TextMeshPro timeText;

    private void FixedUpdate()
    {
        tickTimer++;

        if(left)
            hand.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(50, -50, (float)tickTimer / timeLength));
        else
            hand.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-50, 50, (float)tickTimer / timeLength));

        if (tickTimer > timeLength)
        {
            tickTimer = 0;
            left = !left;
        }
    }

    public void EditTimeLength(int amount) 
    {
        timeLength += amount;
        timeText.text = timeLength * 20 + "ms";
    }
}
