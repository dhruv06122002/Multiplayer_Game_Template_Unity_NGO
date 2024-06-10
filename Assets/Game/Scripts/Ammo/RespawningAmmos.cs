using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningAmmos : Ammo 
{

    public event Action<RespawningAmmos> OnCollected;

    private Vector3 previousPosition;

    private void Update()
    {
        if(previousPosition != transform.position )
        {
            Show(true);
        }

        previousPosition = transform.position;
    }
    public override int Collect()
    {
        if(!IsServer)
        {
            Show(false);
            return 0;
        }

        if (alreadyCollected) { return 0; }

        alreadyCollected = true;
        OnCollected?.Invoke(this);
        return AmmoValue; 
    }

    public void Reset()
    {
        alreadyCollected=false;
    }
}
