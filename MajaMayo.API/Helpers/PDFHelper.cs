using System;
using System.Text;
using OpenHtmlToPdf;

namespace MajaMayo.API.Helpers
{
    public static class PDFHelper
    {
        public static byte[] GeneratePdfFromHtml(byte[] html)
        {
            try
            {
                string htmlContent = Encoding.UTF8.GetString(html);
                // Use OpenHtmlToPdf to render HTML to PDF
                var pdfDocument = Pdf
                                   .From(htmlContent)
                                  .OfSize(PaperSize.A4) // Set page size (A4 by default)
                                  .WithMargins(0.Centimeters())
                                  .Landscape()
                                  .Content();

                return pdfDocument;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while generating the PDF.", ex);
            }
        }
    }
}
