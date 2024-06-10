using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientSingleton : MonoBehaviour
{

    private static ClientSingleton instance;
    public ClientGameManager gameManager {  get; private set; }

    public static ClientSingleton Instance
    {
        get
        {
            if (instance != null) { return instance; }
            instance = FindAnyObjectByType<ClientSingleton>();

            if (instance == null)
            {
                Debug.LogError("No ClientSingleton in the scene");
                return null;
            }

            return instance;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public async Task<bool> CreateClient()
    {
        gameManager = new ClientGameManager();
        return await gameManager.InitAsync();
    }

}
