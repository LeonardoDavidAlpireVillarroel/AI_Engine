using AIEngine.Decision.BehaviourTree;
using AIEngine.Decision.BehaviourTree.Tasks;
using AIEngine.Movement.Components.Algorithms;
using System;
using UnityEngine;

public class ShootAndReloadTask : BHT_Task
{
    private readonly PathAssignerToPathFollowingPoints pathAssigner;
    private readonly CmpPathFollowing pathFollowing;
    private readonly EntityNearSimulation detector;
    private readonly GameObject projectilePrefab;
    private readonly float shootDistance;
    private readonly float shootCooldown;
    private readonly MoveToReloadTask reloadTask;
    private readonly System.Func<bool> hasAmmo;
    private readonly System.Func<bool> noAmmo;
    private readonly int maxAmmo;
    private readonly System.Action onShoot;

    private float lastShootTime;
    private enum State { Shooting, Reloading }
    private State currentState = State.Shooting;
    private BHT_Selector reloadLogic;
    private Func<bool> value1;
    private Func<bool> value2;
    private Action value3;

    public ShootAndReloadTask(
        PathAssignerToPathFollowingPoints pathAssigner,
        CmpPathFollowing pathFollowing,
        EntityNearSimulation detector,
        GameObject projectilePrefab,
        float shootDistance,
        float shootCooldown,
        MoveToReloadTask reloadTask,
        System.Func<bool> hasAmmo,
        System.Func<bool> noAmmo,
        int maxAmmo,
        System.Action onShoot)
    {
        this.pathAssigner = pathAssigner;
        this.pathFollowing = pathFollowing;
        this.detector = detector;
        this.projectilePrefab = projectilePrefab;
        this.shootDistance = shootDistance;
        this.shootCooldown = shootCooldown;
        this.reloadTask = reloadTask;
        this.hasAmmo = hasAmmo;
        this.noAmmo = noAmmo;
        this.maxAmmo = maxAmmo;
        this.onShoot = onShoot;
    }

    public ShootAndReloadTask(PathAssignerToPathFollowingPoints pathAssigner, CmpPathFollowing pathFollowing, EntityNearSimulation detector, GameObject projectilePrefab, float shootDistance, float shootCooldown, BHT_Selector reloadLogic, Func<bool> value1, Func<bool> value2, int maxAmmo, Action value3)
    {
        this.pathAssigner = pathAssigner;
        this.pathFollowing = pathFollowing;
        this.detector = detector;
        this.projectilePrefab = projectilePrefab;
        this.shootDistance = shootDistance;
        this.shootCooldown = shootCooldown;
        this.reloadLogic = reloadLogic;
        this.value1 = value1;
        this.value2 = value2;
        this.maxAmmo = maxAmmo;
        this.value3 = value3;
    }

    public override bool Run()
    {
        if (detector.Target == null)
            return false;

        float distance = Vector3.Distance(pathFollowing.transform.position, detector.Target.transform.position);

        switch (currentState)
        {
            case State.Shooting:
                if (hasAmmo() && distance <= shootDistance && Time.time - lastShootTime >= shootCooldown)
                {
                    Shoot();
                    onShoot?.Invoke();
                    lastShootTime = Time.time;

                    if (noAmmo())
                        currentState = State.Reloading;
                }
                break;

            case State.Reloading:
                if (reloadTask.Run())
                    currentState = State.Shooting;
                break;
        }

        return true;
    }

    private void Shoot()
    {
        Vector3 origin = pathFollowing.transform.position + Vector3.up * 1.5f;
        Vector3 target = detector.Target.transform.position + Vector3.up;
        Vector3 direction = (target - origin).normalized;

        GameObject projectile = GameObject.Instantiate(projectilePrefab, origin, Quaternion.LookRotation(direction));

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction);
        }

        Debug.Log("[Enemy] Disparo realizado");
    }

}
