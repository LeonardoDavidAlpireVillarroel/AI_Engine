using UnityEngine;
using System;

[RequireComponent(typeof(Collider), typeof(Renderer))]
public class PressurePlate : MonoBehaviour
{
    public float activationTime = 5f;
    public Color colorActivado = Color.green;
    public float hundimiento = 0.1f;

    private float timer = 0f;
    private bool isEntityOnPlate = false;
    private bool isActivated = false;

    public event Action<PressurePlate> OnPlateActivated;

    private Renderer rend;
    private Color colorOriginal;
    private Vector3 posicionOriginal;

    void Start()
    {
        rend = GetComponent<Renderer>();
        colorOriginal = rend.material.color;
        posicionOriginal = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // o "Agente" si lo prefieres
            isEntityOnPlate = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEntityOnPlate = false;
            timer = 0f;
        }
    }

    void Update()
    {
        if (isEntityOnPlate && !isActivated)
        {
            timer += Time.deltaTime;
            if (timer >= activationTime)
            {
                ActivarPlaca();
            }
        }
    }

    void ActivarPlaca()
    {
        isActivated = true;
        OnPlateActivated?.Invoke(this);

        // Cambiar color
        rend.material.color = colorActivado;

        // Hundir placa
        transform.position = posicionOriginal - new Vector3(0, hundimiento, 0);
    }

    public bool IsActivated() => isActivated;
}
