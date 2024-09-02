using System;
using System.Collections;
using UnityEngine;

public class AfterimageGenerator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer originSpriteRenderer;
    [SerializeField] private float timeInterval;
    [SerializeField] private Color color;

    private bool activated = false;
    private float time;

    private void Update()
    {
        if (!activated)
        {
            time = 0f;
            return;
        }

        if (time > timeInterval)
        {
            CreateImage();
            time = 0f;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
    
    public void Execute()
    {
        activated = true;
        CreateImage();
    }

    public void Stop()
    {
        activated = false;
    }

    private void CreateImage()
    {
        GameObject go = new GameObject("Afterimage");
        go.transform.SetParent(transform);
        go.transform.position = originSpriteRenderer.transform.position;
        go.layer = originSpriteRenderer.gameObject.layer;

        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = originSpriteRenderer.sprite;
        sr.color = color;
        sr.material = originSpriteRenderer.material;
        sr.sortingLayerID = originSpriteRenderer.sortingLayerID;
        sr.sortingLayerName = originSpriteRenderer.sortingLayerName;
        sr.sortingOrder = originSpriteRenderer.sortingOrder - 1;
        sr.flipX = originSpriteRenderer.flipX;
        sr.flipY = originSpriteRenderer.flipY;

        StartCoroutine(FadeOut(go, sr, 1f));
    }

    private IEnumerator FadeOut(GameObject go, SpriteRenderer sr, float time)
    {
        var elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .8f - elapsedTime / time);
            yield return null;
        }
        
        Destroy(go);
    }
}
