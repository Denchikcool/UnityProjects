using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public Light DirectionalLight;

    private ParticleSystem _particleSystem;
    private bool _isRain = false;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }

    private void Update()
    {
        if (_isRain && DirectionalLight.intensity > 0.2f)
        {
            LightIntensity(-1);
        }
        else if(!_isRain && DirectionalLight.intensity < 0.5f)
        {
            LightIntensity(1);
        }
    }

    private void LightIntensity(int mult)
    {
        DirectionalLight.intensity += 0.1f * Time.deltaTime * mult;
    }

    IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 15f));

            if( _isRain ) _particleSystem.Stop();
            else _particleSystem.Play();

            _isRain = !_isRain;
        }
    }
}
