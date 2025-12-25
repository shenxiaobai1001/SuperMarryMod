using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetManager : Singleton<NetManager>
{
    // 消息队列容器
    private Queue<string> _messageQueue = new Queue<string>();

    public void OnDispseMsg(DataInfo dataInfo)
    {
        if (dataInfo == null)
        {
            PFunc.Log("消息空");
            return;
        }

        switch (dataInfo.call)
        {
            case "乌龟一只":
                MonsterCreater.Instance.OnCreateTortoise(1);
                break;
            case "乌龟十只":
                MonsterCreater.Instance.OnCreateTortoise(10);
                break;
            case "乌龟一百只":
                MonsterCreater.Instance.OnCreateTortoise(100);
                break;
            case "蘑菇一只":
                MonsterCreater.Instance.OnCreateMushroom(1);
                break;
            case "蘑菇十只":
                MonsterCreater.Instance.OnCreateMushroom(10);
                break;
            case "蘑菇一百只":
                MonsterCreater.Instance.OnCreateMushroom(100);
                break;
            case "飞龟一只":
                MonsterCreater.Instance.OnCreateFlyKoopa(1);
                break;
            case "飞龟十只":
                MonsterCreater.Instance.OnCreateFlyKoopa(10);
                break;
            case "飞龟一百只":
                MonsterCreater.Instance.OnCreateFlyKoopa(100);
                break;
            case "飞鱼一只":
                MonsterCreater.Instance.OnCreateFlyFish(1);
                break;
            case "飞鱼十只":
                MonsterCreater.Instance.OnCreateFlyFish(10);
                break;
            case "飞鱼一百只":
                MonsterCreater.Instance.OnCreateFlyFish(100);
                break;
            case "甲壳虫一只":
                MonsterCreater.Instance.OnCreateBeatles(1);
                break;
            case "甲壳虫十只":
                MonsterCreater.Instance.OnCreateBeatles(10);
                break;
            case "甲壳虫一百只":
                MonsterCreater.Instance.OnCreateBeatles(100);
                break;
        }
    }
}

[Serializable]
public class DataInfo
{
    public string user;       // 用户名字段
    public string userAvatar; // 用户头像URL
    public string call;       // 功能
    public int count;         // 数量
    public int time;          // 功能触发时间
    public string enalbe;
}