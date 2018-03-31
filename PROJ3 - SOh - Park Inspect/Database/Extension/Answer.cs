using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Database
{
    public partial class Answer
    {
        public void SetText(DateTime value)
        {
            Text = Convert.ToString(value);
        }

        public void SetText(TimeSpan value)
        {
            Text = value.ToString();
        }

        public void SetText(decimal value)
        {
            Text = value.ToString();
        }

        public void SetText(double value)
        {
            Text = value.ToString();
        }

        public void SetText(string value)
        {
            Text = value;
        }

        public void SetText(int value)
        {
            Text = value.ToString();
        }

        public void SetText(string[] values)
        {
            Text = JsonConvert.SerializeObject(values);
        }

        public void SetText(IEnumerable<AnswerSetValue> answerSetValues)
        {
            SetText(answerSetValues.Select(e => e.Value).ToArray());
        }

        public T GetText<T>()
        {
            if (new[] { typeof(string[]), typeof(IEnumerable<string>), typeof(List<string>) }.Contains(typeof(T)))
            {
                return (T) (object) JArray.Parse(Text).Select(e => (string) e).ToArray();
            }

            if (typeof(T) == typeof(TimeSpan))
            {
                return (T) (object) TimeSpan.Parse(Text);
            }

            return (T) Convert.ChangeType(Text, typeof(T));
        }

        public Answer GetCleanModel()
        {
            return new Answer
            {
                Employee = Employee,
                Inspection = Inspection,
                Photo = Photo,
                Question = Question,
                Text = Text
            };
        }
    }
}