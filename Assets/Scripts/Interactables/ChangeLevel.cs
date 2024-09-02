using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : Interactable
{
    private SceneLoadTrigger _sceneChanger;
    private bool _opened = false;

    protected override void Awake()
    {
        base.Awake();

        _sceneChanger = GetComponent<SceneLoadTrigger>();
    }
    
    public override void OnInteraction()
    {
        if (_opened) return;
        _opened = true;
        
        base.OnInteraction();
        
        _sceneChanger.LoadScenes();
        _sceneChanger.UnloadScenes();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEnter(other.gameObject);
        /*Debug.Log(other.gameObject.name);
        transform.SetParent(other.gameObject.transform);*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TriggerExit(other.gameObject);
    }

}
