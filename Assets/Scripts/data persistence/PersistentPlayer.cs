using UnityEngine;

public class PersistentPlayer : MonoBehaviour
{
    public static PersistentPlayer Instance;

    public Rigidbody playerBody;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // destroy duplicate
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
