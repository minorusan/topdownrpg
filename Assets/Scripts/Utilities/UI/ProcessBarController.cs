﻿using UnityEngine;
using System;
using UnityEngine.UI;
using CnControls;


namespace Core.Utilities
{
	[RequireComponent (typeof(Image))]
	public class ProcessBarController : MonoBehaviour
	{
		#region Private

		private enum EProcessControllerState
		{
			Idle,
			Process
		}

		private static ProcessBarController _instance;
		private Image _progressBar;
		private float _timePassed;
		private float _requiredTime;
		private Action _completion;
		private EProcessControllerState _currentState = EProcessControllerState.Idle;

		#endregion

		public Image ProcessIconImage;

		#region Monobehaviour

		private void Start ()
		{
			_progressBar = GetComponent <Image> ();
			_instance = this;
			gameObject.SetActive (false);
		}

		private void OnDestroy ()
		{
			_instance = null;
		}

		private void Update ()
		{
			switch (_currentState)
			{
			case EProcessControllerState.Process:
				{
					UpdateProcess ();
					break;
				}
			}
		}

		#endregion

		public static void StartProcessWithCompletion (float time, Sprite icon, Action completion, Color color)
		{
			if (_instance != null)
			{
				_instance.gameObject.SetActive (true);
				_instance.InitProcessWithCompletion (time, icon, completion, color);
			}
			else
			{
				Debug.LogError ("ProcessBarController::Cannot find instance.");
			}
		}

		private void InitProcessWithCompletion (float time, Sprite icon, Action completion, Color color)
		{
			_requiredTime = time;
			_timePassed = 0f;
			_progressBar.fillAmount = 0f;
			_progressBar.color = color;
			_completion = completion;
			_currentState = EProcessControllerState.Process;
			ProcessIconImage.sprite = icon;
		}

		private void UpdateProcess ()
		{
			if (CnInputManager.GetAxis ("Vertical") != 0f || CnInputManager.GetAxis ("Horizontal") != 0f)
			{
				_currentState = EProcessControllerState.Idle;
				gameObject.SetActive (false);
			}

			if (_timePassed < _requiredTime)
			{
				_timePassed += Time.deltaTime;
				_progressBar.fillAmount = _timePassed / _requiredTime;
			}
			else
			{
				_currentState = EProcessControllerState.Idle;
				gameObject.SetActive (false);
				_completion ();
			}
		}
	}
}

