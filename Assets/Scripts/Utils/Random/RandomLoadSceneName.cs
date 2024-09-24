using System.Collections.Generic;
using UnityEngine;

public class RandomLoadSceneName : MonoBehaviour
{
    private SceneLoadTrigger loadTrigger;
    [SerializeField] private GetRandomSceneObject randomize;

    private void Awake()
    {
        var item = randomize.Generate();
        
        List<SceneObject> list = new() { item };
        
        loadTrigger = GetComponent<SceneLoadTrigger>();
        loadTrigger.SetLoadScenes(list);
    }
}