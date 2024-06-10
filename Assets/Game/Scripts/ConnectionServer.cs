using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ConnectionServer : MonoBehaviour
{
    [SerializeField] private GameObject Red_Tank;
    [SerializeField] private GameObject black_Tank;
    [SerializeField] private GameObject green_Tank;
    [SerializeField] private GameObject purple_Tank;
    [SerializeField] private GameObject yellow_Tank;
    [SerializeField] private GameObject Select_Tank_Panel;

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void Yellow()
    {
        NetworkManager.Singleton.NetworkConfig.PlayerPrefab = yellow_Tank;
        Select_Tank_Panel.SetActive(false);
    }

    public void Red()
    {
        NetworkManager.Singleton.NetworkConfig.PlayerPrefab = Red_Tank;
        Select_Tank_Panel.SetActive(false);
    }

    public void Green()
    {
        NetworkManager.Singleton.NetworkConfig.PlayerPrefab = green_Tank;
        Select_Tank_Panel.SetActive(false);
    }

    public void Black()
    {
        NetworkManager.Singleton.NetworkConfig.PlayerPrefab = black_Tank;
        Select_Tank_Panel.SetActive(false);
    }

    public void Purple()
    {
        //NetworkManager.Singleton.AddNetworkPrefab(aaa);
        NetworkManager.Singleton.NetworkConfig.PlayerPrefab = purple_Tank;
        Select_Tank_Panel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
