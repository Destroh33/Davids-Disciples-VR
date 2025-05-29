using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class VRArmsFollower : MonoBehaviour
{
    [Tooltip("Reference to the right hand controller object under XR Origin")]
    public Transform rightControllerTransform;
    [Tooltip("Offset from the controller to place the arms (if needed)")]
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    void Update()
    {
        if (rightControllerTransform)
        {
            transform.position = rightControllerTransform.position + rightControllerTransform.TransformVector(positionOffset);
            transform.rotation = rightControllerTransform.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}
