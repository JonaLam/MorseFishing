using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiverBehaviour : MonoBehaviour
{
    [SerializeField] LayerMask twoDLayer;
    [SerializeField] float autoAttackSize, attackSize;

    [SerializeField] Transform topLeftBorder, bottomRightBorder;

    public Dictionary<Fish, int> fishInventory;

    public delegate void FishDelegate();
    public FishDelegate onGetFish;

    public bool actionable = true;

    [SerializeField] Transform defualtPos;

    Queue<MorseVerbs> morseVerbs;

    public float speed;

    public bool autoAttack = true;

    [SerializeField] AudioClip audioSourceAttack, AudioClipTarnation;

    private void Start()
    {
        fishInventory = new Dictionary<Fish, int>();
        morseVerbs = new Queue<MorseVerbs>();
    }

    public void SetMorseVerbs(Queue<MorseVerbs> newMorseVerbs) 
    {
        morseVerbs = newMorseVerbs;

        foreach (var item in newMorseVerbs)
        {
            if (item == null)
                continue;
            return;
        }

        GameManager.gameManager.audioManager.PlayAudio(AudioClipTarnation);
    }

    private void FixedUpdate()
    {
        if(actionable && autoAttack)
            AutoAttack();

        if(actionable && morseVerbs.Count != 0) 
        {
            MorseVerbs morseVerbs = this.morseVerbs.Dequeue();

            if (morseVerbs == null)
                return;


            if (morseVerbs.morseVerbActions[0] is MoveMorseVerbAction) 
            {
                MoveMorseVerbAction moveMorseVerbAction = morseVerbs.morseVerbActions[0] as MoveMorseVerbAction;

                Vector2 dir = moveMorseVerbAction.dir;

                if (this.morseVerbs.Count != 0) 
                {
                    MorseVerbs nextMorseVerb = this.morseVerbs.Peek();

                    if (nextMorseVerb != null)
                    {
                        DistanceMorseVerb distanceMorseVerb = nextMorseVerb.morseVerbActions[0] as DistanceMorseVerb;

                        if (distanceMorseVerb != null)
                            dir *= distanceMorseVerb.multiplier;
                    }
                }

                MoveNormal(dir);
            }

            if(morseVerbs.morseVerbActions[0] is AttackMorseAction) 
            {
                AttackMorseAction attackMorseAction = morseVerbs.morseVerbActions[0] as AttackMorseAction;

                Attack(attackMorseAction.fish);
            }
        }
    }

    public void MoveNormal(Vector2 dir)
    {
        if(actionable)
            StartCoroutine(MoveRoutine((Vector2)transform.position + dir, 0.1f));
    }

    public void MoveToStart() 
    {
        StartCoroutine(MoveRoutine(defualtPos.position, 0.1f));
        fishInventory.Clear();
    }

    public void MoveOut() 
    {
        StartCoroutine(MoveRoutine(defualtPos.position + Vector3.up * 10, 0.1f));
    }

    public void GetFish(Fish fish) 
    {
        if (!fishInventory.ContainsKey(fish)) 
        {
            fishInventory.Add(fish, 0);
        }

        fishInventory[fish]++;

        if (onGetFish != null)
            onGetFish();
    }

    public void AutoAttack() 
    {
       Collider[] col = Physics.OverlapSphere(transform.position, autoAttackSize, twoDLayer);

        foreach (var item in col)
        {
            StartCoroutine(CaptureFish(item.gameObject, 0.25f));
            return;
        }
    }

    public void Attack(Fish fish)
    {
        Collider[] col = Physics.OverlapSphere(transform.position, attackSize, twoDLayer);

        foreach (var item in col)
        {
            if (item.GetComponent<FishBehaviour>().fishType == fish)
            {
                StartCoroutine(CaptureFish(item.gameObject, 0.25f));
                return;
            }
        }

        StartCoroutine(Wait());
    }

    IEnumerator Wait() 
    {
        actionable = false;
        yield return new WaitForSeconds(0.25f);
        GameManager.gameManager.audioManager.PlayAudio(audioSourceAttack);
        actionable = true;
    }

    IEnumerator MoveRoutine(Vector2 newPos, float speed) 
    {
        actionable = false;

        newPos = new Vector2(Mathf.Clamp(newPos.x, topLeftBorder.position.x, bottomRightBorder.position.x), Mathf.Clamp(newPos.y, bottomRightBorder.position.y, topLeftBorder.position.y));

        while (true) 
        {
            yield return new WaitForFixedUpdate();

            Vector3 tempNewPos = Vector2.MoveTowards(transform.position, newPos, speed);
            tempNewPos.z = transform.position.z;

            transform.position = tempNewPos;

            if ((Vector2)transform.position == newPos)
                break;
        }

        actionable = true;
    }

    IEnumerator CaptureFish(GameObject fish, float speed)
    {
        actionable = false;
        while (true)
        {
            if (fish == null)
                break;

            yield return new WaitForFixedUpdate();

            Vector3 tempNewPos = Vector2.MoveTowards(transform.position, (Vector2)fish.transform.position, speed);
            tempNewPos.z = transform.position.z;

            transform.position = tempNewPos;

            if ((Vector2)transform.position == (Vector2)fish.transform.position)
                break;
        }

        GetFish(fish.GetComponent<FishBehaviour>().fishType);
        actionable = true;
        Destroy(fish);
    }
}
