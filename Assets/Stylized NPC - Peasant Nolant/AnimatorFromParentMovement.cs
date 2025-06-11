using UnityEngine;

public class AnimatorFromParentMovement : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;

    // Cuánta velocidad necesita para que se considere que "se está moviendo"
    public float movementThreshold = 0.01f;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calcular cuánta distancia se ha movido desde el último frame
        float movementSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;

        // Establecer parámetro "Speed" en el Animator
        animator.SetFloat("Speed", movementSpeed);

        // Actualizar la última posición
        lastPosition = transform.position;
    }
}