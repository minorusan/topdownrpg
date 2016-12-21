using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Core.Characters.Player;


namespace Core.Input
{
	public class SwipeInput : MonoBehaviour
	{
		#region PRIVATE

		private Vector3 screenPoint;
		private Vector3 initialPosition;
     
		private Vector3 direction;
		private Rigidbody2D targetRB;
		private PlayerBehaviour _player;

		#endregion

		public float SpeedLimit;

		void Start ()
		{
			_player = FindObjectOfType<PlayerBehaviour> ();
			targetRB = _player.GetComponent<Rigidbody2D> ();
		}

		private void FixedUpdate ()
		{
			targetRB.velocity = Vector2.ClampMagnitude (targetRB.velocity, SpeedLimit);
			if (!float.IsNaN (direction.x) || !float.IsNaN (direction.y))
			{
				targetRB.AddForce (direction * _player.MovementSpeed, ForceMode2D.Impulse);
				_player.Moves = true;
			}

			if (targetRB.velocity == Vector2.zero)
			{
				_player.Moves = false;
			}
		}

		private void OnMouseDrag ()
		{
			/*targetRB.velocity = Vector2.zero;
			_player.Moves = false;
			var cursorPoint = new Vector3 (UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, 0);
			//var cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + offset;
			var heading = cursorPosition - initialPosition;
			direction = heading / heading.magnitude;    
			direction.x = Mathf.Clamp (direction.x, -1f, 1f);
			direction.y = Mathf.Clamp (direction.y, -1f, 1f);

			initialPosition = cursorPosition;*/
		}
	}
}

