using UnityEngine;
using System.Collections;

public class ReloadPoint : MonoBehaviour
{
    [Header("Tiempo antes de destruir (segundos)")]
    public float tiempoAntesDeDestruir = 2f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("[ReloadPoint] Recarga recogida");

            // Aquí puedes hacer la lógica de recarga si hace falta

            // Inicia la coroutine que destruye luego de un tiempo
            StartCoroutine(DestruirConRetardo());
        }
    }

    private IEnumerator DestruirConRetardo()
    {
        yield return new WaitForSeconds(tiempoAntesDeDestruir);
        Destroy(this.gameObject);
    }
}/*using System.Threading;
using UnityEngine;

public class ReloadPoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("[ReloadPoint] Recarga recogida");
            Timer 2;
            Destroy(this.gameObject);
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyRecarga scriptRecarga = other.GetComponent<EnemyRecarga>();
            if (scriptRecarga != null)
            {
                scriptRecarga.RecargarYDestruir(this.gameObject);
            }
        }
    }*/
