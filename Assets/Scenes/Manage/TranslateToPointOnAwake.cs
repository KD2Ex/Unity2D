using System.Linq;
using UnityEngine;

public class TranslateToPointOnAwake : MonoBehaviour
{
    [SerializeField] private ScenesLocation locations;
    [SerializeField] private SceneObject scene;
    
    private void Awake()
    {
        var location = locations.Items.FirstOrDefault(item => item.scene.Name == scene.Name);

        if (location != default)
        {
            transform.position = location.point.position;
        }
    }
}
