using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovePlayerToStart : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(MovePlayer());
    }

    IEnumerator MovePlayer()
    {
        yield return new WaitForFixedUpdate();

        Rigidbody rb = PersistentPlayer.Instance.playerBody;

        rb.isKinematic = true;

        rb.transform.SetPositionAndRotation(
            spawnPoint.position,
            spawnPoint.rotation
        );

        rb.isKinematic = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
