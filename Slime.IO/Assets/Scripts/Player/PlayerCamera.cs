using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Vector3 actionOffset;
    [SerializeField] private SlimeStomach slimeStomach;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    private void LateUpdate()
    {
        CameraPosition();
    }

    private void CameraPosition()
    {
        Vector3 targetPos = transform.position + actionOffset * (slimeStomach.SlimeSize * 0.75f);
        //Vector3 smoothPos = Vector3.Lerp(playerCamera.transform.position, targetPos, smoothFactor * Time.fixedDeltaTime);
        playerCamera.transform.position = targetPos;
    }
}
