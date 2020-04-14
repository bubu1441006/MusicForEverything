using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumAnalyzer : MonoBehaviour
{
    //AudioSource audioSource;
	public AudioSource audioSource;

    public static float[] samples = new float[512];

    public static float[] freqBand = new float[8];
    float[] freqBandHighest = new float[8];
    public static float[] audioBands = new float[8];

    private int count = 0;
    public float[] realSamples = new float[512];

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        audioSource.GetSpectrumData(realSamples, 0, FFTWindow.Blackman);
        count++;
        LeftShiftArray<float>(samples, count);
    }

    public static void LeftShiftArray<T>(T[] arr, int shift)
    {
        shift = shift % arr.Length;
        T[] buffer = new T[shift];
        System.Array.Copy(arr, buffer, shift);
        System.Array.Copy(arr, shift, arr, 0, arr.Length - shift);
        System.Array.Copy(buffer, 0, arr, arr.Length - shift, shift);
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        // Iterate through the 8 bins.
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i + 1);

            // Adding the remaining two samples into the last bin.
            if (i == 7)
            {
                sampleCount += 2;
            }

            // Go through the number of samples for each bin, add the data to the average
            for (int j = 0; j < sampleCount; j++)
            {
                average += realSamples[count];
                count++;
            }

            // Divide to create the average, and scale it appropriately.
            average /= count;
            freqBand[i] = (i + 1) * 100 * average;
        }
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > freqBandHighest[i])
                freqBandHighest[i] = freqBand[i];
            audioBands[i] = freqBand[i] / freqBandHighest[i];
            if (freqBandHighest[i] == 0)
                audioBands[i] = 0;

        }
    }

    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        CreateAudioBands();
    }
}
