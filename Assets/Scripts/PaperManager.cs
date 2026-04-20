using UnityEngine;
using System.Collections.Generic;

public class PaperManager : MonoBehaviour
{
    [SerializeField] GameObject[] papers;

    [SerializeField] Stack<GameObject> paperStack, discardedPapers;

    [SerializeField] Transform mainStack, mainStack2, discardStack;

    private void Start()
    {
        float offset = 0;
        foreach (var item in papers)
        {
            item.transform.position = mainStack.position;
            item.transform.position += Vector3.up * offset;


            offset += 0.02f;
        }

        paperStack = new Stack<GameObject>(papers);
        discardedPapers = new Stack<GameObject>();

        GameObject paper = paperStack.Peek();
        paper.transform.position = mainStack2.position;
        paper.transform.position += Vector3.up * 0.02f * paperStack.Count;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            MoveTopPaper();
        if (Input.GetKeyDown(KeyCode.Y))
            MoveDiscardedPaper();
    }

    public void MoveTopPaper() 
    {
        if (paperStack.Count == 0)
            return;

        GameObject paper = paperStack.Pop();
        paper.transform.position = discardStack.position;
        paper.transform.position += Vector3.up * 0.02f * discardedPapers.Count;
        discardedPapers.Push(paper);

        if (paperStack.Count == 0)
            return;

        paper = paperStack.Peek();
        paper.transform.position = mainStack2.position;
        paper.transform.position += Vector3.up * 0.02f * paperStack.Count;
    }

    public void MoveDiscardedPaper() 
    {
        if (discardedPapers.Count == 0)
            return;

        GameObject paper = discardedPapers.Pop();
        paper.transform.position = mainStack2.position;
        paper.transform.position += Vector3.up * 0.02f * (paperStack.Count + 1);

        if (paperStack.Count != 0)
        {

            GameObject otherPaper = paperStack.Peek();
            otherPaper.transform.position = mainStack.position;
            otherPaper.transform.position += Vector3.up * 0.02f * paperStack.Count;
        }
        paperStack.Push(paper);
    }
}
