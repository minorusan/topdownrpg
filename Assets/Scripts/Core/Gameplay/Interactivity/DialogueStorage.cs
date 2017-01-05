using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Core.Inventory;
using Utilities.UI;
using Core.Map;


namespace Core.Gameplay.Interactivity
{
	public struct DialogueStatement
	{
		public string SpeakerGOName;
		public string Sentence;
		public bool Thought;
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
				return (DialogueStatement[])_statements.Clone();
			}
		}

		public Dialogue(DialogueStatement[] statements, Action completion)
		{
			_statements = statements;
			_completion = completion;
		}
	}

	public class DialogueStorage
	{
		private static Dictionary<string, Dialogue> _dialogs = new Dictionary<string, Dialogue>();

		static DialogueStorage()
		{
			InitTestPolilogue();
			InitDialogues();
			InitTutorialDialogues();
		}
		// Update is called once per frame
		private static void InitDialogues()
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

			_dialogs.Add("dialogue.id.dumbfuck", new Dialogue(dialogueStatementsOne, 
			                                                  () => QuestController.StartQuest("quest.id.getmybear")));

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


			_dialogs.Add("dialogue.id.thanks", new Dialogue(dialogueStatementsTwo, 
			                                                () =>
			{
				var item = ItemsData.GetItemById("trapitem.id.basictrap");

				PlayerInventory.Instance.TryAddItemToInventory(item);
			}));
		}

		private static void InitTestPolilogue()
		{
			//TODO: Init from storage
			var dialogueStatementsOne = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Joe",
					Sentence = "So hi, this is test polilogue"
				},
				new DialogueStatement {
					SpeakerGOName = "Betsie",
					Sentence = "We are introducing some concept here."
				},
				new DialogueStatement {
					SpeakerGOName = "Crunchie",
					Sentence = "After it ends, doggy will be released.."
				},
				new DialogueStatement {
					SpeakerGOName = "Bob",
					Sentence = "And player gonna deal with it."
				},
				new DialogueStatement {
					SpeakerGOName = "Betsie",
					Sentence = "Releasing doggy in 3.."
				},
				new DialogueStatement {
					SpeakerGOName = "Bob",
					Sentence = "2.."
				},
				new DialogueStatement {
					SpeakerGOName = "Crunchie",
					Sentence = "1.."
				},
				new DialogueStatement {
					SpeakerGOName = "Bob",
					Sentence = "WHO LET THE DOGS OUT!"
				},
			};

			_dialogs.Add("dialogue.id.polilogue", new Dialogue(dialogueStatementsOne, 
			                                                   () =>
			{
				var testChain = GameObject.Find("testChain");
				GameObject.Find("testPolilogueTrigger").SetActive(false);
				testChain.GetComponent<NonWalkable>().ClearObject();
			}));
		}

		private static void InitTutorialDialogues()
		{
			//TODO: Init from storage
			var dialogueStatementsCraft = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "Hm..maybe I can learn a few tricks on it.",
					Thought = true 
				},
			};

			var dialogueStatementMonster = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "If I will be quiet, it wont spot me.",
					Thought = true
				},
			};

			var dialogueStatementLocked = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "Maybe I could cut it if I got nippers.",
					Thought = true
				},
			};
			var dialogueStatementHunger = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "Nice! I can eat it and move faster!",
					Thought = true
				},
			};

			var dialogueStatementStress = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "I can use it to calm myself and stop making so much noise.",
					Thought = true
				},
			};
			var dialogueStatementGeneric = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "Guess I can use it later..",
					Thought = true
				},
			};

			var dialogueStatementHide = new DialogueStatement [] { 
				new DialogueStatement {
					SpeakerGOName = "Player",
					Sentence = "I can hide here and monsters wont see me",
					Thought = true
				},
			};

			_dialogs.Add("dialogue.id.tutorial.craft", new Dialogue(dialogueStatementsCraft, 
			                                                        null));
			
			_dialogs.Add("dialogue.id.tutorial.monster", new Dialogue(dialogueStatementMonster, 
			                                                          null));
			_dialogs.Add("dialogue.id.tutorial.nippers", new Dialogue(dialogueStatementLocked, 
			                                                          null));
			_dialogs.Add("dialogue.id.tutorial.hunger", new Dialogue(dialogueStatementHunger, 
			                                                         null));
			_dialogs.Add("dialogue.id.tutorial.stress", new Dialogue(dialogueStatementStress, 
			                                                         null));
			_dialogs.Add("dialogue.id.tutorial.generic", new Dialogue(dialogueStatementGeneric, 
			                                                          null));
			_dialogs.Add("dialogue.id.tutorial.canhide", new Dialogue(dialogueStatementHide, 
			                                                          null));
		}


		public static Dialogue GetDialogueByID(string dialogueID)
		{
			return _dialogs[dialogueID];
		}
	}
}

