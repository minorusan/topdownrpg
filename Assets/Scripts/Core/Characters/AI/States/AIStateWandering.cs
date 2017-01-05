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
		private float _searchDistance;
		private float _suspention;
		private Image _suspentionBar;
		private Core.Characters.Player.PlayerBehaviour _player;
		private NoiseEffect _effect;
		private AudioClip _whispering;
		private AudioClip _bellCreepy;
		private SequentialMovement _movementController;


		public AIStateWandering(ArtificialIntelligence brains, float searchDistance, Transform pathRoot, Image suspentionBar) : base(brains)
		{
			_searchDistance = searchDistance;
			State = EAIState.Wandering;

			_movementController = new SequentialMovement(pathRoot.GetComponentsInChildren <Transform>(),
			                                             _masterBrain.MovableObject,
			                                             true);
			_bellCreepy = Resources.Load <AudioClip>("Sounds/bellCreepy");
			_whispering = Resources.Load <AudioClip>("Sounds/whisper");
			_effect = GameObject.FindObjectOfType<NoiseEffect>();
			_player = GameObject.FindObjectOfType<PlayerBehaviour>();
			_suspentionBar = suspentionBar;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if(_player != null && _player.isActiveAndEnabled)
			{
				var playerNode = _masterBrain.MovableObject.Map.GetNodeByPosition(_player.transform.position);
				_suspention = _suspention > 0f && playerNode != null ? 0.9f : 0f;
				if(_effect != null)
				{
					_effect.ChangeOpacity(_suspention);
				}

				_masterBrain.StatusText.text = "Set a trap on my path, please. I wonna die ^^";
				_previousMoveSpeed = _masterBrain.MovableObject.MovementSpeed;
				_masterBrain.MovableObject.MovementSpeed *= 0.4f;

				_masterBrain.MovableObject.DebugColor = Color.green;
			}
		}

		public override void OnLeave()
		{
			AudioSource.PlayClipAtPoint(_bellCreepy, Vector3.zero, 0.5f);
			AudioSource.PlayClipAtPoint(_whispering, Vector3.zero, 0.5f);
			_masterBrain.MovableObject.MovementSpeed = _previousMoveSpeed;
		}

		public override void UpdateState()
		{
			base.UpdateState();
			CheckLeaveStateConditions();

			_movementController.UpdateMovement();
			_suspention -= _suspention > 0f ? 0.01f : 0f;
		}

		private void CheckNoise()
		{
			var noise = _player.Noise;

			_suspention += noise;
			_effect.ChangeOpacity(_suspention);
			_suspentionBar.fillAmount = _suspention;
		}

		private void CheckLeaveStateConditions()
		{
			var playerNode = _masterBrain.MovableObject.Map.GetNodeByPosition(_player.transform.position);
			if(Vector3.Distance(_masterBrain.transform.position, _player.transform.position) < _searchDistance
			   && playerNode != null && !PlayerQuirks.Hidden)
			{
				_currentCondition = AIStateCondition.Done;
				_pendingState = EAIState.Attack;
			}

			if(_suspention > 1f)
			{
				_currentCondition = AIStateCondition.Done;
				_pendingState = EAIState.Alert;
			}

			if(playerNode != null)
			{
				CheckNoise();
			}
			else
			{
				if(_suspention > 0f)
				{
					_effect.ChangeOpacity(0f);
				}
			}

		}
	}
}

