using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    private Slider _slider;
    
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 1f;
    }

    public void UpdateValue(float value, float maxValue)
    {
        _slider.value = value / maxValue;
    }
}
