using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour
{
    public AssetReference persistent;

    private void Awake()
    {
       // Screen.SetResolution(2560, 1440, false);
        persistent.LoadSceneAsync(LoadSceneMode.Additive);
    }
}
