using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Core.Characters.AI;
using Core.Characters.Player;


namespace Core.Interactivity.AI
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class GuardBrains:ArtificialIntelligence
	{
		public float SearchDistance = 6f;
		public float AlertTime = 5f;
		public Image SuspentionBar;
		public Transform WanderingPointsRoot;

		private SpriteRenderer _renderer;

		public SpriteRenderer Renderer
		{
			get
			{
				if (_renderer == null)
				{
					_renderer = GetComponent<SpriteRenderer> ();
				}
				return _renderer;
			}
		}

		private void OnDrawGizmos ()
		{
			var color = Color.white;
			var radius = SearchDistance;
			#if !UNITY_EDITOR
            if (_currentState != null && _currentState.State == EAIState.Wandering)
            {
            color = Color.green;
            }
            else
            {
            color = Color.yellow;
            radius = SearchDistance * 2f;
            }
			#endif


			Gizmos.color = color;
			Gizmos.DrawWireSphere (transform.position, radius);
		}

		#region ArtificialIntelligence

		protected override void InitStates ()
		{
			_availiableStates.Add (EAIState.Wandering, new AIStateWandering (this, SearchDistance, WanderingPointsRoot, SuspentionBar));
			_availiableStates.Add (EAIState.Alert, new AIStateAlert (this, SearchDistance * 2, AlertTime));
			_availiableStates.Add (EAIState.Attack, new AIStateAttack (this));
			BaseState = EAIState.Wandering;
		}

		protected override void Start ()
		{
			base.Start ();
			_renderer = GetComponent <SpriteRenderer> ();
		}

		protected override void Update ()
		{
			base.Update ();
			if (transform.position.y > PlayerBehaviour.CurrentPlayer.transform.position.y)
			{
				_renderer.sortingOrder = PlayerBehaviour.CurrentPlayer.Renderer.sortingOrder - 1;
			}
			else
			{
				_renderer.sortingOrder = PlayerBehaviour.CurrentPlayer.Renderer.sortingOrder + 1;
			}
		}

		#endregion
	}
}

