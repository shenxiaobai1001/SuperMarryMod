using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ModVideoPlayerCreater : MonoBehaviour
{
    public static ModVideoPlayerCreater Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public GameObject ModVideoPlayer;
    public Transform videoParent;
    public bool IsPlaying = false;

    private void Start()
    {
        EventManager.Instance.AddListener(Events.OnModVideoPlayEnd, OnVideoPlayEnd);
    }

    public void OnPlayDJ() 
    {
        int number = Random.Range(1, 13);
        OnCreateModVideoPlayer(Vector3.zero, Vector3.one, Vector3.zero, $"DJ/{number}",1);
    }
    public void OnPlayWuSaQi()
    {
        int number = Random.Range(1, 13);
        float scaleValue = Random.Range(0.5f, 2);
        int rotateValue= Random.Range(0, 360);
        Vector3 scale = new Vector3(scaleValue, scaleValue,1);
        Vector3 rotate = new Vector3(0, 0, rotateValue);
        OnCreateModVideoPlayer(Vector3.zero, scale, rotate, "GreenScreen/wusaqi", 2);
        Invoke("OnTriggerDao", 0.9f);
    }
    public void OnPlayMenace()
    {
        if (ItemCreater.Instance.isHang)
            PlayerModController.Instance.OnCancelHangSelf();
        int number = Random.Range(1, 39);
        OnCreateModVideoPlayer(new Vector3(0.5f,0.1f), Vector3.one, Vector3.zero, $"Question/{number}", 2);
        PlayerModController.Instance.OnTriggerModAnimator("menace");
    }
    void OnTriggerDao()
    {
        PlayerModController.Instance.OnTriggerModAnimator("dao");
    }
    string nullDUCK = "GreenScreen/Duck/Null";
    string getDUCK = "GreenScreen/Duck/Get";
    Queue<int> onCreate = new Queue<int>();
    public void OnCreateDuckVideoPlayer()
    {
        int index = Random.Range(0, 150);
        bool getduck = index >= 17;
        string title = getduck ? getDUCK : nullDUCK;
        int duckPath = 0;
        if (getduck)
        {
            if (index >= 17 && index < 21)
            {
                duckPath = 1;
            }
            else if (index >= 21 && index < 25)
            {
                duckPath = 1;
            }
            else if (index >= 25 && index < 29)
            {
                duckPath = 2;
            }
            else if (index >= 29 && index < 33)
            {
                duckPath = 11;
            }
            else if (index >= 33 && index < 37)
            {
                duckPath = 11;
            }
            else if (index >= 37 && index < 41)
            {
                duckPath = 15;
            }
            else if (index >= 41 && index < 45)
            {
                duckPath = 20;
            }
            else if (index >= 45 && index < 49)
            {
                duckPath = 40;
            }
            else if (index >= 49 && index < 53)
            {
                duckPath = 50;
            }
            else if (index >= 53 && index < 57)
            {
                duckPath = 80;
            }
            else if (index >= 57 && index < 61)
            {
                duckPath = 100;
            }
            else if (index >= 61 && index < 64)
            {
                duckPath = 200;
            }
            else if (index >= 65 && index < 67)
            {
                duckPath = 300;
            }
            else if (index >= 73 && index < 75)
            {
                duckPath = 500;
            }
            else if (index == 77)
            {
                duckPath = 1000;
            }
            else if (index == 79)
            {
                duckPath = 3000;
            }
            else if (index == 80)
            {
                duckPath = 5000;
            }
            else if (index == 81)
            {
                duckPath = 10000;
            }
            else
            {
                duckPath = 1;
            }
        }
        else
            duckPath = Random.Range(1, 24);
        string path = $"{title}/{duckPath}";
        OnCreateModVideoPlayer(Vector3.zero, Vector3.one, Vector3.zero, path, 2);
        duckPath = getduck ? duckPath : 0;
        onCreate.Enqueue(duckPath);
        Invoke("OnBeginCreateDuck",2);
    }

    void OnBeginCreateDuck( )
    {
        if (onCreate == null || onCreate.Count <= 0) return;
        int getduck = onCreate.Dequeue();
        if (getduck > 0)
            ItemCreater.Instance.OnCreateDuck(getduck);
    }

    public void OnCreateModVideoPlayer(Vector3 offset, Vector3 scale, Vector3 rotateA,string path, int type, string layer = "Video", bool snake = false)
    {
        GameObject vplayerObj = SimplePool.Spawn(ModVideoPlayer, transform.position, Quaternion.identity);
        ModVideoPlayer vplayer = vplayerObj.GetComponent<ModVideoPlayer>();
        vplayer.OnPlayVideo(offset, scale, rotateA, path, type, layer, snake);
        vplayerObj.transform.SetParent(videoParent);
        IsPlaying = true;
    }

    void OnVideoPlayEnd(object msg)
    {
        IsPlaying = false; 
        PlayerModController.Instance.OnTriggerModAnimator("endMenace");
    }
    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(Events.OnModVideoPlayEnd, OnVideoPlayEnd);
    }
}
