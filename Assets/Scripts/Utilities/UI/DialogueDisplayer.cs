using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Gameplay.Interactivity;


namespace Core.Utilities.UI
{
	public class DialogueDisplayer : MonoBehaviour
	{
		private enum EDialogueDisplayerState
		{
			Idle,
			Showing
		}

		private static DialogueDisplayer _current;
		private Dialogue _dialogue;
		private int _currentMessage;
		private EDialogueDisplayerState _state;

		public DialogueBubble Bubble;

		// Use this for initialization
		private void Start()
		{
			_current = FindObjectOfType<DialogueDisplayer>();
		}

		// Update is called once per frame
		private void Update()
		{
			if(_state == EDialogueDisplayerState.Showing)
			{
				if(_currentMessage >= _dialogue.Statements.Length)
				{
					_state = EDialogueDisplayerState.Idle;
					Bubble.BubbleCompleted += EndDialog;
				}

				if(Bubble.Ready)
				{
					Bubble.ShowMessage(_dialogue.Statements[_currentMessage].Sentence,
					                    _dialogue.Statements[_currentMessage].SpeakerGOName,
					                    _dialogue.Statements[_currentMessage].Thought);
					_currentMessage++;
				}
			}
		}

		private void EndDialog()
		{
			Bubble.BubbleCompleted -= EndDialog;
			if(_dialogue.Completion != null)
			{
				_dialogue.Completion();
			}
			_dialogue = null;
		}

		public static void ShowDialogue(Dialogue dialogue)
		{
			if(dialogue != _current._dialogue)
			{
				_current._currentMessage = 0;
				_current._dialogue = dialogue;
				_current._state = EDialogueDisplayerState.Showing;
			}
		}
	}
}

