using UnityEngine;

public class ReloadPoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("[ReloadPoint] Recarga recogida");
            Destroy(this.gameObject);
        }
    }
}