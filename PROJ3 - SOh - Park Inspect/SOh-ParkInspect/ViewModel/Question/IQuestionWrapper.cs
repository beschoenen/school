using System.Windows.Controls;
using Database;

namespace SOh_ParkInspect.ViewModel.Question
{
    public interface IQuestionWrapper
    {
        Database.Question Question { get; }

        string Answer { get; }

        UserControl Control { get; }

        Answer GetAnswer();

        bool IsValid { get; }
    }
}