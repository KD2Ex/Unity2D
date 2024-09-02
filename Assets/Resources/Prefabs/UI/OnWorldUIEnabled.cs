using UnityEngine;
using UnityEngine.UI;

public class OnWorldUIEnabled : MonoBehaviour
{
    private Canvas _canvas;
    [SerializeField] private WorldCanvasManager _so;
    //[SerializeField] private Slider _healthBar;
    
    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _so.Initialize(gameObject);
    }

    private void OnEnable()
    {
        _canvas.worldCamera = Camera.main;
    }
}
