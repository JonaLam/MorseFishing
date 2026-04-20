using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DivingManager : MonoBehaviour
{
    int timer;

    [SerializeField] FishBehaviour starFish, fish;
    public DiverBehaviour diverBehaviour;

    public Transform topLeft, bottomRight, diver;


    public bool diveing = false;

    [SerializeField] GameObject diveWall;
    [SerializeField] Transform diveWallDefaultPos;

    public List<FishBehaviour> fishBehaviours;

    public int initalStarFish;
    public bool spawnFish = false;

    public SpriteRenderer finalFishEye1, finalFishEye2;
    public GameObject TopJaw, BottomJaw;

    public void FinalFish()
    {
        StartCoroutine(FinalFishRoutine());
    }

    private void Start()
    {
        fishBehaviours = new List<FishBehaviour>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            StartDive();
        if (Input.GetKeyDown(KeyCode.O))
            FinishDive();
    }

    private void FixedUpdate()
    {
        if (!spawnFish)
            return;

        timer++;

        if(timer > 1000) 
        {
            Vector3 pos = new Vector3(topLeft.position.x, Random.Range(topLeft.position.y, bottomRight.position.y), diver.position.z);
            FishBehaviour fishInstance = Instantiate(fish, pos, Quaternion.identity);
            fishInstance.SetDir(Vector2.right);
            fishBehaviours.Add(fishInstance);
            timer = 0;
        }
    }

    public void StartDive() 
    {
        diverBehaviour.MoveToStart();
        StartCoroutine(MoveRoutine(diveWallDefaultPos.transform.position + Vector3.up * 6, .1f));

        for (int i = 0; i < initalStarFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(topLeft.position.x, bottomRight.position.x), Random.Range(topLeft.position.y, bottomRight.position.y), diver.position.z);
            FishBehaviour fishInstance = Instantiate(starFish, pos, Quaternion.identity);
            fishBehaviours.Add(fishInstance);
        }
    }

    public void FinishDive() 
    {
        diverBehaviour.MoveOut();
        StartCoroutine(MoveRoutine(diveWallDefaultPos.transform.position, .1f));

        while (fishBehaviours.Count != 0)
        {
            Destroy(fishBehaviours[0].gameObject);
            fishBehaviours.RemoveAt(0);
        }
        fishBehaviours.Clear();
    }

    IEnumerator MoveRoutine(Vector2 newPos, float speed)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            Vector3 tempNewPos = Vector2.MoveTowards(diveWall.transform.position, newPos, speed);
            tempNewPos.z = diveWall.transform.position.z;

            diveWall.transform.position = tempNewPos;

            if ((Vector2)diveWall.transform.position == newPos)
                break;
        }
    }

    IEnumerator FinalFishRoutine() 
    {
        yield return new WaitForSeconds(30);
        Color color = finalFishEye2.color;
        for (int i = 0; i < 10; i++)
        {
            color.a += 0.01f;
            finalFishEye1.color = color;
            finalFishEye2.color = color;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(10);
        while (true)
        {
            yield return new WaitForFixedUpdate();

            Vector3 tempNewPos = Vector2.MoveTowards(TopJaw.transform.position, diver.transform.position, 1);
            tempNewPos.z = TopJaw.transform.position.z;

            TopJaw.transform.position = tempNewPos;

            tempNewPos = Vector2.MoveTowards(BottomJaw.transform.position, diver.transform.position, 1);
            tempNewPos.z = BottomJaw.transform.position.z;

            BottomJaw.transform.position = tempNewPos;

            if ((Vector2)TopJaw.transform.position == (Vector2)diver.transform.position && (Vector2)BottomJaw.transform.position == (Vector2)diver.transform.position)
                break;
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("WinScene");
    }
}
