using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemyInArea : MonoBehaviour
{
    private BoxCollider2D area;
    [SerializeField] private RuntimeSet<RuntimeItem> set;
    [SerializeField] private InputReader input;

    [SerializeField] private RuntimeItem item;

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        input.TestEvent += Spawn;
    }

    private void OnDisable()
    {
        input.TestEvent -= Spawn;
    }

    public void Spawn()
    {
        var x = Random.Range(area.bounds.min.x, area.bounds.max.x);
        var y = Random.Range(area.bounds.min.y, area.bounds.max.y);
        var position = new Vector2(x, y);
        
        var newItem = Instantiate(item,position, Quaternion.identity);
        //set.Add(newItem);
    }

    private void Remove(int i)
    {
        set.Items[i].gameObject.SetActive(false);
    }

    public void RemoveAtRandom()
    {
        Remove(Random.Range(0, set.Items.Count));
    }
}
