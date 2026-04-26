using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FrameManager : MonoBehaviour
{
    public Camera puzzleCamera;
    public List<Frame> frames = new List<Frame>();

    private Frame selectedFrame = null;
    private bool isActive = false;


    void Start()
    {
        foreach (Frame f in frames)
        {
            f.SetPuzzle(this);
            if (f.GetComponent<Collider>() == null)
                f.gameObject.AddComponent<BoxCollider>();
        }
    }

    void Update()
    {
        if (!isActive)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200f))
            {
                Debug.Log("Ray hit: " + hit.collider.name);
                Frame f = hit.collider.GetComponent<Frame>();
                if (f != null)
                {
                    SelectFrame(f);
                    Debug.Log("Clicked frame: " + f.name);
                }
            }
            else
            {
                Debug.Log("Ray hit nothing");
            }
        }
    }

    public void ActivatePuzzle()
    {
        isActive = true;
        Debug.Log("Frame puzzle activated");
    }

    public void DeactivatePuzzle()
    {
        isActive = false;
        Debug.Log("Frame puzzle deactivated");
        GameManager.instance.finishedFrame = true;
        
        GameManager.instance.focusCamera.StopFocus();
    }

    public void SelectFrame(Frame frame)
    {
        if (!isActive)
        {
            transform.position += new Vector3(0.2f, 0f, 0f);
            Debug.Log("Puzzle not active");
            return;
        }

        if (selectedFrame == null)
        {
            selectedFrame = frame;
            Debug.Log("Selected: " + frame.name);
        }
        else
        {
            TrySwap(selectedFrame, frame);
            selectedFrame = null;
        }
    }

    void TrySwap(Frame a, Frame b)
    {
        if (Mathf.Abs(a.currentIndex - b.currentIndex) == 1)
        {
            SwapFrames(a, b);
            CheckWin();
        }
        else
        {
            Debug.Log("Frames not adjacent!");
        }
    }

    void SwapFrames(Frame a, Frame b)
    {
        StartCoroutine(AnimateSwap(a, b));
    }
    IEnumerator AnimateSwap(Frame a, Frame b)
    {
        Vector3 posA = a.transform.position;
        Vector3 posB = b.transform.position;

        float duration = 0.3f;
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;

            t = Mathf.SmoothStep(0, 1, t);

            a.transform.position = Vector3.Lerp(posA, posB, t);
            b.transform.position = Vector3.Lerp(posB, posA, t);

            time += Time.deltaTime;
            yield return null;
        }

        a.transform.position = posB;
        b.transform.position = posA;

        int tempIndex = a.currentIndex;
        a.currentIndex = b.currentIndex;
        b.currentIndex = tempIndex;

        Debug.Log("Swapped " + a.name + " with " + b.name);
    }

    void CheckWin()
    {
        foreach (Frame f in frames)
        {
            if (f.currentIndex != f.correctIndex)
                return;
        }

        Debug.Log("Frame puzzle solved!");

        DeactivatePuzzle();
    }
}