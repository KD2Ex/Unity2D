using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private InputReader inputReader;

    private void OnEnable()
    {
        inputReader.TestEvent += Restart;
    }

    private void OnDisable()
    {
        inputReader.TestEvent -= Restart;
    }

    private void Restart()
    {
        SceneManager.UnloadSceneAsync(sceneName);
        //SceneManager.UnloadSceneAsync("PersistentGameplay");
        SceneManager.LoadScene("PersistentGameplay");
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
