//using UnityEngine;

//public class ReloadPointSpawner : MonoBehaviour
//{
//    [SerializeField] private GameObject reloadPointPrefab;
//    [SerializeField] private Vector3 areaCenter = Vector3.zero;
//    [SerializeField] private Vector3 areaSize = new Vector3(20, 0, 20);

//    private GameObject currentReloadPoint;

//    private void Start()
//    {
//        SpawnReloadPoint();
//    }

//    private void Update()
//    {
//        if (currentReloadPoint == null)
//        {
//            SpawnReloadPoint();
//        }
//    }

//    private void SpawnReloadPoint()
//    {
//        Vector3 randomPosition = areaCenter + new Vector3(
//            Random.Range(-areaSize.x / 2, areaSize.x / 2),
//            0,
//            Random.Range(-areaSize.z / 2, areaSize.z / 2)
//        );

//        currentReloadPoint = Instantiate(reloadPointPrefab, randomPosition, Quaternion.identity);
//        currentReloadPoint.tag = "Ammo";
//        currentReloadPoint.AddComponent<ReloadPoint>(); // Asegura que tenga el comportamiento
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireCube(areaCenter, areaSize);
//    }
//}
using UnityEngine;

public class ReloadPointSpawner : MonoBehaviour
{
    [SerializeField] private GameObject reloadPointPrefab;
    [SerializeField] private Vector3 areaCenter = Vector3.zero;
    [SerializeField] private Vector3 areaSize = new Vector3(20, 0, 20);

    private GameObject currentReloadPoint;

    private void Start()
    {
        SpawnReloadPoint();
    }

    private void Update()
    {
        if (currentReloadPoint == null)
        {
            SpawnReloadPoint();
        }/*
        else if (currentReloadPoint != null)
        {
            Destroy(currentReloadPoint);
        }*/
    }

    private void SpawnReloadPoint()
    {
        Vector3 randomPosition = areaCenter + new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            0,
            Random.Range(-areaSize.z / 2, areaSize.z / 2)
        );

        // Redondear X y Z a múltiplos de 5
        randomPosition.x = RoundToNearestMultiple(randomPosition.x, 5f);
        randomPosition.z = RoundToNearestMultiple(randomPosition.z, 5f);

        currentReloadPoint = Instantiate(reloadPointPrefab, randomPosition, Quaternion.identity);
        currentReloadPoint.tag = "Ammo";
        currentReloadPoint.AddComponent<ReloadPoint>(); // Asegura que tenga el comportamiento
    }

    // Función auxiliar para redondear a múltiplos de un número dado
    private float RoundToNearestMultiple(float value, float multiple)
    {
        return Mathf.Round(value / multiple) * multiple;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(areaCenter, areaSize);
    }
}
