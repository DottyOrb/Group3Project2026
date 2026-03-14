using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab; // Just here to help check for typos

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name + ", parent: " + other.transform.parent.name);

        if (WeaponSwap.Instance != null && weaponPrefab != null && !WeaponSwap.Instance.IsUnlocked(weaponPrefab))
        {
            WeaponSwap.Instance.WeaponUnlock(weaponPrefab);
            Destroy(gameObject);
        }
    }
}

