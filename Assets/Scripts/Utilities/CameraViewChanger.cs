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
		private Vector3 _destination = new Vector3 (0f, 0f, -10f);
		private PlayerBehaviour _player;

		#endregion

		public float TransitionSpeed;
		public bool FollowPlayer;

		private void OnEnable ()
		{
			_currentMap = null;
			_maps = MapController.GetMapsOnScene ();
			_player = FindObjectOfType<PlayerBehaviour> ();
		}

		private void Update ()
		{
			if (FollowPlayer)
			{
				
				_destination = new Vector3 (_player.transform.position.x,
				                            _player.transform.position.y,
				                            -10);
			}
			else
			{
				if (HasMapChanged ())
				{
					_destination = new Vector3 (_currentMap.CenterNode.Position.x, _currentMap.CenterNode.Position.y, -10);
				}
			}

			transform.position = Vector3.MoveTowards (transform.position, _destination, TransitionSpeed * Time.deltaTime);
		}

		private bool HasMapChanged ()
		{
			for (int i = 0; i < _maps.Length; i++)
			{
				var playerNode = _maps [i].GetNodeByPosition (_player.transform.position);
				if (playerNode != null && (_currentMap == null || _currentMap != _maps [i]))
				{
					_currentMap = _maps [i];
					return true;
				}
			}
			return false;
		}
	}
}


