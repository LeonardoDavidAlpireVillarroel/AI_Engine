using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AIEngine.Movement.Components.Algorithms;
using AIEngine.Movement.Components.Agents;

public class PathAssignerToPathFollowingPoints : MonoBehaviour
{
    [Header("Componentes de pathfinding")]
    public GraphComponent graphComponent;

    [Tooltip("Transform desde donde se buscará el nodo más cercano (ej: el jugador)")]
    public Transform pathOrigin;

    [Header("Destino del path")]
    public PathFollowingPoints pathFollowing;

    [Header("Opciones de actualización")]
    public bool autoUpdate = true;
    public float updateInterval = 1f;

    private float timer;

    private IEnumerator Start()
    {
        if (graphComponent == null || pathFollowing == null || pathOrigin == null)
        {
            Debug.LogError("Faltan referencias en PathAssignerToPathFollowingPoints.");
            enabled = false;
            yield break;
        }

        timer = updateInterval;
        yield return null;

        AssignPath();
    }

    private void Update()
    {
        if (!autoUpdate) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            AssignPath();
            timer = updateInterval;
        }
    }

    public void AssignPath()
    {
        Graph graph = graphComponent.BuildGraph();
        Node closestNode = FindClosestNode(graph, pathOrigin.position);

        CmpStatic target = FindClosestCmpStaticWithPlayerTag(pathOrigin.position);
        if (target == null)
        {
            Debug.LogWarning("[PathAssigner] No se encontró ningún objetivo con tag 'Player' y CmpStatic.");
            return;
        }

        Node endNode = FindClosestNode(graph, target.transform.position);

        if (closestNode == null || endNode == null)
        {
            Debug.LogWarning("[PathAssigner] Nodo más cercano o nodo destino inválido.");
            return;
        }

        if (closestNode.id == endNode.id)
        {
            return;
        }

        AStar astar = new AStar();
        List<Node> path = astar.FindPath(graph, closestNode, endNode);

        if (path == null || path.Count == 0)
        {
            Debug.LogWarning("[PathAssigner] No se encontró un camino entre nodos.");
            return;
        }

        Vector2[] unityVertexes = new Vector2[path.Count];
        for (int i = 0; i < path.Count; i++)
        {
            unityVertexes[i] = new Vector2(path[i].position.X, path[i].position.Y);
        }

        pathFollowing.unityVertexes = unityVertexes;
        pathFollowing.InitializePath();

        CmpPathFollowing cmpPathFollowing = pathFollowing.GetComponent<CmpPathFollowing>();
        if (cmpPathFollowing != null)
        {
            cmpPathFollowing.UpdatePath(pathFollowing);
        }

        Debug.Log($"[PathAssigner] Camino actualizado: {closestNode.id} → {endNode.id} ({unityVertexes.Length} puntos)");
    }

    private Node FindClosestNode(Graph graph, Vector3 origin)
    {
        Node closest = null;
        float minDist = float.MaxValue;

        foreach (var node in graph.nodes)
        {
            Vector3 nodePos = new Vector3(node.position.X, 0, node.position.Y);
            float dist = Vector3.Distance(origin, nodePos);
            if (dist < minDist)
            {
                minDist = dist;
                closest = node;
            }
        }

        return closest;
    }

    private CmpStatic FindClosestCmpStaticWithPlayerTag(Vector3 origin)
    {
        CmpStatic[] candidates = FindObjectsByType<CmpStatic>(FindObjectsSortMode.None);
        CmpStatic closest = null;
        float minDist = float.MaxValue;

        foreach (var candidate in candidates)
        {
            if (!candidate.gameObject.CompareTag("Player"))
                continue;

            if (candidate.gameObject == this.gameObject)
                continue; // Ignorar al propio agente

            float dist = Vector3.Distance(origin, candidate.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = candidate;
            }
        }

        return closest;
    }
}
