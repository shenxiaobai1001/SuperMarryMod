using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangSeng : MonoBehaviour
{
    public float moveSpeed = 3f;
    public bool isLeft;
    bool kickPlayer = true;
    Transform playerTarget;
    bool isMove = false;
    void Start()
    {
        playerTarget = PlayerController.Instance.transform;
    }

    void Update()
    {
        if (!isMove) return;
        if (kickPlayer)
        {
            ChasePlayer();
        }
    }

    // 外部方法：设置初始位置
    public void StartMove()
    {
        isMove = true;
        kickPlayer = true;
    }

    void ChasePlayer()
    {
        // 向玩家移动
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, playerTarget.position) < 1.3f)
        {
            kickPlayer = false;
            PlayerModMoveController.Instance.TriggerModMove(MoveType.Normal, new Vector3(1,0.5f), 20,0.5f,true,false,1);
            OnClose();
        }
    }

    void OnClose()
    {
        SimplePool.Despawn(gameObject);
        gameObject.SetActive(false);
    }

}
