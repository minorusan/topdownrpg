﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace Core.Gameplay.Interactivity
{
	public class DialogueBubble : MonoBehaviour
	{
		private Dictionary<string, GameObject> _cachedSpeakers = new Dictionary<string, GameObject> ();
		private GameObject _currentSpeaker;

		public event Action BubbleCompleted;

		public Text Text;
		public float TextSpeed;

		public bool Ready
		{
			get;
			private set;
		}

		private void Start ()
		{
			Ready = true;
			gameObject.SetActive (false);
		}

		private void Update ()
		{
			if (!Ready)
			{
				transform.position = new Vector2 (_currentSpeaker.transform.position.x + 2f, 
				                                  _currentSpeaker.transform.position.y + 2f);
			}
		}

		public void ShowMessage (string message, string GOName)
		{
			Ready = false;
			_currentSpeaker = null;
			gameObject.SetActive (true);
			StartCoroutine (DisplayMessage (message));
		
			_cachedSpeakers.TryGetValue (GOName, out _currentSpeaker);
			if (_currentSpeaker == null)
			{
				_currentSpeaker = GameObject.Find (GOName);
				_cachedSpeakers.Add (GOName, _currentSpeaker);
			}
				
			transform.localScale = Vector2.one;
		}

		private IEnumerator DisplayMessage (string message)
		{
			for (int i = 0; i <= message.Length; i++)
			{
				Text.text = message.Substring (0, i);
				yield return new WaitForSeconds (TextSpeed);
			}
			Invoke ("ResetBubble", 2f);
		}

		private void ResetBubble ()
		{
			Ready = true;
			if (BubbleCompleted != null)
			{
				BubbleCompleted ();
			}
			gameObject.SetActive (false);
		}
	}
}
