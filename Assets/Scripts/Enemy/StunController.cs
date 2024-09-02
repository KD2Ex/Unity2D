using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunController : MonoBehaviour
{
    [field: SerializeField] public float StunThreshold { get; private set; }
    [field: SerializeField] public float ResetRate { get; private set; }

    [SerializeField] private float stunCap;
    
    public float _stunValue;
    public bool _stunImmunity = false;
    public bool IsStunned { get; private set; }

    private CountDownTimer timer;

    private void Start()
    {
        /*timer = new CountDownTimer(ResetRate);
        timer.OnTimerStart += () => _stunValue -= 1f;
        timer.OnTimerStop += () => timer.Reset();
        
        timer.Start();*/
        StartCoroutine(Timer(ResetRate));
    }

    private IEnumerator Timer(float seconds)
    {
        while (enabled)
        {
            yield return new WaitForSeconds(seconds);
//            Debug.Log("Update target pos");
            _stunValue -= 1f;
            _stunValue = Mathf.Clamp(_stunValue, 0, stunCap);

            if (_stunValue < .1f)
            {
                _stunImmunity = false;
            }
        }
    }

    IEnumerator Immune()
    {
        yield return new WaitForSeconds(1f);
        _stunImmunity = false;
    }
    
    void Update()
    {
        IsStunned = _stunValue >= StunThreshold;
    }
    
    public void AddValue(float value)
    {
        //if (_stunValue >= StunThreshold) return;
        if (_stunImmunity) return;
        
        _stunValue += value;
        _stunValue = Mathf.Clamp(_stunValue, 0, stunCap);

        if (_stunValue > stunCap - .1f)
        {
            _stunImmunity = true;
        }
    }
    
}
