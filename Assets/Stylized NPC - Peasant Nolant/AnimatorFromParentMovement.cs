using UnityEngine;

public class AnimatorFromParentMovement : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;

    // Cu�nta velocidad necesita para que se considere que "se est� moviendo"
    public float movementThreshold = 0.01f;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calcular cu�nta distancia se ha movido desde el �ltimo frame
        float movementSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;

        // Establecer par�metro "Speed" en el Animator
        animator.SetFloat("Speed", movementSpeed);

        // Actualizar la �ltima posici�n
        lastPosition = transform.position;
    }
}