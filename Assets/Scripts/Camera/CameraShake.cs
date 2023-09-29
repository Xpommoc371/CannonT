using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float maxShakeAmount = 1.0f;

    public float shakeDuration = 0.5f;

    public float shakeSpeed = 10.0f;

    private Vector3 originalPosition;
    private float currentShakeDuration = 0.0f;

    private void Start()
    {
        originalPosition = transform.position;
        StaticEvents.OnCannonShot.AddListener(StartShake);
    }

    private void Update()
    {
        if (currentShakeDuration > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * maxShakeAmount;
            transform.position = originalPosition + shakeOffset;
            currentShakeDuration -= Time.deltaTime * shakeSpeed;
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    // Start the camera shake.
    public void StartShake()
    {
        currentShakeDuration = shakeDuration;
    }
}