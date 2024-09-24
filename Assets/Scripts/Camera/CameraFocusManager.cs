using Cinemachine;
using UnityEngine;

public class CameraFocusManager : MonoBehaviour
{
    public static CameraFocusManager instance;
    
    public CinemachineVirtualCamera VirtualCamera { get; private set; }
    
    
    public void SetFocus(Transform followTarget)
    {
        VirtualCamera.m_Follow = followTarget;
        VirtualCamera.m_Priority = 13;
    }

    public void RemoveFocus()
    {
        VirtualCamera.m_Priority = 8;
    }
    
    private void Awake()
    {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (instance == null)
        {
            instance = this;
        }
    }
}
