using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using SystemScripts;
using UnityEngine;

public class PlayerHitCheck : MonoBehaviour
{
    public PlayerController controller;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBody"))
        {
            if (!GameStatusController.IsBigPlayer)
            {
                // StartCoroutine(Die(other.gameObject));
                if (!controller.isInvulnerable)
                {
                    Sound.PlaySound("smb_mariodie");
                    GameStatusController.IsDead = true;
                    GameStatusController.Live -= 1;
                    controller._playerRb.isKinematic = true;
                    controller. _playerRb.velocity = Vector2.zero;
                }
                else
                {
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), controller.smallPlayerCollider.GetComponent<Collider2D>());
                }
            }
            else if (GameStatusController.IsBigPlayer)
            {
                GameStatusController.IsBigPlayer = false;
                GameStatusController.IsFirePlayer = false;
                GameStatusController.PlayerTag = "Player";
                gameObject.tag = GameStatusController.PlayerTag;
                controller.ChangeAnim();
                controller.isInvulnerable = true;
            }
            return;
        }
    
    }
}
