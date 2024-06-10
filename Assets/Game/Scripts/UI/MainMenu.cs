using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private TMP_InputField joinCodeField;
    public async void StartHost()
    {
        await HostSingleton.Instance.gameManager.StartHostAsync();
    }

    public async void StartClient()
    {
        Debug.Log(joinCodeField.text);
        await ClientSingleton.Instance.gameManager.StartClientAsync(joinCodeField.text);
        
    }
}
