using UnityEngine;

public class Billboarding : MonoBehaviour
{
    //keeps 2D objects facing the main camera

    void Update()
    {
        Camera cam = Camera.main;
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
