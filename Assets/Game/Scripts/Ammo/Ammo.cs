using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Ammo : NetworkBehaviour
{
    [SerializeField]    private SpriteRenderer SpriteRenderer;

    protected int AmmoValue = 1;

    protected bool alreadyCollected;

    public abstract int Collect();

    public void SetValue(int value)
    {
        AmmoValue = value;
    }

    protected void Show(bool show)
    {
        SpriteRenderer.enabled = show;
    }
}
