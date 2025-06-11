using UnityEngine;

public class ZonaDesaparicion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // OPCI�N 1: Desactivar el personaje
            other.gameObject.SetActive(false);

            // OPCI�N 2: Destruir el personaje
            // Destroy(other.gameObject);

            Debug.Log($"{other.name} ha entrado en la zona de desaparici�n.");
        }
    }
}
