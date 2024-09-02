using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffect : MonoBehaviour
{
    private Vignette vignette;
    private Volume volume;

    [SerializeField] private float inTime;
    [SerializeField] private float outTime;
    [SerializeField] private float value;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
    }

    public void ExecuteEffect()
    {
        StartCoroutine(Effect());
    }

    private IEnumerator Effect()
    {
        var time = 0f;

        while (time < inTime)
        {
            time += Time.deltaTime;

            // elapsed * maxTIme / maxValue;
            var intensify = time / inTime * value;


            vignette.intensity.value = intensify;
            yield return null;
        }

        time = outTime;
        
        while (time > 0f)
        {
            time -= Time.deltaTime;

            // elapsed * maxTIme / maxValue;
            var intensify = time / outTime * value;
            vignette.intensity.value = intensify;
            yield return null;
        }
        
        
        
    }
}
