using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CircleSpawn : WorldEvent
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float radius;
    [SerializeField] private int number;
    [SerializeField] private float spawnInterval;
    
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

            angle += Random.Range(30, 78);

            yield return wait;
        }

        InProgress = false;
        Finished = true;

    }
}