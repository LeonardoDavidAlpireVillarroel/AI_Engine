using UnityEngine;

public class ZonaDesaparicion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // OPCIÓN 1: Desactivar el personaje
            other.gameObject.SetActive(false);

            // OPCIÓN 2: Destruir el personaje
            // Destroy(other.gameObject);

            Debug.Log($"{other.name} ha entrado en la zona de desaparición.");
        }
    }
}
