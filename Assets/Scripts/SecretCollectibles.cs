using UnityEngine;

public class SecretCollectibles : MonoBehaviour
{
    bool isCollected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            if (!isCollected)
            {
                HUD.instance.setCollectables(1);
                isCollected = true;
            }
            Destroy(gameObject); 
        }
    }
}
