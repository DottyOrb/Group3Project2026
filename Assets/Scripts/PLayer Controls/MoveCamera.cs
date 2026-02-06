using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //keeps the camera object on the player

    public Transform cameraPos;

    private void Update()
    {
        transform.position = cameraPos.position;
    }
}
