using UnityEngine;
using UnityEngine.InputSystem;

public class MouvementAir : MonoBehaviour
{
    public float airTorque = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        rb.angularVelocity *= 0.98f;

        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb == null)
            return;

        // La voiture est considérée en l'air si elle monte ou descend
        bool inAir = Mathf.Abs(rb.linearVelocity.y) > 1f;

        if (inAir)
        {
            rb.angularVelocity *= 0.95f;
        }

        if (!inAir)
            return;

        // Tourner horizontalement uniquement
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            rb.AddRelativeTorque(0f, airTorque, 0f, ForceMode.Force);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            rb.AddRelativeTorque(0f, airTorque, 0f, ForceMode.Force);
        }
    }
}