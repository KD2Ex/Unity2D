using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : IngameEvent
{
    [SerializeField] private List<Enemy> objectsToSpawn;
    //[SerializeField] public Dictionary<Vector2, Enemy> objectsToSpawn;
    private List<Enemy> _enemies;

    [SerializeField] private float radius;
    [SerializeField] private int number;
    
    private void Awake()
    {
        _enemies = new List<Enemy>();
        foreach (var go in objectsToSpawn)
        {
            
        }
    }

    private void OnDeath(Enemy enemy)
    {
        _enemies.Remove(enemy);
        Debug.Log("Death");
    }

    public override IEnumerator Event()
    {
        InProcess = true;

        float angle = 0;
        foreach (var go in objectsToSpawn)
        {
            for (int i = 0; i < number; i++)
            {
                var pos = new Vector2(transform.position.x + 1f, transform.position.y + 2f);

                float x = transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                var position = new Vector2(x, y);
            
                var enemy = Instantiate<Enemy>(go, position, Quaternion.identity);
                //enemy.GetComponent<GoapAgent>().SetKnownLocations();
                _enemies.Add(enemy);

                angle += 360f / number;
            }
            
        }

        while (_enemies.Count > 0)
        {
            Debug.Log(_enemies.Count);
            yield return null;
        }

        Debug.Log("Event finished");
        
        Finished = true;
        InProcess = false;
    }

    /*public override void StartEvent()
    {
        StartCoroutine(Event());
    }*/
}
