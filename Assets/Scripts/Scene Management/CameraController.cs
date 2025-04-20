using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraController : Singleton<CameraController>
{
    private CinemachineCamera cinemachineVirtualCamera;

    private void Start()
    {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow()
    {
        // Updated to use the recommended method to avoid the obsolete warning
        cinemachineVirtualCamera = Object.FindFirstObjectByType<CinemachineCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;


    }
}
      