using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Collections;

public class ZonaDesaparicion : MonoBehaviour
{
    public TextMeshProUGUI texto;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            texto.gameObject.SetActive(true);
            Tiempo();
            EditorApplication.isPlaying = false;
        }
    }
    public IEnumerator Tiempo()
    {

        yield return new WaitForSecondsRealtime(5f);
    }
}
