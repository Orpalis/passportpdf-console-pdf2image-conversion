using PassportPDF.Api;
using PassportPDF.Client;
using PassportPDF.Model;

namespace PdfConversion
{

    public class ConvertPDF
    {
        static async Task Main(string[] args)
        {
            GlobalConfiguration.ApiKey = "YOUR-PASSPORT-CODE";

            PassportManagerApi apiManager = new();
            PassportPDFPassport passportData = await apiManager.PassportManagerGetPassportInfoAsync(GlobalConfiguration.ApiKey);

            if (passportData == null)
            {
                throw new ApiException("The Passport number given is invalid, please set a valid passport number and try again.");
            }
            else if (passportData.IsActive is false)
            {
                throw new ApiException("The Passport number given is not active, please go to your PassportPDF dashboard and choose a plan.");
            }

            string uri = "https://passportpdfapi.com/test/invoice_with_barcode.pdf";

            DocumentApi documentApi = new();

            Console.WriteLine("Loading document into PassportPDF...");
            DocumentLoadResponse document = await documentApi.DocumentLoadFromURIAsync(new LoadDocumentFromURIParameters(uri));
            Console.WriteLine("Document loaded.");

            PDFApi pdfApi = new();

            Console.WriteLine("Convert and save document as a PNG...");

            try
            {
                string savePath = Path.Join(Directory.GetCurrentDirectory(), "converted_doc.png");

                var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);

                await pdfApi.SaveAsPNGFileAsync(new PdfSaveAsPNGParameters(document.FileId) { PageRange = "1" }, fileStream);

                Console.WriteLine("PDF file converted and saved as JPEG successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not save stream to JPEG file! Error : {0}", ex);
            }

            Console.WriteLine("Convert and save document as a JPEG...");

            try
            {
                string savePath = Path.Join(Directory.GetCurrentDirectory(), "converted_doc.jpeg");

                var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);

                await pdfApi.SaveAsJPEGFileAsync(new PdfSaveAsJPEGParameters(document.FileId) { PageRange = "1"}, fileStream);

                Console.WriteLine("PDF file converted and saved as JPEG successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not save stream to JPEG file! Error : {0}", ex);
            }

            Console.WriteLine("Convert and save document as a TIFF...");

            try
            {
                string savePath = Path.Join(Directory.GetCurrentDirectory(), "converted_doc.tiff");

                var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);

                await pdfApi.SaveAsTIFFFileAsync(new PdfSaveAsTIFFParameters(document.FileId) { PageRange = "1" }, fileStream);

                Console.WriteLine("PDF file converted and saved as TIFF successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not save stream to TIFF file! Error : {0}", ex);
            }
        }
    }
}


