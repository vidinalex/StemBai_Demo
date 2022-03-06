using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float
        shakeDuration = 0f,
        shakeAmount = 0.7f,
        decreaseFactor = 1.0f;
    private Transform camTransform;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void Shake()
    {
        shakeDuration = 0.5f;
    }
}