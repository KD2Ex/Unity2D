using System.Linq;
using UnityEngine;

public class TranslateToPointOnAwake : MonoBehaviour
{
    [SerializeField] private ScenesLocation locations;
    [SerializeField] private string sceneName;
    
    private void Awake()
    {
        var location = locations.Items.FirstOrDefault(item => item.name == sceneName);

        if (location != default)
        {
            transform.position = location.point.position;
        }
    }
}
