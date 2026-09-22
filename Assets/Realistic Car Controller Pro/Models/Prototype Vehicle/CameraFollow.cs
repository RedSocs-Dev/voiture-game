using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -6);
    public float smoothTime = 0.4f;

    private Vector3 velocity = Vector3.zero;

    private bool wasInAir = false;
    private Quaternion frozenRotation;

    void LateUpdate()
    {
        if (target == null)
            return;

        Rigidbody rb = target.GetComponentInParent<Rigidbody>();

        if (rb == null)
            rb = target.GetComponentInChildren<Rigidbody>();

        if (rb == null)
            return;

        bool inAir = Mathf.Abs(rb.linearVelocity.y) > 1f;

        if (inAir)
        {
            if (!wasInAir)
            {
                frozenRotation = target.rotation;
                wasInAir = true;
            }
        }
        else
        {
            wasInAir = false;
        }

        Quaternion rotationToUse = inAir ? frozenRotation : target.rotation;

        Vector3 desiredPosition =
            target.position +
            rotationToUse * offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );

        transform.LookAt(target);
    }
}