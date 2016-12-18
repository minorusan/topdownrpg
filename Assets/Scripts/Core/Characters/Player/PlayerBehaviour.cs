using UnityEngine;
using Core.Characters.Player.Demand;
using Core.Inventory;


namespace Core.Characters.Player
{
	[RequireComponent (typeof(StressAffector))]
	[RequireComponent (typeof(Animator))]
	public class PlayerBehaviour : MonoBehaviour
	{
		public static string kPlayerTag = "Player";
		public static float BaseMovementSpeed = 10f;
		public AudioClip StepSound;

		private Animator _animator;
		private static PlayerBehaviour _player;
		private StressAffector _stress;
		private bool _moves;

		[Header ("Movement")]
		public float MovementSpeed;
		public float Noise;

		public static PlayerBehaviour CurrentPlayer
		{
			get
			{
				if (_player == null)
				{
					_player = FindObjectOfType <PlayerBehaviour> ();
				}
				return _player;
			}
		}

		public bool Moves
		{
			get
			{
				return _moves;
			}
			set
			{
				if (_moves != value)
				{
					_moves = value;
					_animator.SetBool ("Walk", _moves);
				}
			}
		}

		public void Step ()
		{
			Noise += (float)_stress.DemandState / 100;
			AudioSource.PlayClipAtPoint (StepSound, transform.position, Noise);
		}

		private void Start ()
		{
			_stress = GetComponent<StressAffector> ();
			_animator = GetComponent<Animator> ();
			BaseMovementSpeed = MovementSpeed;
		}

		private void LateUpdate ()
		{
			Noise = Mathf.MoveTowards (Noise, 0, 0.2f);
		}

		public static void SetPlayerPosition (Vector3 position)
		{
			var player = FindObjectOfType <PlayerBehaviour> ();
			player.transform.position = position;
		}
	}
}