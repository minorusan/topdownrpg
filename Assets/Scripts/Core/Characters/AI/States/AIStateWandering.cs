using UnityEngine;

using Core.Pathfinding;
using Core.Map;
using Core.Map.Pathfinding;
using Core.Characters.Player;
using UnityEngine.UI;
using Utils;


namespace Core.Characters.AI
{
	public class AIStateWandering:AIStateBase
	{
		private float _previousMoveSpeed;
		private float _timeIdle = 3f;
		private float _searchDistance;
		private float _suspention;
		private Image _suspentionBar;
		private Core.Characters.Player.PlayerBehaviour _player;
		private NoiseEffect _effect;
		private AudioClip _bellCreepy;


		public AIStateWandering (ArtificialIntelligence brains, float searchDistance, Image suspentionBar) : base (brains)
		{
			_searchDistance = searchDistance;
			State = EAIState.Wandering;
			_bellCreepy = Resources.Load <AudioClip> ("Sounds/bellCreepy");
			_effect = GameObject.FindObjectOfType<NoiseEffect> ();
			_player = GameObject.FindObjectOfType<PlayerBehaviour> ();
			_suspentionBar = suspentionBar;
		}

		public override void OnEnter ()
		{
			base.OnEnter ();
			_suspention = _suspention > 0f ? 0.9f : 0f;
			_masterBrain.StatusText.text = "ZZZZzz....";
			_previousMoveSpeed = _masterBrain.MovableObject.MovementSpeed;
			_masterBrain.MovableObject.MovementSpeed *= 0.4f;

			_masterBrain.MovableObject.DebugColor = Color.green;
		}

		public override void OnLeave ()
		{
			AudioSource.PlayClipAtPoint (_bellCreepy, Vector3.zero, 0.5f);
			_masterBrain.MovableObject.MovementSpeed = _previousMoveSpeed;
		}

		public override void UpdateState ()
		{
			base.UpdateState ();
			var playerNode = _masterBrain.MovableObject.Map.GetNodeByPosition (_player.transform.position);
			if (Vector3.Distance (_masterBrain.transform.position, _player.transform.position) < _searchDistance && playerNode != null)
			{
				_currentCondition = AIStateCondition.Done;
				_pendingState = EAIState.Attack;
			}

			if (_suspention > 1f)
			{
				_currentCondition = AIStateCondition.Done;
				_pendingState = EAIState.Alert;
			}

			if (_timeIdle <= 0)
			{
				FindNewpath ();
				_timeIdle = 3f;

			}
			else
			{
				CheckNoise ();
				_timeIdle -= Time.deltaTime;
				_suspention -= _suspention > 0f ? 0.01f : 0f;
			}
		}

		private void CheckNoise ()
		{
			var noise = _player.Noise;
			_suspention += noise;
			_effect.ChangeOpacity (_suspention);
			_suspentionBar.fillAmount = _suspention;
		}

		private void FindNewpath ()
		{
			var possibleLocations = _map.GetNeighbours (_masterBrain.MovableObject.CurrentNode);
			if (possibleLocations != null && possibleLocations.Length > 1)
			{
				var destination = possibleLocations [Random.Range (0, possibleLocations.Length - 1)];
				if (destination != null)
				{
					_masterBrain.MovableObject.BeginMovementByPath (Pathfinder.FindPathToDestination (
						_map,
						_masterBrain.MovableObject.CurrentNode.GridPosition,
						destination.GridPosition));
                    
				}
			}
		}
	}
}

