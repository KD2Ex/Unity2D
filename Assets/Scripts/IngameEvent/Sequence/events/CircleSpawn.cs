using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

[CreateAssetMenu]
public class CircleSpawn : WorldEvent
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float radius;
    [SerializeField] private int number;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float angleDistance;

    private List<GameObject> objects;
    
    public override IEnumerator Event()
    {
        Finished = false;
        InProgress = true;
        
        var wait = new WaitForSeconds(spawnInterval);
        
        float angle = 0;
        for (int i = 0; i < number; i++)
        {
            var pos = new Vector2(Pos.x + 1f, Pos.y + 2f);

            float x = Pos.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Pos.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            var position = new Vector2(x, y);

            var instance = Instantiate(objectToSpawn, position, Quaternion.identity);
            objects.Add(instance.gameObject);
            
            angle += angleDistance;

            yield return wait;
        }
        
        InProgress = false;
        Timing.RunCoroutine(IsInScene());
    }

    private IEnumerator<float> IsInScene()
    {
        Debug.Log(objects);
        while (objects.Exists((i) => i.activeInHierarchy))
        {
            yield return Timing.WaitForOneFrame;
        }

        Finished = true;
    }
}