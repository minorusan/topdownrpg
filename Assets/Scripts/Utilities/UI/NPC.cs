using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters.Player;


namespace Utilities.UI
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class NPC : MonoBehaviour
	{
		private SpriteRenderer _renderer;
	    private int _inittialSortingOrder;
		// Use this for initialization
		void Start()
		{
			_renderer = GetComponent<SpriteRenderer>();
            _inittialSortingOrder = _renderer.sortingOrder;

        }

		// Update is called once per frame
		void Update()
		{
			if(PlayerBehaviour.CurrentPlayer.isActiveAndEnabled && transform.position.y > PlayerBehaviour.CurrentPlayer.transform.position.y)
			{
				_renderer.sortingOrder = _inittialSortingOrder;
			}
			else
			{
				_renderer.sortingOrder = PlayerBehaviour.CurrentPlayer.Renderer.sortingOrder + 1;
			}
		}
	}

}
