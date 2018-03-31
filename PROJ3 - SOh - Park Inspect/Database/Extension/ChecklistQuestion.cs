namespace Database
{
    public partial class ChecklistQuestion
    {
        public ChecklistQuestion GetCleanModel()
        {
            return new ChecklistQuestion
            {
                Question = new Question
                {
                    AnswerPrefix = Question.AnswerPrefix,
                    AnswerSuffix = Question.AnswerSuffix,
                    AnswerSetValues = Question.AnswerSetValues,
                    QuestionType = Question.QuestionType,
                    Remarks = Question.Remarks,
                    Text = Question.Text,
                    Prebuilt = false,
                    Hash = Question.Hash
                }
            };
        }
    }
}
