using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private Vector2 position;
    
    [SerializeField] private List<string> _scenesToLoad;
    [SerializeField] private List<string> _scenesToUnload;

    [SerializeField] private bool onTriggerEnter;
    
    private GameObject player;

    public void SetLoadScenes(List<string> newList)
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
                if (loadedScene.name == sceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }

            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
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
                if (loadedScene.name == sceneName)
                {
                    SceneManager.UnloadSceneAsync(sceneName);
                }
            }
        }
    }
    
}
