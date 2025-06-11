using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [SerializeField] private float floatAmplitude = 0.5f;  // Qu� tanto sube y baja
    [SerializeField] private float floatFrequency = 1f;     // Qu� tan r�pido oscila

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
