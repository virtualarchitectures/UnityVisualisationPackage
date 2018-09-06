//Rotate camera around target when mouse button pressed
//Zoom with scroll wheel

using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera Control/Mouse Orbit Improved")]
public class MouseOrbitImprovedEdit : MonoBehaviour
{
    public Transform camTarget;

    //Leaving camRaycastIgnore set to 'nothing' is a hackyfix for raycast hit issue with mesh colliders
    public LayerMask camRaycastIgnore;

    public float camTargetDistance = 80;
    public float xSpeed = 0.5f;
    public float ySpeed = 180.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float scrollSpeed = 200f;
    public float minTargetDist = 50f;
    public float maxTargetDist = 800f;

    private new Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    public float MinTargetDist
    {
        get
        {
            return minTargetDist;
        }

        set
        {
            minTargetDist = value;
        }
    }

    public float MaxTargetDist
    {
        get
        {
            return maxTargetDist;
        }

        set
        {
            maxTargetDist = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (camTarget)
        {
            //Rotate with Mouse
            if (Input.GetMouseButton(1))
            {

                x += Input.GetAxis("Mouse X") * xSpeed * camTargetDistance * 0.02f;

                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                SetPosition();
            }

            //Zoom with mouse
            if (Input.GetMouseButton(2))
            {
                camTargetDistance -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                camTargetDistance = Mathf.Clamp(camTargetDistance, MinTargetDist, maxTargetDist);

                SetPosition();
            }

            //Zoom with scroll wheel
            camTargetDistance = Mathf.Clamp(camTargetDistance - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, MinTargetDist, maxTargetDist);

            //Rotate camera with cursor keys
            x -= Input.GetAxis("Horizontal") * xSpeed * camTargetDistance * 0.02f;

            //Zoom with cursor keys
            if (Input.GetKey(KeyCode.LeftShift))
            {
                camTargetDistance -= Input.GetAxis("Vertical") * ySpeed * 0.02f;

                camTargetDistance = Mathf.Clamp(camTargetDistance, MinTargetDist, maxTargetDist);

                SetPosition();
            }

            //Change elevation with cursor keys
            else if (!Input.GetMouseButton(1))
            {
                y += Input.GetAxis("Vertical") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                SetPosition();
            }
        }
    }

    public void SetPosition()
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        //camTargetDistance = Mathf.Clamp(camTargetDistance - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, MinTargetDist, maxTargetDist);

        RaycastHit hit;
        if (Physics.Linecast(camTarget.position, transform.position, out hit, camRaycastIgnore))
        {
            camTargetDistance -= hit.distance;
        }
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -camTargetDistance);
        Vector3 position = rotation * negDistance + camTarget.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}