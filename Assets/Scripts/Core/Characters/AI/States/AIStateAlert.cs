using UnityEngine;

using Core.Pathfinding;
using Core.Map;
using Core.Map.Pathfinding;
using Core.Characters.Player;
using UnityEngine.UI;


namespace Core.Characters.AI
{
	public class AIStateAlert:AIStateBase
	{
		private float _timeIdle = 1f;
		private float _searchDistance;
		private float _previousMoveSpeed;
		private float _alertTime;
		private float _timeInState;
		private Core.Characters.Player.PlayerBehaviour _player;

		public AIStateAlert (ArtificialIntelligence brains, float searchDistance, float alertTime) : base (brains)
		{
			_searchDistance = searchDistance;
			_alertTime = alertTime;
			State = EAIState.Alert;
			_timeInState = alertTime;
			_player = GameObject.FindObjectOfType<Core.Characters.Player.PlayerBehaviour> ();
		}

		public override void OnEnter ()
		{
			base.OnEnter ();
			_previousMoveSpeed = _masterBrain.MovableObject.MovementSpeed;
			_masterBrain.MovableObject.MovementSpeed *= 1.2f;
			_timeInState = _alertTime;
			_masterBrain.StatusText.text = "Wha..what was that?!";

			_masterBrain.MovableObject.DebugColor = Color.green;
		}

		public override void OnLeave ()
		{
			_masterBrain.MovableObject.MovementSpeed = _previousMoveSpeed;
		}

		public override void UpdateState ()
		{
			base.UpdateState ();
			if (Vector3.Distance (_masterBrain.transform.position, _player.transform.position) < _searchDistance)
			{
				_currentCondition = AIStateCondition.Done;
				_pendingState = EAIState.Attack;
			}

			if (_timeInState <= 0)
			{
				_currentCondition = AIStateCondition.Done;
				_pendingState = EAIState.Wandering;
			}

			if (_masterBrain.MovableObject.ReachedDestination)
			{
				FindNewpath ();
			}
			_timeInState -= Time.deltaTime;
		}

		private void FindNewpath ()
		{
			var possibleLocations = _map.GetNeighbours (_masterBrain.MovableObject.CurrentNode);
			if (possibleLocations.Length > 1)
			{
				var destination = possibleLocations [Random.Range (0, possibleLocations.Length - 1)];
				_masterBrain.MovableObject.BeginMovementByPath (Pathfinder.FindPathToDestination (
					_map,
					_masterBrain.MovableObject.CurrentNode.GridPosition,
					destination.GridPosition));
			}
		}
	}
}

