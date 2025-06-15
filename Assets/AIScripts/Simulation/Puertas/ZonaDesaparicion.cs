using UnityEngine;
using TMPro;
using UnityEditor;
using System.Collections;

public class ZonaDesaparicion : MonoBehaviour
{
    public TextMeshProUGUI texto;            // Texto que muestra mensajes finales
    public TextMeshProUGUI temporizadorUI;   // Texto que muestra la cuenta atrás

    public float tiempoLimite = 5f;
    public string mensajePerdido = "¡Has perdido!";
    public string mensajeTiempoAgotado = "Se acabó el tiempo, simulación terminada.";

    private bool juegoTerminado = false;

    private void Start()
    {
        texto.gameObject.SetActive(false);
        StartCoroutine(Temporizador());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!juegoTerminado && other.CompareTag("Player"))
        {
            juegoTerminado = true;
            StopAllCoroutines();

            Destroy(other.gameObject);

            texto.text = mensajePerdido;
            texto.gameObject.SetActive(true);
            temporizadorUI.text = ""; // Borra la cuenta atrás

            StartCoroutine(FinalizarJuegoConRetardo(3f));
        }
    }

    private IEnumerator Temporizador()
    {
        float tiempoRestante = tiempoLimite;

        while (tiempoRestante > 0f)
        {
            temporizadorUI.text = $"Tiempo restante: {Mathf.Ceil(tiempoRestante)}s";
            yield return new WaitForSecondsRealtime(1f);
            tiempoRestante -= 1f;
        }

        if (!juegoTerminado)
        {
            juegoTerminado = true;

            texto.text = mensajeTiempoAgotado;
            texto.gameObject.SetActive(true);
            temporizadorUI.text = ""; // Oculta el contador

            StartCoroutine(FinalizarJuegoConRetardo(3f));
        }
    }

    private IEnumerator FinalizarJuegoConRetardo(float segundos)
    {
        yield return new WaitForSecondsRealtime(segundos);

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

