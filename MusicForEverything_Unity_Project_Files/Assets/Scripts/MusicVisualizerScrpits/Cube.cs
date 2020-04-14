using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int band;
    public float startScale;
    public float scaleMultiplier;

    void Update()
    {
        if (SpectrumAnalyzer.audioBands[band] == 0)
            transform.localScale = new Vector3(1, startScale, 1);
        else
            transform.localScale = new Vector3(1, SpectrumAnalyzer.audioBands[band] * scaleMultiplier, 1);
    }
}
