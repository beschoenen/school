using System.IO;
using System.Linq;
using System.Windows.Forms;
using Database;
using iTextSharp.text.pdf;
using it = iTextSharp.text;

namespace SOh_ParkInspect.Helper
{
    public class PdfConverter
    {
        public bool IncludeAnswers;

        public bool Convert(Inspection inspection)
        {
            var sf = new SaveFileDialog
            {
                FileName = $"{inspection.Checklist.Name}.pdf",
                Filter = @"Pdf Files|*.pdf"
            };

            if (sf.ShowDialog() != DialogResult.OK) return false;

            // Now here's our save folder
            var savePath = sf.FileName + (sf.FileName.EndsWith(".pdf") ? "" : ".pdf");

            const it.Font.FontFamily font = it.Font.FontFamily.TIMES_ROMAN;

            var header = new it.Font(font, 22f);
            var title = new it.Font(font, 18f);
            var subtitle = new it.Font(font, 13f);
            var paragraph = new it.Font(font, 9f);

            var templateDocument = new it.Document(it.PageSize.A4);
            
            var pdfWriter = PdfWriter.GetInstance(templateDocument, new FileStream(savePath, FileMode.Create));
            pdfWriter.PageEvent = new ITextEvents();

            templateDocument.Open();

            var name = new it.Paragraph(inspection.Checklist.Name, header);
            var datetime = new it.Paragraph(inspection.Checklist.DateTimeCreated.ToString(), subtitle);

            name.Alignment = it.Element.ALIGN_CENTER;
            datetime.Alignment = it.Element.ALIGN_CENTER;

            templateDocument.Add(name);
            templateDocument.Add(datetime);
            templateDocument.Add(new it.Paragraph("Vragen:", header));
            //Load questions and convert to pdf format.

            //Assemble list of questions
            var questions = inspection.Answers.Select(a => a.Question).Distinct();

            foreach (var question in questions)
            {
                templateDocument.Add(new it.Paragraph(question.Text, title));
                templateDocument.Add(new it.Paragraph(question.QuestionType.Name, subtitle));

                if (!IncludeAnswers) continue;

                //Place answers in an unordered(bullet point) list below the question title
                var list = new it.List(it.List.UNORDERED);

                list.SetListSymbol("\u2022"); //Set type to bulletpoint 
                list.IndentationLeft = 30f; //Neatly indent it

                foreach (var answer in question.Answers)
                {
                    list.Add(new it.ListItem($"{question.AnswerPrefix} {answer.Text} {question.AnswerSuffix}", paragraph));
                }

                templateDocument.Add(list);
            }

            templateDocument.Close();

            return true;
        }
    }
}