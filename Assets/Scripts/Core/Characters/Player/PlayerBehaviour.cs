﻿using UnityEngine;
using Core.Characters.Player.Demand;


namespace Core.Characters.Player
{
	[RequireComponent (typeof(StressAffector))]
	[RequireComponent (typeof(Animator))]
	public class PlayerBehaviour : MonoBehaviour
	{
		private Animator _animator;
		private StressAffector _stress;
		private bool _moves;

		[Header ("Movement")]
		public float MovementSpeed;
		public float Noise;

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
			Noise += (float)_stress.Stress / 100;
		}

		private void Start ()
		{
			_stress = GetComponent<StressAffector> ();
			_animator = GetComponent<Animator> ();
		}

		private void LateUpdate ()
		{
			Noise = Mathf.MoveTowards (Noise, 0, 0.2f);
		}
	}
}