using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Core.Inventory;
using Utilities.UI;


namespace Core.Gameplay.Interactivity
{
	public struct DialogueStatement
	{
		public string SpeakerGOName;
		public string Sentence;
	}

	public class Dialogue
	{
		private DialogueStatement[] _statements;
		private Action _completion;

		public Action Completion
		{
			get
			{
				return _completion;
			}
		}

		public DialogueStatement[] Statements
		{
			get
			{
				return (DialogueStatement[])_statements.Clone ();
			}
		}

		public Dialogue (DialogueStatement[] statements, Action completion)
		{
			_statements = statements;
			_completion = completion;
		}
	}

	public class DialogueStorage
	{
		private static Dictionary<string, Dialogue> _dialogs = new Dictionary<string, Dialogue> ();

		static DialogueStorage ()
		{
			InitDialogues ();
		}
		// Update is called once per frame
		private static void InitDialogues ()
		{
			//TODO: Init from storage
			var dialogueStatementsOne = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "Привет. Как ты?"
				},
				new DialogueStatement {
					SpeakerGOName = "Dumbfuck",
					Sentence = "Плохо. Потерял своего мишку."
				},
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "Мишку?"
				},
				new DialogueStatement {
					SpeakerGOName = "Dumbfuck",
					Sentence = "Да. Плюшевого. Поможешь мне найти его?"
				},
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "А что мне за это будет?"
				},
				new DialogueStatement {
					SpeakerGOName = "Dumbfuck",
					Sentence = "Ну..я дам тебе ловушку."
				},
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "Ладно, попробую."
				},
			};

			_dialogs.Add ("dialogue.id.dumbfuck", new Dialogue (dialogueStatementsOne, 
			                                                    () => QuestController.StartQuest ("quest.id.getmybear")));

			var dialogueStatementsTwo = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Dumbfuck",
					Sentence = "Уууу! Ты нашла моего мишку!!"
				},
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "Ну да. Неплохо бы увидеть ловушку.."
				},
				new DialogueStatement {
					SpeakerGOName = "Dumbfuck",
					Sentence = "Да, конечно. Вот!"
				},
			};


			_dialogs.Add ("dialogue.id.thanks", new Dialogue (dialogueStatementsTwo, 
			                                                  () =>
			{
				var item = ItemsData.GetItemById ("trapitem.id.basictrap");
				FanfareMessage.ShowWithText ("Ловушка добавлена в инвентарь");
				PlayerInventory.Instance.TryAddItemToInventory (item);
			}));
		}

		public static Dialogue GetDialogueByID (string dialogueID)
		{
			return _dialogs [dialogueID];
		}
	}
}

