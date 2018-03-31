namespace Database
{
    public partial class Question
    {
        public bool Equals(Question question)
        {
            return Text == question.Text &&
                   AnswerPrefix == question.AnswerPrefix &&
                   AnswerSuffix == question.AnswerSuffix &&
                   QuestionType == question.QuestionType &&
                   Equals(AnswerSetValues, question.AnswerSetValues);
        }

        public Question GetCleanModel()
        {
            return new Question
            {
                ID = ID,
                Text = Text,
                AnswerPrefix = AnswerPrefix,
                AnswerSuffix = AnswerSuffix,
                Prebuilt = Prebuilt,
                Remarks = Remarks,
                Hash = Hash
            };
        }
    }
}