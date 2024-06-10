using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] private ClientSingleton ClientPrefab;
    [SerializeField] private HostSingleton HostPrefab;

    private async void Start()
    {
        DontDestroyOnLoad(gameObject);

        await LaunchInMode(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null);
    }

    private async Task LaunchInMode(bool isDedicatedServer)
    {
        if (isDedicatedServer)
        {

        }
        else
        {
            HostSingleton hostSingleton = Instantiate(HostPrefab);
            hostSingleton.CreateHost();

            ClientSingleton clientSingleton = Instantiate(ClientPrefab);
            bool authenticated =  await clientSingleton.CreateClient();

            if(authenticated)
            {
                clientSingleton.gameManager.GoToMenu();
            }
        }
    }
}
