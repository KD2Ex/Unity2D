using Cinemachine;
using UnityEngine;

public class BlendFocusWithPlayer : MonoBehaviour
{
    private CinemachineTargetGroup targetGroup;
    private Transform player;

    private void Awake()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>().transform;

        targetGroup.AddMember(player, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
