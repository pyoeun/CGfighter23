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
            //�갡 ������ �� ������ �ʰ� �˾Ƽ� ���� �� �����ض�
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
            yield return new DialogueTalkLine("A", "�ȳ��ϼ���.", "a_standing");
            yield return new DialogueTalkLine("B", "��, �ȳ��ϼ���, A. ���� ��ħ�̿���.", "b_standing");

            if(DateTime.Now.Hour > 12)
                yield return new DialogueTalkLine("A", $"���� �Ҹ�����? ������ {DateTime.Now.Hour} �� �ε�.", "a_standing");
            else
                yield return new DialogueTalkLine("A", $"�׷��Կ�. ������ {DateTime.Now.Hour} �� ����.", "a_standing");

            yield return new DialogueTalkLine("B", "������. �������� �� �Ͻ� �ǰ���?", "b_standing");

            yield return new DialogueSelectLine("B", "b���� ������ �Ѵٰ� ���ұ�?", "b_standing", new string[]
            {
                "�� �ſ���.",
                "������ �� �ſ���.",
                "�𸣰ھ��."
            });

            yield return new DialogueTalkLine("B", "�׷�����. �� �˰ڽ��ϴ�.", "b_standing");

            IntInput input = new IntInput();

            yield return new DialogueInputHandleLine(input);
            if(input.Value == 0)
            {
                yield return new DialogueTalkLine("B", "���� �� ��������.", "b_standing");
            }
            if (input.Value == 1)
            {
                yield return new DialogueTalkLine("B", "������ �����ϼ���.", "b_standing");
            }
            if (input.Value == 2)
            {
                yield return new DialogueTalkLine("B", "����ȹ�� ���� ��ȹ����.", "b_standing");
            }

            yield return new DialogueTalkLine("B", "���� �̸� �����Կ�.", "b_standing");
            yield return new DialogueTalkLine("A", "�ȳ��� ������.", "a_standing");
        }
    }

}