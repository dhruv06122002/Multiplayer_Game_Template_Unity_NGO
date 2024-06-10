using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AmmoSpawner : NetworkBehaviour
{
    [SerializeField] private RespawningAmmos AmmoPrefab;
    
    [SerializeField] private int maxAmmo = 50;
    
    [SerializeField] private int AmmoValue = 10;
    
    [SerializeField] private Vector2 xSpawnRange;
    
    [SerializeField] private Vector2 ySpawnRange;

    [SerializeField] private LayerMask layerMask;

    private Collider2D[] coinBuffer = new Collider2D[1];
    private float AmmoRadius;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        AmmoRadius = AmmoPrefab.GetComponent<BoxCollider2D>().edgeRadius;
        for(int i = 0;i<maxAmmo;i++) 
        { 
            SpawnAmmo(); 
        }
    }
    private void SpawnAmmo()
    {
        RespawningAmmos ammoInstance = Instantiate(
           AmmoPrefab, 
           GetSpawnPoint(),
           Quaternion.identity);

        ammoInstance.SetValue(AmmoValue);
        ammoInstance.GetComponent<NetworkObject>().Spawn();

        ammoInstance.OnCollected += HandleAmmoCollected;

    }

    private void HandleAmmoCollected(RespawningAmmos Ammo)
    {
        Ammo.transform.position = GetSpawnPoint();
        Ammo.Reset();
    }

    private Vector2 GetSpawnPoint()
    {
        float x = 0;
        float y = 0;
        while(true) 
        {
            x = Random.Range(xSpawnRange.x, xSpawnRange.y);
            y = Random.Range(ySpawnRange.x, ySpawnRange.y);
            Vector2 spawnPoint = new Vector2(x, y);
            int numColliders = Physics2D.OverlapCircleNonAlloc(spawnPoint,AmmoRadius,coinBuffer, layerMask);
            if(numColliders == 0)
            {
                return spawnPoint;
            }
        }
    }
}
