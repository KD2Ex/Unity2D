using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WorldCanvasEvents", menuName = "SO/UI/Events")]
public class WorldCanvasManager : ScriptableObject
{
    private GameObject uiParent;
    [SerializeField] private GameObject healthBar;
    private Slider _slider;
    public void Initialize(GameObject uiParent)
    {
        this.uiParent = uiParent;
        Debug.Log(uiParent);
    }
    
    public void InstantiateHealthBar(Transform handler, HealthComponent healthComponent)
    {
        Debug.Log(healthBar);
        
        var go = Instantiate(healthBar, uiParent.transform);
        //go.GetComponent<HealthSlider>().Initialize(healthComponent, handler.transform);
    }
}
