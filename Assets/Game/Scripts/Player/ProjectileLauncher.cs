using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileLauncher : NetworkBehaviour
{
    [Header("Referencer")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private AmmoWallet wallet;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject serverProjectilePrefab;
    [SerializeField] private GameObject clientProjectilePrefab;
    [SerializeField] private GameObject MuzzleFlash;
    [SerializeField] private Collider2D playerCollider;

    [Header("Settings")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float MuzzleFlashDuration;
    [SerializeField] private int CostToFire;

    private bool shouldFire;
    private float timer;
    private float muzzleFlashTimer;
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) { return; }

        inputReader.PrimaryFireEvent += HandlePrimaryFire;
 
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; }

        inputReader.PrimaryFireEvent -= HandlePrimaryFire;
    }


    private void HandlePrimaryFire(bool shouldFire)
    {
        this.shouldFire = shouldFire; 
    }

    [ServerRpc]
    private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
    {

        if (wallet.TotalAmmos.Value < CostToFire) { return; }
        wallet.SpendAmmos(CostToFire);

        GameObject projectileInstance = Instantiate(
           serverProjectilePrefab,
           spawnPos,
           Quaternion.identity);

        projectileInstance.transform.up = direction;

        Physics2D.IgnoreCollision(playerCollider,projectileInstance.GetComponent<Collider2D>());


        if (projectileInstance.TryGetComponent<DealDamageOnContact>(out DealDamageOnContact dealDamage))
        {
            dealDamage.SetOwner(OwnerClientId);
        }

        if (projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * projectileSpeed;
        }

        spawnDummyProjectileClientRpc(spawnPos, direction);
    }


    void Update()
    {
        if(muzzleFlashTimer>0)
        {
            muzzleFlashTimer -= Time.deltaTime;
            {
                if(muzzleFlashTimer<=0f)
                {
                    MuzzleFlash.SetActive(false);
                }
            }
        }
        if(!IsOwner) { return; }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        

        if (!shouldFire) { return; }

        if(timer > 0){return;}
         
        if(wallet.TotalAmmos.Value < CostToFire) { return;}


        PrimaryFireServerRpc(projectileSpawnPoint.position, projectileSpawnPoint.up);

        spawnDummyProjectile(projectileSpawnPoint.position, projectileSpawnPoint.up);
        
        timer = 1/ fireRate;
    }

    [ClientRpc]
    private void spawnDummyProjectileClientRpc(Vector3 spawnPos, Vector3 direction)
    {
        if(IsOwner) { return;}
        spawnDummyProjectile(spawnPos , direction);
    }

    private void spawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
        MuzzleFlash.SetActive(true);
        muzzleFlashTimer = MuzzleFlashDuration;
       GameObject projectileInstance = Instantiate(
           clientProjectilePrefab, 
           spawnPos, 
           Quaternion.identity);

        projectileInstance.transform.up = direction;

        Physics2D.IgnoreCollision(playerCollider, projectileInstance.GetComponent<Collider2D>());

        if(projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * projectileSpeed;
        }
    }
}
