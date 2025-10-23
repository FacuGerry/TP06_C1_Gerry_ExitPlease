using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineCamera;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
