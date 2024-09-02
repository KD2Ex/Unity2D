using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIAbilityCooldown : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color uptimeColor;
    [SerializeField] private Color cooldownColor;

    // Start is called before the first frame update
    void Start()
    {
        image.color = uptimeColor;
    }

    public void StartCooldown(FloatVariable cooldown)
    {
        StartCoroutine(Cooldown(cooldown.Value));
    }
    
    private IEnumerator Cooldown(float time)
    {
        image.color = cooldownColor;

        var elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            image.fillAmount = elapsedTime / time;
            yield return null;
        }

        image.color = uptimeColor;
    }
}
