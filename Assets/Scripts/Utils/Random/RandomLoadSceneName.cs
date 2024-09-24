using System.Collections.Generic;
using UnityEngine;

public class RandomLoadSceneName : MonoBehaviour
{
    private SceneLoadTrigger loadTrigger;

    [SerializeField] private GetRandomString randomize;

    private void Awake()
    {
        List<string> list = new() { randomize.Item };
        
        loadTrigger = GetComponent<SceneLoadTrigger>();
        loadTrigger.SetLoadScenes(list);
    }
}