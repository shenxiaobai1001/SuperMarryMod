using PlayerScripts;
using TMPro;
using UnityEngine;

namespace SystemScripts
{
    public class FollowPlayer : MonoBehaviour
    {
        public GameObject player;
        private float _furthestPlayerPosition;
        private float _currentPlayerPosition;

        private void Start()
        {
            if (player != null)
            {
                _currentPlayerPosition = player.transform.position.x;
            }
        }

        void LateUpdate()
        {
            if (player == null)
            {
                if (PlayerController.Instance != null)
                {
                    player = PlayerController.Instance.gameObject;
                }
            }
            if (player == null) return;
            if (player.transform.position.x <= 1.5f) return;
            if (GameStatusController.IsHidden&&!GameStatusController.HiddenMove) return;
            float y = GameStatusController.IsHidden ? 32 : 5;

            transform.position = Vector3.Lerp(
               transform.position,
               !GameStatusController.IsBossBattle
                ? new Vector3(player.transform.position.x, y, -10)
                : new Vector3(285, y, -10),
               10 * Time.deltaTime
           );
        }
    }
}