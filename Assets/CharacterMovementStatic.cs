using AIEngine.Movement.Agents;
using AIEngine.Movement.Components.Agents;
using UnityEngine;
using NumVec2 = System.Numerics.Vector2;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementStatic : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;
    private CmpStatic cmpStatic;
    private AgStatic agent;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cmpStatic = GetComponent<CmpStatic>();
        agent = cmpStatic.GetAgent();
    }

    private void Update()
    {
        // Obtener input del jugador
        float h = Input.GetAxisRaw("Horizontal"); // A/D o ←/→
        float v = Input.GetAxisRaw("Vertical");   // W/S o ↑/↓

        NumVec2 moveDir = new NumVec2(h, v);
        if (moveDir.Length() > 1f)
            moveDir = NumVec2.Normalize(moveDir);

        // Actualizar posición del agente
        agent.position += moveDir * moveSpeed * Time.deltaTime;

        // Mover visualmente el GameObject (solo en XZ)
        Vector3 newPos = new Vector3(agent.position.X, transform.position.y, agent.position.Y);
        Vector3 delta = newPos - transform.position;
        controller.Move(delta); // O usar transform.position = newPos si no necesitas colisiones
    }
}
