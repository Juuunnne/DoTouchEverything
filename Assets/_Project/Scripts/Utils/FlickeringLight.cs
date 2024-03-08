using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour
{

    public enum WaveForm { sin, tri, sqr, saw, inv, noise };
    public WaveForm waveform = WaveForm.sin;

    public float baseStart = 0.0f; // start 
    public float amplitude = 1.0f; // amplitude of the wave
    public float phase = 0.0f; // start point inside on wave cycle
    public float frequency = 0.5f; // cycle frequency per second

    // Keep a copy of the original color
    private Color _originalColor;
    private Light _light;

    // Store the original color
    void Start()
    {
        _light = GetComponent<Light>();
        _originalColor = _light.color;
    }

    void Update()
    {
        _light.color = _originalColor * EvalWave();
    }

    float EvalWave()
    {
        float x = (Time.time + phase) * frequency;
        float y;
        x -= Mathf.Floor(x); // normalized value (0..1)

        switch (waveform)
        {
            case WaveForm.sin:
                float frequencyFactor = 2f;
                float amplitudeFactor = 0.5f;
                float phaseOffset = Random.Range(0f, 2f * Mathf.PI);

                float adjustedSinValue = Mathf.Sin((x * frequencyFactor + phaseOffset) * 2 * Mathf.PI) * amplitudeFactor;
                y = Mathf.Clamp(adjustedSinValue, 0f, 1f);
                break;

            case WaveForm.tri:
                y = (x < 0.5f) ? 4.0f * x - 1.0f : -4.0f * x + 3.0f;
                break;

            case WaveForm.sqr:
                y = (x < 0.5f) ? 1.0f : -1.0f;
                break;

            case WaveForm.saw:
                y = x;
                break;

            case WaveForm.inv:
                y = 1.0f - x;
                break;

            case WaveForm.noise:
                y = 1f - (Random.value * 2);
                break;

            default:
                y = 1.0f;
                break;
        }


        return (y * amplitude) + baseStart;
    }
}