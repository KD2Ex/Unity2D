using System.Collections.Generic;
using MEC;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow time effect", menuName = "SO/Items/Consumable/Effect/SlowTime")]
public class SlowTimeEffect : ConsumableEffect
{
    public float Seconds;
    
    public override void Execute()
    {
        Timing.KillCoroutines("Slowtime");
        Timing.RunCoroutine(Slowtime());
    }

    private IEnumerator<float> Slowtime()
    {
        Time.timeScale = .5f;
        yield return Timing.WaitForSeconds(Seconds);
        Time.timeScale = 1f;
    }
}