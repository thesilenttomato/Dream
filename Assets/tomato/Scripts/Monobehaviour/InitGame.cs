using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour
{
    public AssetReference persistent;

    private void Awake()
    {
        persistent.LoadSceneAsync(LoadSceneMode.Additive);
    }
}
