using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [Tooltip("Distance the object should move forward.")]
    public float moveDistance = 1.0f;

    [Tooltip("Duration of the move forward or backward.")]
    public float moveDuration = 1.0f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        StaticEvents.OnCannonShot.AddListener(AnimateBarrel);
    }

    private void AnimateBarrel()
    {
        StartCoroutine(AnimateMove());
    }

    IEnumerator AnimateMove()
    {
        // Calculate target position
        Vector3 targetPosition = initialPosition - transform.forward * moveDistance;

        // Move forward
        float startTime = Time.time;
        while (Time.time - startTime < moveDuration)
        {
            float t = (Time.time - startTime) / moveDuration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }
        transform.position = targetPosition;

        // Move backward
        startTime = Time.time;
        while (Time.time - startTime < moveDuration)
        {
            float t = (Time.time - startTime) / moveDuration;
            transform.position = Vector3.Lerp(targetPosition, initialPosition, t);
            yield return null;
        }
        transform.position = initialPosition;
    }
}
