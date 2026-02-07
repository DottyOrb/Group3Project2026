using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //this is the players camera controls and stuff

    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;

    float xRotation;
    float yRotation;

    public float targetFOV;
    public float lerpSpeed;
    public float tiltLerpSpeed;
    private float currentTilt;
    public float targetTilt;
    bool isDone;
    Camera cam;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = GetComponent<Camera>();
        targetFOV = cam.fieldOfView;
        isDone = true;
    }

    private void Update()
    {
        //gets the moust input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);


        //rotate camera orientation
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltLerpSpeed);
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, currentTilt);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        LerpFOV();
    }

    private void LerpFOV()
    {
        if (isDone) return;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * lerpSpeed);
        if (Mathf.Abs(cam.fieldOfView - targetFOV) <= 0.1f)
        {
            cam.fieldOfView = targetFOV;
            isDone = true;
        }
    }

    public void SetFOV(float endVal)
    {
        targetFOV = endVal;
        isDone = false;
    }

    public void cameraTilt(float zTilt)
    {
        targetTilt = zTilt;
    }
}
