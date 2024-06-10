using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinWallet : NetworkBehaviour
{
    public NetworkVariable<int> TotalCoints = new NetworkVariable<int>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.TryGetComponent<Coin>(out Coin coin)) {  return; }

        int coinValue = coin.Collect();

        if (!IsServer) { return; }  

        TotalCoints.Value += coinValue;
    }
}
