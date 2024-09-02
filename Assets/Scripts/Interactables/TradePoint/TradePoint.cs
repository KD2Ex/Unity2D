using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradePoint : MonoBehaviour
{
    //[SerializeField] private Dictionary<ResourceRI, int> offers;

    [SerializeField] private List<ResourceRI> offers;
    [SerializeField] private List<int> costs;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        for (int i = 0; i < offers.Count; i++)
        {
            if (offers[i].Amount >= costs[i])
            {
                Debug.Log($"Could spend {costs[i]} of yours {offers[i].Amount} {offers[i].Name}");
            }
            else
            {
                Debug.Log($"Not enough {offers[i].Name}");
            }
        }
    }
}
