using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private UIDocument document;
    private VisualElement container;
    private Button buttonContinue;
    private Button buttonSettings;
    private Button buttonExit;

    private bool open;
    
    private const string hiddenClass = "hidden";
    
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        container = document.rootVisualElement.Q<VisualElement>("Container");

        buttonContinue = document.rootVisualElement.Q<Button>("Continue");
        buttonSettings = document.rootVisualElement.Q<Button>("Settings");
        buttonExit = document.rootVisualElement.Q<Button>("Exit");
        buttonContinue.clicked += Resume;
        buttonSettings.clicked += OpenSettings;
        buttonExit.clicked += Quit;
    }

    private void OnEnable()
    {
        inputReader.EscEvent += OnPause;
    }

    private void OnDisable()
    {
        inputReader.EscEvent -= OnPause;
    }

    private void OnPause()
    {
        Debug.Log("Pause");
        open = !open;

        if (open)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    private void Pause()
    {
        Debug.Log("Open");
        container.RemoveFromClassList(hiddenClass);
        Time.timeScale = 0f;
        
        inputReader.BlockAllGameplayInput();
    }

    private void Resume()
    {
        Debug.Log("Close");
        container.AddToClassList(hiddenClass);
        open = false;
        Time.timeScale = 1f;
        
        inputReader.UnblockAllGameplayInput();
    }

    private void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    private void OpenSettings()
    {
        
    }
}
