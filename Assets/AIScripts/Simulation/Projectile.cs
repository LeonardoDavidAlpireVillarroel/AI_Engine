using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;

    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí puede ir la lógica para dañar al jugador
            Debug.Log("Jugador golpeado por proyectil");

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy")) // evitar chocar con el enemigo mismo
        {
            Destroy(gameObject);
        }
    }
}
