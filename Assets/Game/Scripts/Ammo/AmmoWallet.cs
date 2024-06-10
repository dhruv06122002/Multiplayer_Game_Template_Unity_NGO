using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AmmoWallet : NetworkBehaviour
{
    public NetworkVariable<int> TotalAmmos = new NetworkVariable<int>();

    public void SpendAmmos(int costToFire)
    {
        TotalAmmos.Value -= costToFire;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ammo");
        if(!collision.TryGetComponent<Ammo>(out Ammo ammo)) {  return; }
        int AmmosValue = ammo.Collect();
        if (!IsServer) { return; }
        TotalAmmos.Value += AmmosValue;
    }
}
