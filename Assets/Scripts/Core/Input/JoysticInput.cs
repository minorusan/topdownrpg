using UnityEngine;
using System.Collections;
using CnControls;
using Core.Characters.Player;

namespace Core.Input
{
    [RequireComponent(typeof(PlayerBehaviour))]
    public class JoysticInput : MonoBehaviour
    {
        private PlayerBehaviour _player;

        private void Start()
        {
            _player = GetComponent<PlayerBehaviour>();
        }

        private void FixedUpdate()
        {
            var cnInputHorizontal = CnInputManager.GetAxis("Horizontal");
            var cnInputVertical = CnInputManager.GetAxis("Vertical");

            if (cnInputVertical != 0f || cnInputVertical != 0f)
            {
                var destination = new Vector3(_player.transform.position.x + cnInputHorizontal,
                                      _player.transform.position.y + cnInputVertical,
                                      0f);

                _player.transform.position = Vector3.MoveTowards(_player.transform.position, destination, _player.MovementSpeed * Time.deltaTime);
                _player.Moves = true;
            }
            else
            {
                _player.Moves = false;
            }
        }
    }
}

