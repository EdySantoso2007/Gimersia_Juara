using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [Header("Target & Smoothing")]
    public Transform target;
    public float smoothTime = 0.15f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 offset = new Vector3(0f, 0f, -10f);

    void LateUpdate()
    {
        if (target == null) return;

        // Posisi tujuan kamera dengan mempertahankan Z = -10
        Vector3 targetPosition = target.position + offset;

        // Pergerakan kamera yang halus mengikuti player
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            targetPosition, 
            ref velocity, 
            smoothTime
        );
    }
}