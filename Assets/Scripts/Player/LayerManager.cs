using UnityEngine;

public class LayerManager : MonoBehaviour
{
    [SerializeField] private LayerMask origin;
    [SerializeField] private LayerMask target;

    private bool switched;
        
    public void SwitchLayer()
    {
        switched = !switched;

        gameObject.layer = switched ? target : origin;
    }
    
}
