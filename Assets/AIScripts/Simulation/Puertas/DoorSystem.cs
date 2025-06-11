using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public PressurePlate[] plates;
    public Transform door;
    public Vector3 openOffset = new Vector3(0, 0, 5);
    public float openSpeed = 2f;

    private Vector3 closedPos;
    private Vector3 openPos;
    public bool isOpen = false;
    public bool IsOpen()
    {
        return isOpen;
    }

    void Start()
    {
        closedPos = door.position;
        openPos = closedPos + openOffset;

        foreach (var plate in plates)
        {
            plate.OnPlateActivated += CheckAllPlates;
        }

        Debug.Log("Puerta inicializada. Posición cerrada: " + closedPos + ", posición abierta: " + openPos);
    }

    void CheckAllPlates(PressurePlate _)
    {
        foreach (var plate in plates)
        {
            if (!plate.IsActivated())
            {
                Debug.Log("Una placa aún no está activada.");
                return;
            }
        }

        isOpen = true;
        Debug.Log("¡Todas las placas activadas! Abriendo puerta...");
    }

    void Update()
    {
        if (isOpen)
        {
            door.position = Vector3.MoveTowards(door.position, openPos, openSpeed * Time.deltaTime);
            Debug.Log("Moviendo puerta hacia posición abierta: " + door.position);
        }
    }
}