using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public Transform playerReturnPoint;

    private void OnTriggerEnter(Collider other)
    {
        HP hp = other.GetComponentInParent<HP>();

        if (hp == null)
        {
            Debug.Log("what is this");
            return;
        }

        if(hp.isPlayer == true)
        {
            Debug.Log("Hi mark");

            //damage the player for 25 HP
            hp.TakeDamage(10);

            //return the player to the return position
            Transform root = other.transform.root;
            root.position = playerReturnPoint.position;

            //reset players velocity
            Rigidbody rb = hp.GetComponent<Rigidbody>();

            if (rb == null)
            {
                Debug.Log("no rigidbody");
            }

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.position = playerReturnPoint.position;
                rb.rotation = playerReturnPoint.rotation;
            }
            else
            {
                // fallback for when a rigidbody is not found.
                other.transform.root.position = playerReturnPoint.position;
            }
        }
        else
        {
            hp.TakeDamage(9999f);
        }
    }
}
