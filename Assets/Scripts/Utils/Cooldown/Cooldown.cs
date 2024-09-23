using System;
using System.Collections;
using UnityEngine;

namespace Utils.Cooldown
{
    public class Cooldown
    {
        public static IEnumerator Start(float time, Action<bool> func)
        {
            func(false);

            var elapsed = 0f;

            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            func(true);
        }
    }
}