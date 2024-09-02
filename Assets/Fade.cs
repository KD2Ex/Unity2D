using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fade : MonoBehaviour
{

    private Tilemap _tilemap;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_tilemap.color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Enter");

        StopAllCoroutines();
        StartCoroutine(FadeIn());
        //_tilemap.color = new Color(1f, 1f, 1f, .1f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float alpha = _tilemap.color.a;
        var tempColor = _tilemap.color;
        while (_tilemap.color.a > .2f)
        {
            alpha -= .01f;
            tempColor.a = alpha;
            _tilemap.color = tempColor;
            
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float alpha = _tilemap.color.a;
        var tempColor = _tilemap.color;
        while (_tilemap.color.a < 1f)
        {
            alpha += .01f;
            tempColor.a = alpha;
            _tilemap.color = tempColor;
            
            yield return null;
        }
    }
}
