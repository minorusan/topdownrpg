using UnityEngine;
using System.Collections;

using Core.Map;
using Core.Characters.Player;

namespace Utils
{
    public class CameraViewChanger : MonoBehaviour
    {
        #region PRIVATE

        private MapController[] _maps;
        private MapController _currentMap;
        private Vector3 _destination = new Vector3(0f, 0f, -10f);
        private PlayerBehaviour _player;

        #endregion

        public float TransitionSpeed;

        private void Start()
        {
            _maps = FindObjectsOfType<MapController>();
            _player = FindObjectOfType<PlayerBehaviour>();
        }

        private void Update()
        {
            if (hasMapChanged())
            {
                _destination = new Vector3(_currentMap.CenterNode.Position.x, _currentMap.CenterNode.Position.y, -10);
            }
            transform.position = Vector3.MoveTowards(transform.position, _destination, TransitionSpeed * Time.deltaTime);
        }

        private bool hasMapChanged()
        {
            for (int i = 0; i < _maps.Length; i++)
            {
                var playerNode = _maps[i].GetNodeByPosition(_player.transform.position);
                if (playerNode != null && (_currentMap == null || _currentMap != _maps[i]))
                {
                    _currentMap = _maps[i];
                    return true;
                }
            }
            return false;
        }
    }
}


