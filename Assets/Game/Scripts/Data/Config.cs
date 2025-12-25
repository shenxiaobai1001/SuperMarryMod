using System;
using System.Collections.Generic;

/// <summary>配置</summary>
public class Config
{
    public static int ClearType=1;
 
}

/// <summary>事件合集 </summary>
public enum Events
{
    None,
    OnCreateBomb,
    StandResbay,
    OnTCMove,
    OnQLMove,
    FlyShaTimeChange,
    FlyShaTimeOver,
    ShieldTimeChange,
    ShieldTimeOver,
    OnCreateTrunck,
    OnTrunckColse,
    OnGameWin,
}
