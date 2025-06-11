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

        // Ignorar pequeños movimientos o sacudidas
        if (movement.magnitude > 0.01f)
        {
            // Solo usar dirección horizontal (X y Z), ignorar Y
            Vector3 direction = new Vector3(movement.x, 0f, movement.z).normalized;

            // Calcular la rotación hacia la dirección del movimiento
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Girar suavemente hacia la nueva dirección
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        lastPosition = transform.position;
    }
}