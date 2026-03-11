using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float range;
    void Start()
    {
        Destroy(gameObject, range);
    }
}
