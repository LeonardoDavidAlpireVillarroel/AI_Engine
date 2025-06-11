using UnityEngine;
using System;

public class PressurePlate : MonoBehaviour
{
    public float activationTime = 5f;
    private float timer = 0f;
    private bool isEntityOnPlate = false;
    private bool isActivated = false;

    public event Action<PressurePlate> OnPlateActivated;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // O la etiqueta de la entidad
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
                isActivated = true;
                OnPlateActivated?.Invoke(this);
            }
        }
    }

    public bool IsActivated() => isActivated;
}
