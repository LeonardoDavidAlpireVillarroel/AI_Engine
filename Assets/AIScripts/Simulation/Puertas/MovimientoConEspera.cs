using System.Collections;
using UnityEngine;
using AIEngine.Movement.Components.Algorithms; // CmpSeek2
using AIEngine.Movement.Components.Agents;     // CmpStatic2

public class MovimientoConEspera : MonoBehaviour
{
    public DoorSystem puerta;
    public Transform[] puntosDestino; // Los objetos deben tener CmpStatic2
    public float distanciaMinima = 0.2f;
    public float tiempoEspera = 5f;

    private int indiceActual = 0;
    private CmpSeek cmpSeek;
    private bool esperando = false;
    private float velocidadOriginal;

    void Start()
    {
        cmpSeek = GetComponent<CmpSeek>();
        velocidadOriginal = cmpSeek.maxSpeed;

        if (puntosDestino.Length > 0)
        {
            AsignarNuevoDestino();
        }
        else
        {
            Debug.LogError("No se han asignado puntos de destino.");
        }
    }

    void Update()
    {
        if (!esperando && cmpSeek.target != null)
        {
            float distancia = Vector3.Distance(transform.position, cmpSeek.target.transform.position);
            if (distancia < distanciaMinima)
            {
                esperando = true;
                StartCoroutine(EsperarYIrAlSiguiente());
            }
        }
    }

    IEnumerator EsperarYIrAlSiguiente()
    {
        cmpSeek.maxSpeed = 0;

        Debug.Log("Esperando a que se abra la puerta...");
        yield return new WaitUntil(() => puerta != null && puerta.IsOpen());

        indiceActual++;
        if (indiceActual >= puntosDestino.Length)
        {
            cmpSeek.target = null;
            yield break; // Ya no hay más destinos
        }

        AsignarNuevoDestino();
        cmpSeek.maxSpeed = velocidadOriginal;
        esperando = false;
    }

    void AsignarNuevoDestino()
    {
        Transform destino = puntosDestino[indiceActual];
        CmpStatic agenteDestino = destino.GetComponent<CmpStatic>();

        if (agenteDestino != null)
        {
            cmpSeek.SetTarget(agenteDestino);
        }
        else
        {
            Debug.LogError($"El punto de destino '{destino.name}' no tiene un componente CmpStatic.");
        }
    }
}

