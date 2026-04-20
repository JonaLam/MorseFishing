using UnityEngine;
using Unity.Cinemachine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] CinemachineCamera currentCam;

    [SerializeField] CinemachineCamera defaultCam;

    bool defaultCamBool = true;

    public delegate void PlayerCameraDelegate(CinemachineCamera cam);
    public PlayerCameraDelegate onChangeCamera;

    void Start()
    {
        defaultCam.enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick();

        if (Input.GetKeyDown(KeyCode.Escape))
            ChangeCamera(defaultCam);

        defaultCamBool = currentCam != defaultCam;
    }

    void OnClick() 
    {
        if (Input.GetMouseButtonDown(0) && defaultCamBool)
        {
            if (Input.mousePosition.y < 0.2f * Screen.height)
            {
                ChangeCamera(defaultCam);
                return;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if( Physics.Raycast(ray, out raycastHit)) 
        {
            Interactable interactable;

            if (raycastHit.collider.gameObject.TryGetComponent<Interactable>( out interactable)) 
            {
                interactable.OnClick();
            }
        }
    }

    public void ChangeCamera(CinemachineCamera newCamera)
    {
        newCamera.enabled = true;
        currentCam.enabled = false;
        currentCam = newCamera;

        if (onChangeCamera != null)
            onChangeCamera(newCamera);
    }
}
