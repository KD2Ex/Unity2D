using Unity.Mathematics;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject obj;
    [SerializeField] private int times;

    private void Start()
    {
        if (times <= 0) times = 1;
        
        for (int i = 0; i < times; i++)
        {
            var position = transform.position;
            position.x += i * 2f;
            
            var instance = Instantiate(obj, position, Quaternion.identity);
        }
    }
}
