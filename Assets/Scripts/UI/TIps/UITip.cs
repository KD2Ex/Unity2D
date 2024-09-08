using UnityEngine;

public class UITip : MonoBehaviour
{
    [SerializeField] private GameObject tip;

    private void OnEnable()
    {
        tip.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        tip.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        tip.SetActive(false);
    }

}
