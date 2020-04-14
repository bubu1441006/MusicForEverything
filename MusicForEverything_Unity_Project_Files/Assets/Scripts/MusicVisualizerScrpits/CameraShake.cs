using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;
    public float shakeDuration = 0f;
    private float shakeAmount;
    public float decreaseFactor = 1.0f;
    Vector3 originalPos;

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        shakeAmount = SpectrumAnalyzer.audioBands[0] + SpectrumAnalyzer.audioBands[1] * 2.0f;
        if (shakeAmount > 1.0f)
            shakeDuration = 2.0f;
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
}