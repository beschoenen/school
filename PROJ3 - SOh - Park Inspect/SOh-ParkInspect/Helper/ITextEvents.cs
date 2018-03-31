using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SOh_ParkInspect.Helper
{
    public class ITextEvents : IPdfPageEvent
    {
        private readonly Image _logo;
        private readonly Image _logolarge;

        public ITextEvents()
        {
            _logo = Image.GetInstance(@"..\..\\Asset\logo.png");
            _logolarge = Image.GetInstance(@"..\..\\Asset\ParkInspectLogoNoBackground.png");
            _logolarge.Alignment = Element.ALIGN_CENTER;
            _logo.ScalePercent(24f);
        }

        public void OnOpenDocument(PdfWriter writer, Document document)
        {
            if (document.PageNumber == 1)
            {
                document.Add(_logolarge);
            }
        }

        public void OnStartPage(PdfWriter writer, Document document)
        {
            if (document.PageNumber != 1)
            {
                document.Add(_logo);
            }
        }

        public void OnEndPage(PdfWriter writer, Document document)
        {
        }

        public void OnCloseDocument(PdfWriter writer, Document document)
        {
        }

        public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
        {
        }

        public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
        }

        public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
        {
        }

        public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
        }

        public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title)
        {
        }

        public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
        }

        public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
        {
        }
    }
}