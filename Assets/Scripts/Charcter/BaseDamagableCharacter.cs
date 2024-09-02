using System.Collections;
using Interfaces;
using Structs;
using UnityEngine;
using UnityEngine.Events;

public class BaseDamagableCharacter : MonoBehaviour
{
    //protected SpriteRenderer sprite;
    [SerializeField] protected ParticleSystem onDamageParticles;

    protected bool _isFlashing;

    protected IEnumerator Flash(SpriteRenderer sprite, int materialPropName, float flashTime)
    {
        _isFlashing = true;
        var _timer = 0f;
        sprite.material.SetFloat(materialPropName, 1f);
        
        while (_timer < flashTime)
        {
            var lerpValue = Mathf.Lerp(1f, 0f, (_timer / flashTime));
            //var lerpValue = Mathf.MoveTowards(1f, 0f, (_timer / _flashTime));
            sprite.material.SetFloat(materialPropName, lerpValue);

            _timer += Time.deltaTime;
            
            yield return null;
        }

        sprite.material.SetFloat(materialPropName, 0f);
        
        _isFlashing = false;
    }
}
