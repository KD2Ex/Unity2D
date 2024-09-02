using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public UnityEvent<Transform, HealthComponent> OnSpawn = new();
    
    [SerializeField] private HealthComponent _health;

    private void OnEnable()
    {
        OnSpawn.Invoke(transform, _health);
        
        /*_slider = Instantiate(_slider, uiParent.transform);
        _slider.GetComponent<FollowTransform>().SetTarget(transform);
        _slider.maxValue = _health.MaxValue;
        _slider.value = _health.Value;

        _slider.gameObject.SetActive(_slider.value < _slider.maxValue);

        _health.OnValueChanged += OnHealthChanged;*/
    }

    /*private void OnDisable()
    {
        _health.OnValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float newValue)
    {
        _slider.value = newValue;

        _slider.gameObject.SetActive(_slider.value < _slider.maxValue);

        if (newValue <= .01f)
        {
            _slider.gameObject.SetActive(false);
        }
    }*/
}
