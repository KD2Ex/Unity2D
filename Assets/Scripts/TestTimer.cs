using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    // Start is called before the first frame update
    CountDownTimer timer = new CountDownTimer(1f);

    
    void Start()
    {

        timer.OnTimerStop += () =>
        {
            
            timer.Reset();
        };
        
        timer.Start();

        /*StartCoroutine(Timer());

        IEnumerator Timer()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(2f);
                Debug.Log("Timer elapsed");
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick(Time.deltaTime);
    }
}
