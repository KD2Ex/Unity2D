using System.Collections;
using System.Collections.Generic;
using MEC;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[CreateAssetMenu]
public class CircleEnemySpawn : WorldEvent
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float radius;
    [SerializeField] private int number;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float angleDistance;

    private List<Enemy> enemies;
    
    public override IEnumerator Event()
    {
        enemies.Clear();        
        Finished = false;
        InProgress = true;
        
        var wait = new WaitForSeconds(spawnInterval);
        
        float angle = 0;
        for (int i = 0; i < number; i++)
        {

            var position = MathUtils.GetPointOnCircle(Pos, radius, angle);

            var instance = Instantiate(objectToSpawn, position, Quaternion.identity);
            var enemy = instance.gameObject.GetComponent<Enemy>();
            enemy.OnDeath.AddListener(() =>
            {
                enemies.Remove(enemy);
                Debug.Log(enemies.Count);
            });
            
            enemies.Add(enemy);
            
            
            angle += angleDistance;

            yield return wait;
        }
        
        InProgress = false;
        Timing.RunCoroutine(IsAlive());
    }

    private void Remove()
    {
        
    }
    
    private IEnumerator<float> IsAlive()
    {
        while (enemies.Count > 0)
        {
            yield return Timing.WaitForOneFrame;
        }

        Finished = true;
    }
}