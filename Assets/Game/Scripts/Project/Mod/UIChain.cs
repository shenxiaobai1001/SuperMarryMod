using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using SystemScripts;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIChain : MonoBehaviour
{
    public static UIChain Instance;
    public List<GameObject> gameObjects;
    public GameObject center;
    public Text tx_number;
    void Awake()
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
        center.SetActive(false);
    }

    public void OnStartMove()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].gameObject.SetActive(false);
        }
        center.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Config.chainCount <= 0) return;
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.K))
        {
            OnRande();
            Config.chainCount--;
            if (Config.chainCount <= 0) {
                ItemCreater.Instance.lockPlayer = false;
                center.SetActive(false);
                PlayerController.Instance.isHit = false;
                SimplePool.Despawn(ChainPlayer.Instance.gameObject);
                PlayerModController.Instance.OnSetPlayerIns(true);
                PlayerModController.Instance.OnChangeState(true);
            }
        }
        tx_number.text = $"{Config.chainCount}";
    }

    public void OnRande()
    {
        int value = UnityEngine.Random.Range(0, gameObjects.Count);
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].gameObject.SetActive(value == i);
        }
    }
}
