using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancer : MonoBehaviour
{
    [SerializeField] public bool isDancing;

    private void Start()
    {
        isDancing = false; 
    }
    public IEnumerator SideMoves()
    {
        isDancing = true;
        Vector3 currentPos = transform.position;
        Vector3 startingPos = currentPos;
        int startingBeat = Conductor.instance.pulseCounter;
        while (Conductor.instance.pulseCounter == startingBeat) { yield return null; }
        currentPos.x -= 1; // -1; 0
        transform.position = currentPos;
        yield return new WaitForSecondsRealtime(0.476f);
        currentPos.x += 1;
        currentPos.y += 1; // 0; 1
        transform.position = currentPos;
        yield return new WaitForSecondsRealtime(0.476f);
        currentPos.x += 1;
        currentPos.y -= 1; // 1; 0
        transform.position = currentPos;
        yield return new WaitForSecondsRealtime(0.476f);
        currentPos = startingPos; // 0; 0
        transform.position = currentPos;
        isDancing = false;
    }

    public IEnumerator BarrelRoll()
    {
        isDancing = true;
        int startingBeat = Conductor.instance.pulseCounter;
        while (Conductor.instance.pulseCounter == startingBeat) { yield return null; }
        Quaternion currentRotation = Quaternion.Euler(0, 0, 90);
        transform.rotation = currentRotation;
        yield return new WaitForSecondsRealtime(0.476f);
        currentRotation = Quaternion.Euler(0, 0, 180);
        transform.rotation = currentRotation;
        yield return new WaitForSecondsRealtime(0.476f);
        currentRotation = Quaternion.Euler(0, 0, 270);
        transform.rotation = currentRotation;
        yield return new WaitForSecondsRealtime(0.476f);
        currentRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = currentRotation;
        isDancing = false;
    }

    public IEnumerator ScaleUpAndDown()
    {
        isDancing = true;
        Vector3 currentScale = transform.localScale;
        Vector3 startingScale = currentScale;
        int startingBeat = Conductor.instance.pulseCounter;
        while (Conductor.instance.pulseCounter == startingBeat) { yield return null; }
        currentScale.x += 0.5f;
        currentScale.y += 0.5f;
        transform.localScale = currentScale;
        yield return new WaitForSecondsRealtime(0.476f);
        currentScale.x += 0.5f;
        currentScale.y += 0.5f;
        transform.localScale = currentScale;
        yield return new WaitForSecondsRealtime(0.476f);
        currentScale.x += 0.5f;
        currentScale.y += 0.5f;
        transform.localScale = currentScale;
        yield return new WaitForSecondsRealtime(0.476f);
        currentScale = startingScale;
        transform.localScale = currentScale;
        isDancing = false;
    }

    public IEnumerator SpecialDance()
    {
        int startingBeat = Conductor.instance.pulseCounter;
        while (Conductor.instance.pulseCounter == startingBeat) { yield return null; }
    }
}
