using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
public class SceneLoadManger : MonoBehaviour
{
    public FadePannel fadePannel;
    private AssetReference CurrentScene;
    public AssetReference Fight;
    public AssetReference Menu;
    public AssetReference yesterday;

    private void Awake()
    {
       // LoadMenu();
    }

    private async Awaitable LoadSceneTask()
    {
        //var s = Addressables.LoadSceneAsync(CurrentScene,LoadSceneMode.Additive);
        fadePannel.FadeOut(0.2f);
        var s = CurrentScene.LoadSceneAsync(LoadSceneMode.Additive);

        await s.Task;
        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(s.Result.Scene);
        } 
    }

    private async Awaitable UnLoadScene()
    {
        fadePannel.FadeIn(0.4f);
        await Awaitable.WaitForSecondsAsync(0.45f);
        await Awaitable.FromAsyncOperation(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene())) ;
    }
    public async void LoadFight()
    {
        if (CurrentScene != null)
        {
            await UnLoadScene();
        }
        CurrentScene = Fight;
        await LoadSceneTask();
    }
    public async void LoadYesterday()
    {
        if (CurrentScene != null)
        {
            await UnLoadScene();
        }
        CurrentScene = yesterday;
        await LoadSceneTask();
    }
    public async void LoadMenu()
    {
        if (CurrentScene != null)
        {
            await UnLoadScene();
        }
        CurrentScene = Menu;
        await LoadSceneTask();
    }
    
}
