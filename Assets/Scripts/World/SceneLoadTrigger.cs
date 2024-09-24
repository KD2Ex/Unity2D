using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private Vector2 position;
    
    [SerializeField] private List<SceneObject> _scenesToLoad;
    [SerializeField] private List<SceneObject> _scenesToUnload;

    [SerializeField] private bool onTriggerEnter;
    
    private GameObject player;

    public void SetLoadScenes(List<SceneObject> newList)
    {
        _scenesToLoad = newList;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!onTriggerEnter) return;
        
        if (other.CompareTag("Player"))
        {
            LoadScenes();
            UnloadScenes();
        }
    }

    public void LoadScenes()
    {
        player = FindObjectOfType<Player>().gameObject;
        //player.transform.position = position;
        
        foreach (var sceneName in _scenesToLoad)
        {
            bool isSceneLoaded = false;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var loadedScene = SceneManager.GetSceneAt(i);
                if (loadedScene.name == sceneName.Name)
                {
                    isSceneLoaded = true;
                    break;
                }
            }

            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(sceneName.Name, LoadSceneMode.Additive);
            }
        }
    }

    public void UnloadScenes()
    {
        foreach (var sceneName in _scenesToUnload)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var loadedScene = SceneManager.GetSceneAt(i);
                if (loadedScene.name == sceneName.Name)
                {
                    SceneManager.UnloadSceneAsync(sceneName.Name);
                }
            }
        }
    }
    
}
