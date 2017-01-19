using System;
using System.Collections.Generic;
using UnityEngine;

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
            InitDialogues();
            InitTutorialDialogues();
        }
        // Update is called once per frame
        private static void InitDialogues()
        {
            InitTestPolilogue();
            InitKidDialogue();
            InitScholarDialogues();
        }

        private static void InitScholarDialogues()
        {
            var dialogueStatement = new[]
            {
                new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "So, it appears that I was wrong and there is something good even in you.."
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "Now, what is it all about?"
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "I was wondering, if I can get to that vault.."
                },
                 new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "And why on earth would you want that?"
                },
                 new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "Me and my friends are starving. I thought we can find something there."
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "*scratches his chin*"
                },
                 new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "Passage is locked..but I guess I can help you. I'll try to make a pick, but you figure out how to use it yourself."
                },
                   new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "Sure! What you want for that?"
                },
                     new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "You just..just bring me some nails, okay?"
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "Yeah! Okay!"
                },
            };

           
            var dialogueCompleted = new[]
            {
                new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "Aaand there. You. Go. Pile of nails."
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "Okaay, let's try do do something usefull.."
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "*crafting something with intense. Looks like he did it before many times*"
                },
                 new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "Here you are. But it can take loong time to open a lock with that thing. You better ask someone to teach you."
                },
                 new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "Thanks! By the how, how you did that one? Nails are pretty touch to change form.."
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "Ah, it's pretty easy I'll write it on a sheet of paper for ya."
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "*smiles thankfully*"
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Scholar",
                    Sentence = "Now, off you go! Good luck. So..where's that kid.."
                },
            };
            _dialogs.Add("dialogue.id.scholar", new Dialogue(dialogueStatement, () => QuestController.StartQuest("quest.id.lockpick")));
            _dialogs.Add("dialogue.id.scholardone", new Dialogue(dialogueCompleted, null));

            var dialogueStatementsOne = new[]
            {
                new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "*coughs*"
                },
                  new DialogueStatement
                {
                    SpeakerGOName = "ScholarMad",
                    Sentence = "What? Who are you?"
                },
                     new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "I was just wondering if.."
                },
                new DialogueStatement
                {
                    SpeakerGOName = "ScholarMad",
                    Sentence = "Well I don't care! You little brat only want to rob and cheat."
                },
                 new DialogueStatement
                {
                    SpeakerGOName = "ScholarMad",
                    Sentence = "Stay away from me and that boy over there.."
                },
                  new DialogueStatement
                {
                    SpeakerGOName = "ScholarMad",
                    Sentence = "*points north*"
                },
                new DialogueStatement
                {
                    SpeakerGOName = "ScholarMad",
                    Sentence = "..or I'll crush your filthy head. It would be a blessing for ya, thou.."
                },
                new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Thought = true,
                    Sentence = "Whoa..looks like he had some bad time over here lately.."
                }
        };



            _dialogs.Add("dialogue.id.madscholar", new Dialogue(dialogueStatementsOne,null));
        }

        private static void InitKidDialogue()
        {
            var dialogueStatementsBoy = new[] {
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "Hey there. What are you doing here?"
                },
                 new DialogueStatement {
                    SpeakerGOName = "Little",
                    Sentence = "Playing hide and seek with that fellow at the wall. See him?"
                },
                new DialogueStatement {
                    SpeakerGOName = "Little",
                    Sentence = "*points south*"
                },
                 new DialogueStatement {
                    SpeakerGOName = "Little",
                    Sentence = "He wont speek much.He lost someone now I'm his only friend."
                },
                 new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "I want to be friends with him too. Can you help me?"
                },
                  new DialogueStatement {
                    SpeakerGOName = "Little",
                    Sentence = "Mmaaaaybe..But whom I gonna play with then?"
                },
                   new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "I'll try to figure it out"
                }
            };

            var dialogueStatementBearQuest = new[]
            {
                new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "Here you are. Do you like it?"
                },
                 new DialogueStatement
                {
                    SpeakerGOName = "Little",
                    Sentence = "Hell yeah! Thanks!"
                },
                  new DialogueStatement
                {
                    SpeakerGOName = "Player",
                    Sentence = "Will you speak with your friend for me now?"
                },
                   new DialogueStatement
                {
                    SpeakerGOName = "Little",
                    Sentence = "Sure thing! You can count on me!"
                }
            };

            _dialogs.Add("dialogue.id.littleboy", new Dialogue(dialogueStatementsBoy,
                () => { QuestController.StartQuest("quest.id.findtoy"); }));
            _dialogs.Add("dialogue.id.littleboyquestdone", new Dialogue(dialogueStatementBearQuest,
                () =>
                {
                    var scholarmad = GameObject.Find("ScholarMad");
                    var scholar = GameObject.Find("Scholar");
                    scholar.transform.position = scholarmad.transform.position;
                    scholarmad.SetActive(false);
                }));

        }

        private static void InitTestPolilogue()
        {
            //TODO: Init from storage
            var dialogueStatementsOne = new[] {
                new DialogueStatement {
                    SpeakerGOName = "Joe",
                    Sentence = "Gosh, I'm so hungry.."
                },
                new DialogueStatement {
                    SpeakerGOName = "Joe",
                    Sentence = "Something to eat? Anyone?"
                },
                new DialogueStatement {
                    SpeakerGOName = "Tim",
                    Sentence = "Nope"
                },
                new DialogueStatement {
                    SpeakerGOName = "Genny",
                    Sentence = "David had something, but he wont share."
                },
                new DialogueStatement {
                    SpeakerGOName = "Genny",
                    Sentence = "'cause he is a.."
                },
                new DialogueStatement {
                    SpeakerGOName = "Tim",
                    Sentence = "Jerk! Haha."
                },
                new DialogueStatement {
                    SpeakerGOName = "Joe",
                    Sentence = "Does not help my hunger thou. Hey, maybe you got something?"
                },
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "Me?"
                },
                 new DialogueStatement {
                    SpeakerGOName = "Tim",
                    Sentence = "Yup. Where've you been lately? Maybe you found something?"
                },
                   new DialogueStatement {
                    SpeakerGOName = "Genny",
                    Sentence = "Ah, she wouldn't share. She's much like David.."
                },
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "No, I'm not!"
                },
                   new DialogueStatement {
                    SpeakerGOName = "Genny",
                    Sentence = "Yes you do, yes you do, yes you do ahahah.."
                },
                   new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "Imbicils.."
                },
                   new DialogueStatement {
                    SpeakerGOName = "Joe",
                    Sentence = "If only we could get to that vault over there.."
                },
                    new DialogueStatement {
                    SpeakerGOName = "Tim",
                    Sentence = "Yap, but entrance is locked.."
                },
                     new DialogueStatement {
                    SpeakerGOName = "Player",
                    Thought = true,
                    Sentence = "No such a lock, that does not have a key.."
                }
            };

            _dialogs.Add("dialogue.id.firstdialogue", new Dialogue(dialogueStatementsOne,
                                                               () =>
                                                               {
                                                                   QuestController.StartQuest("quest.id.findentrance");
                                                               }));
        }

        private static void InitTutorialDialogues()
        {
            //TODO: Init from storage
            var dialogueStatementsCraft = new[] {
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "Hm..maybe I can learn a few tricks on it.",
                    Thought = true
                }
            };

            var dialogueStatementMonster = new[] {
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "If I will be quiet, it wont spot me.",
                    Thought = true
                }
            };

            var dialogueStatementLocked = new[] {
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "Maybe I could cut it if I got nippers.",
                    Thought = true
                }
            };
            var dialogueStatementHunger = new[] {
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "Nice! I can eat it and move faster!",
                    Thought = true
                }
            };

            var dialogueStatementStress = new[] {
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "I can use it to calm myself and stop making so much noise.",
                    Thought = true
                }
            };
            var dialogueStatementGeneric = new[] {
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "Guess I can use it later..",
                    Thought = true
                }
            };

            var dialogueStatementHide = new[] {
                new DialogueStatement {
                    SpeakerGOName = "Player",
                    Sentence = "I can hide here and noone will see me",
                    Thought = true
                }
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

