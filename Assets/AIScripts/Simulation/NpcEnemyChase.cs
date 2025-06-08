using AIEngine.Decision.BehaviourTree;
using AIEngine.Decision.BehaviourTree.Tasks;
using AIEngine.Movement.Components.Algorithms;
using UnityEngine;

public class NpcEnemyChase : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 1;
    [SerializeField] private float detectionDistance = 15f;
    [SerializeField] private float stopDistance = 3f;
    [SerializeField] private float shootDistance = 10f;
    [SerializeField] private float shootCooldown = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform reloadPoint;

    private BHT_Task behaviourTree;
    private int currentAmmo;

    private void Awake()
    {
        var pathAssigner = GetComponent<PathAssignerToPathFollowingPoints>();
        var pathFollowing = GetComponent<CmpPathFollowing>();

        if (pathAssigner == null || pathFollowing == null || projectilePrefab == null)
        {
            Debug.LogError("[Enemy] Faltan referencias.");
            enabled = false;
            return;
        }

        currentAmmo = maxAmmo;

        var detector = new EntityNearSimulation(detectionDistance, gameObject);


        var reloadTask = new MoveToReloadTask(pathAssigner, pathFollowing, () =>
        {
            currentAmmo = maxAmmo;
        });

        var shootTask = new ShootAndReloadTask(
            pathAssigner, pathFollowing, detector, projectilePrefab,
            shootDistance, shootCooldown, reloadTask,
            () => currentAmmo > 0,
            () => currentAmmo <= 0,
            maxAmmo,
            () => currentAmmo--
        );

        behaviourTree = new BHT_Selector(new BHT_Task[]
        {
    // Si no tiene munición, va a recargar
    new BHT_Sequence(new BHT_Task[]
    {
        new CheckAmmoTask(() => currentAmmo <= 0),
        reloadTask
    }),

    // Si tiene munición, persigue y dispara
    new BHT_Sequence(new BHT_Task[]
    {
        detector,
        new CheckAmmoTask(() => currentAmmo > 0),
        new ChaseObjectiveSimulation(pathAssigner, pathFollowing, detector, stopDistance),
        shootTask
    })
        });
    }

    private void Update()
    {
        behaviourTree?.Run();
    }
}
