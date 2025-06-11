using UnityEngine;

public class FaceMovementDirection : MonoBehaviour
{
    private Vector3 lastPosition;

    public float rotationSpeed = 10f; // Velocidad de giro

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 movement = transform.position - lastPosition;

        // Ignorar peque�os movimientos o sacudidas
        if (movement.magnitude > 0.01f)
        {
            // Solo usar direcci�n horizontal (X y Z), ignorar Y
            Vector3 direction = new Vector3(movement.x, 0f, movement.z).normalized;

            // Calcular la rotaci�n hacia la direcci�n del movimiento
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Girar suavemente hacia la nueva direcci�n
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        lastPosition = transform.position;
    }
}