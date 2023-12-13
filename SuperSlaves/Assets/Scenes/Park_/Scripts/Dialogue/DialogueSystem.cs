using System;
using System.Collections.Generic;

namespace DialogueSystem
{ 
    public interface IDialogueLine
    {
        void OnExecute(DialogueMachine pMachine);
    }

    public class DialogueTalkLine : IDialogueLine
    {
        private String m_talker;
        private String m_line;
        private String m_illust;

        public DialogueTalkLine(String pTalker, String pLine, String pIllust)
        {
            m_talker = pTalker;
            m_line = pLine;
            m_illust = pIllust;
        }

        public void OnExecute(DialogueMachine pMachine)
        {
            pMachine.Output.WriteLine(m_line);
            pMachine.Output.WriteTalkerName(m_talker);
            pMachine.Output.WriteIllust(m_illust);

            pMachine.Output.DoPrint(pMachine.NextLine);
            //얘가 프린팅 다 끝나면 너가 알아서 다음 줄 실행해라
        }
    }

    public class DialogueSelectLine : IDialogueLine
    {
        private String m_talker;
        private String m_line;
        private String m_illust;
        private String[] m_selects;

        public DialogueSelectLine(String pTalker, String pLine, String pIllust, String[] pSelects)
        {
            m_talker = pTalker;
            m_line = pLine;
            m_illust = pIllust;
            m_selects = pSelects;
        }

        public void OnExecute(DialogueMachine pMachine)
        {
            pMachine.Output.WriteLine(m_line);
            pMachine.Output.WriteTalkerName(m_talker);
            pMachine.Output.WriteIllust(m_illust);
            pMachine.Output.WriteSelections(m_selects);

            pMachine.Output.DoPrint(pMachine.NextLine);
        }
    }

    public class IntInput
    {
        public Int32 Value { get; set; }
    }

    public class DialogueInputHandleLine : IDialogueLine
    {
        private IntInput m_input;

        public DialogueInputHandleLine(IntInput pInput)
        {
            m_input = pInput;
        }

        public void OnExecute(DialogueMachine pMachine)
        {
            m_input.Value = pMachine.Input.ReadSelection();
            pMachine.NextLine();
        }
    }







    public interface IDialogueOutput
    {
        void WriteLine(String pLine);
        void WriteTalkerName(String pTalkerName);
        void WriteSelections(String[] pSelections);
        void WriteIllust(String pIllust);

        void BeginPrint();
        void DoPrint(Action pNext);
        void EndPrint();
    }

    public interface IDialogueInput
    {
        Int32 ReadSelection();
    }


    public class DialogueMachine
    {
        private IDialogueInput m_input;
        private IDialogueOutput m_output;

        public IDialogueInput Input => m_input;
        public IDialogueOutput Output => m_output;

        public void BindInput(IDialogueInput pInput)
        {
            m_input = pInput;
        }
        public void BindOutput(IDialogueOutput pOutput)
        {
            m_output = pOutput;
        }

        private IEnumerator<IDialogueLine> m_enumerator;
        public void RunDialog(IEnumerator<IDialogueLine> pLineEnumerator)
        {
            m_enumerator = pLineEnumerator;
            m_output.BeginPrint();
            NextLine();
        }

        public void NextLine()
        {
            if (m_enumerator.MoveNext())
            {
                m_enumerator.Current.OnExecute(this);
            }
            else
            {
                m_output.EndPrint();
            }
        }

        public static IEnumerator<IDialogueLine> ExampleDialogue()
        {
            yield return new DialogueTalkLine("A", "안녕하세요.", "a_standing");
            yield return new DialogueTalkLine("B", "오, 안녕하세요, A. 좋은 아침이에요.", "b_standing");

            if(DateTime.Now.Hour > 12)
                yield return new DialogueTalkLine("A", $"무슨 소리에요? 지금은 {DateTime.Now.Hour} 시 인데.", "a_standing");
            else
                yield return new DialogueTalkLine("A", $"그러게요. 상쾌한 {DateTime.Now.Hour} 시 에요.", "a_standing");

            yield return new DialogueTalkLine("B", "하하하. 이제부턴 뭘 하실 건가요?", "b_standing");

            yield return new DialogueSelectLine("B", "b에게 무엇을 한다고 답할까?", "b_standing", new string[]
            {
                "잘 거에요.",
                "게임을 할 거에요.",
                "모르겠어요."
            });

            yield return new DialogueTalkLine("B", "그렇구나. 잘 알겠습니다.", "b_standing");

            IntInput input = new IntInput();

            yield return new DialogueInputHandleLine(input);
            if(input.Value == 0)
            {
                yield return new DialogueTalkLine("B", "좋은 밤 보내세요.", "b_standing");
            }
            if (input.Value == 1)
            {
                yield return new DialogueTalkLine("B", "열심히 게임하세요.", "b_standing");
            }
            if (input.Value == 2)
            {
                yield return new DialogueTalkLine("B", "무계획도 좋은 계획이죠.", "b_standing");
            }

            yield return new DialogueTalkLine("B", "저는 이만 가볼게요.", "b_standing");
            yield return new DialogueTalkLine("A", "안녕히 가세요.", "a_standing");
        }
    }

}