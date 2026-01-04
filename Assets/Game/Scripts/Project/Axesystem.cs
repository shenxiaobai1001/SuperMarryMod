using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axesystem : MonoBehaviour
{
    public ChainBridge bridge;
    bool check = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.tag.Equals("Player")&& check)
        {
            check = false;
            bridge.OnMinBrige();
            PlayerController.Instance.isHit = true;
            OnEnterNextLevel();
        }
    }

    void OnEnterNextLevel()
    {
        StartCoroutine(NextLevel());
    }
    private static IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1);
        Config.passIndex++;
        string name = Config.passName[Config.passIndex];
        GameModController.Instance.OnLoadScene(name);
    }
}
