using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthImage;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private FloatReference hp;
    [SerializeField] private FloatReference maxHp;

    private void Update()
    {
        UpdateValues();
    }

    private void UpdateValues()
    {
        _healthImage.fillAmount = Mathf.Clamp01(hp.Value / maxHp.Value);
        _text.text = ClampValue().ToString();
    }

    private int ClampValue()
    {
        if (hp.Value < 0) return 0;
        //if (hp.Value > maxHp.Value) return (int)maxHp.Value;

        return (int)hp.Value;
    }
}
