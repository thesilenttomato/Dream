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
    public AssetReference hero;
    public AssetReference mid;
    public BoolEventSO audioPlay;
    public ObjectEventSO afterLoadFight;
    public IntVarible TheEndGame;
    public IntVarible Hp;
    private bool Onetime;
    public UIManger UIManger;

    private void Awake()
    {
        LoadMenu();
       //CurrentScene = yesterday;
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
        Hp.currentVaule = Hp.maxVaule;
        Debug.Log("LoadFight");
        if (CurrentScene != null)
        {
            await UnLoadScene();
        }
        CurrentScene = Fight;
        await LoadSceneTask();
        audioPlay.RaiseEvent(true,this);
        afterLoadFight.RaiseEvent(null,this);
        
        Onetime = true;
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
        Debug.Log("LoadMenu");
        if (CurrentScene != null)
        {
            await UnLoadScene();
        }
        
        CurrentScene = Menu;
        await LoadSceneTask();
        audioPlay.RaiseEvent(false,this);
    }
    public async void LoadHero()
    {
        if (CurrentScene != null)
        {
            await UnLoadScene();
        }
        
        CurrentScene = hero;
        await LoadSceneTask();
    }
    public async void TimeToLoadMid(int hour)
    {
        
        if (CurrentScene != Fight)
        {
            return;
        }
        Debug.Log("TimeToLoadMid");
        if (CurrentScene != null)
        {
            await UnLoadScene();
        }
        
        CurrentScene = mid;
        await LoadSceneTask();
    }
    public async void DieToLoadMid(int hp)
    {
        
        if (hp > 0)
        {
            return;
        }

        if (!Onetime)
        {
            return;
        }

        Onetime = false;
        Debug.Log("DieToLoadMid");
        if (CurrentScene != null)
        {
            await UnLoadScene();
        }
        
        CurrentScene = mid;
        await LoadSceneTask();
    }
    public async void EndToLoadMid()
    {
        if (TheEndGame.currentVaule == 1)
        {
            return;
        }
        TheEndGame.currentVaule = 1;
        Debug.Log("EndToLoadMid");
        if (CurrentScene != null)
        {
            await UnLoadScene();
        }
        
        CurrentScene = mid;
        await LoadSceneTask();
    }

    
}
