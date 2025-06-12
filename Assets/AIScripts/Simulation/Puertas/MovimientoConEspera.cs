using System.Collections;
using System.Linq;
using UnityEngine;
using AIEngine.Movement.Components.Algorithms;
using AIEngine.Movement.Components.Agents;

public class MovimientoConEspera : MonoBehaviour
{
    public Transform destinoFinal; // Punto al que ir cuando todas las placas estén activadas
    public PressurePlate[] placas; // Todas las placas del nivel

    private CmpSeek cmpSeek;
    private float velocidadOriginal;
    private PressurePlate objetivoActual;
    private bool esperando = false;

    void Start()
    {
        cmpSeek = GetComponent<CmpSeek>();
        velocidadOriginal = cmpSeek.maxSpeed;

        BuscarYAsignarPlacaMasCercana(); // Primer destino: la placa más cercana
    }

    void Update()
    {
        if (!esperando && objetivoActual != null)
        {
            float distancia = Vector3.Distance(transform.position, objetivoActual.transform.position);
            if (distancia < 0.3f && !objetivoActual.IsActivated())
            {
                StartCoroutine(EsperarYActivar());
            }
        }
    }

    IEnumerator EsperarYActivar()
    {
        esperando = true;
        cmpSeek.maxSpeed = 0;

        yield return new WaitUntil(() => objetivoActual.IsActivated());

        if (TodasPlacasActivadas())
        {
            CmpStatic destinoPuerta = destinoFinal.GetComponent<CmpStatic>();
            cmpSeek.SetTarget(destinoPuerta);
        }
        else
        {
            BuscarYAsignarPlacaMasCercana();
        }

        cmpSeek.maxSpeed = velocidadOriginal;
        esperando = false;
    }

    void BuscarYAsignarPlacaMasCercana()
    {
        PressurePlate[] desactivadas = placas
            .Where(p => !p.IsActivated())
            .ToArray();

        if (desactivadas.Length == 0)
        {
            Debug.Log("No quedan placas desactivadas.");
            return;
        }

        objetivoActual = desactivadas
            .OrderBy(p => Vector3.Distance(transform.position, p.transform.position))
            .FirstOrDefault();

        CmpStatic targetStatic = objetivoActual.GetComponent<CmpStatic>();
        if (targetStatic != null)
        {
            cmpSeek.SetTarget(targetStatic);
        }
        else
        {
            Debug.LogError("La placa no tiene componente CmpStatic");
        }
    }

    bool TodasPlacasActivadas()
    {
        return placas.All(p => p.IsActivated());
    }
}
