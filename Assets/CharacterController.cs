using AIEngine.Movement.Agents;
using AIEngine.Movement.Components.Agents;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleCharacterMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rotationSpeed = 90f; // grados por segundo

    private CharacterController controller;
    private CmpKinematic cmpKinematic;
    private AgKinematic agent;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cmpKinematic = GetComponent<CmpKinematic>();
        agent = cmpKinematic.GetAgent();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal"); // A/D o ←/→
        float v = Input.GetAxis("Vertical");   // W/S o ↑/↓

        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.Q)) rotationInput = -1f;
        if (Input.GetKey(KeyCode.E)) rotationInput = 1f;

        // Rotar al personaje modificando agent.orientation (en radianes)
        //agent.orientation += rotationInput * rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
        transform.Rotate(0f, rotationInput * rotationSpeed * Time.deltaTime, 0f);

        // Movimiento basado en orientación
        Vector3 forward = new Vector3(Mathf.Sin(agent.orientation), 0f, Mathf.Cos(agent.orientation));
        Vector3 move = (h * Vector3.right + v * forward);

        if (move.magnitude > 1f)
            move.Normalize();

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
