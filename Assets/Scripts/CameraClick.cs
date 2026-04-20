using UnityEngine;
using Unity.Cinemachine;


public class CameraClick : MonoBehaviour, Interactable
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] CinemachineCamera cam;
    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
        playerManager.onChangeCamera += OnCamChange;
        cam.enabled = false;
    }

    public void OnClick() 
    {
        playerManager.ChangeCamera(cam);
    }

    public void OnCamChange(CinemachineCamera newCam) 
    {
        col.enabled = newCam != cam;
    }
}
