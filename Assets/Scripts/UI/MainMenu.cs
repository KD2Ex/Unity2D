using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _loadingBarObject;
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private GameObject[] _objectsToHide;
    
    [Header("Scenes")] 
    [SerializeField] private string _persistentGameplay = "PersistentGameplay";
    [SerializeField] private string _levelHub = "Dung1";
    
    private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();
    
    private void Awake()
    {
        _loadingBarObject.SetActive(false);
    }

    public void StartGame()
    {
        HideMenu();
        
        _loadingBarObject.SetActive(true);
        
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(_persistentGameplay));
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(_levelHub, LoadSceneMode.Additive));

        StartCoroutine(ProgressBar());
    }

    private void HideMenu()
    {
        foreach (var obj in _objectsToHide)
        {
            obj.SetActive(false);
        }
    }

    private IEnumerator ProgressBar()
    {
        var progress = 0f;

        foreach (var scene in _scenesToLoad)
        {
            while (!scene.isDone)
            {
                progress += scene.progress;
                _loadingSlider.value = progress / _scenesToLoad.Count;
                yield return null;
            }
        }
    }
    
}
