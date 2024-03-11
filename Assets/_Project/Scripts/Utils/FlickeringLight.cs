using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour
{
    private Light _light;

    [SerializeField]
    private float
        _tolerance = 0.1f,
        _flickerSpeed = 0.4f,
        _minIntensity = 0.6f,
        _maxIntensity = 1.0f;
    private float _random = 0.5f;

    private void Start()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        if (_light.intensity.IsEqualTo(_random, _tolerance))
        {
            _random = NewRandom();

        }

        _light.intensity += Mathf.Sign(_random - _light.intensity) * _flickerSpeed * Time.deltaTime;
    }

    private float NewRandom()
    {
        return Random.Range(_minIntensity, _maxIntensity);
    }
}