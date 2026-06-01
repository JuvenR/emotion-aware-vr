using UnityEngine;

public class XRPlayerColliderFollower : MonoBehaviour
{
    [SerializeField] private Transform xrCamera;
    [SerializeField] private float colliderCenterHeight = 1f;

    private void LateUpdate()
    {
        if (xrCamera == null)
        {
            return;
        }

        Vector3 cameraPosition = xrCamera.position;

        transform.position = new Vector3(
            cameraPosition.x,
            colliderCenterHeight,
            cameraPosition.z
        );

        transform.rotation = Quaternion.identity;
    }
}