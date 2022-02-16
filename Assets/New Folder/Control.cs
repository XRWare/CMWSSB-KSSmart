using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] [Range(0, 89)] float maxRotationDegrees = 10.0f; // At 90+ gimbal oddities must be dealt with.
    [SerializeField] bool ClampToMaxRotationDegrees = true; // Disable for free rotation.
    [SerializeField] float rotationSpeed = 10.0f;

    const float fullArc = 360.0f;
    const float halfArc = 180.0f;
    const float nullArc = 0.0f;

    void Update() { Tilt(); }

    void Tilt()
    {
        // Apply the 'pre-clamp' rotation (rotation-Z and rotation-X from X & Y of mouse, respectively).
        if (maxRotationDegrees > 0) { SimpleRotation(GetMouseInput()); }

        // Clamp rotation to maxRotationDegrees.
        if (ClampToMaxRotationDegrees) { ClampRotation(transform.rotation.eulerAngles); }
    }

    void ClampRotation(Vector3 tempEulers)
    {
        tempEulers.x = ClampPlane(tempEulers.x);
        tempEulers.y = ClampPlane(tempEulers.y);
        tempEulers.z = nullArc; // ClampPlane(tempEulers.y); // *See GIST note below...
        transform.localRotation = Quaternion.Euler(tempEulers);
        ///Debug.Log(tempEulers);
    }

    float ClampPlane(float plane)
    {
        if (OkayLow(plane) || OkayHigh(plane)) DoNothing(); // Plane 'in range'.
        else if (BadLow(plane)) plane = Mathf.Clamp(plane, nullArc, maxRotationDegrees);
        else if (BadHigh(plane)) plane = Mathf.Clamp(plane, fullArc - maxRotationDegrees, fullArc);
        else Debug.LogWarning("WARN: invalid plane condition");
        return plane;
    }

    Vector2 GetMouseInput()
    {
        Vector2 mouseXY = Vector2.zero;
        if (Controller.instance)
        {
            mouseXY.x = Controller.instance.Hor; // MouseX -> rotZ.
            mouseXY.y = -Controller.instance.Vert; // MouseY -> rotX.
        }

        return mouseXY;
    }

    void SimpleRotation(Vector2 mouseXY)
    {
        Vector3 rotation = Vector3.zero;
        rotation.x = mouseXY.y * Time.deltaTime * rotationSpeed;
        rotation.y = mouseXY.x * Time.deltaTime * rotationSpeed;
        transform.eulerAngles += rotation;
    }

    void DoNothing() { }
    bool OkayHigh(float test) { return (test >= fullArc - maxRotationDegrees && test <= fullArc); }
    bool OkayLow(float test) { return (test >= nullArc && test <= maxRotationDegrees); }
    bool BadHigh(float test) { return (test > halfArc && !OkayHigh(test)); }
    bool BadLow(float test) { return (test < halfArc && !OkayLow(test)); }
}