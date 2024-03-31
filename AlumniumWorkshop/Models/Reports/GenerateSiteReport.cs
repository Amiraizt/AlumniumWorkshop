using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;

namespace AlumniumWorkshop.Models.Reports
{
    public class GenerateSiteReport
    {

        string fontpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\Fonts\\Arial.ttf";

        int _totalColumn = 8;
        Document _doc = new Document();
        Font _fontStyle;
        PdfPTable _pdfTable1 = new PdfPTable(6);
        PdfPTable _itemsTable = new PdfPTable(4);
        PdfPTable _aluminumsTable = new PdfPTable(3);
        PdfPTable _totalsTable = new PdfPTable(2)
        {
            WidthPercentage = 100f

        };

        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();
        SiteReportModel _model = new SiteReportModel();

        public byte[] PrepareReport(SiteReportModel model)
        {
            _model = model;

            _doc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _doc.SetPageSize(PageSize.A4);
            _doc.SetMargins(20f, 20f, 20f, 20f);
            _pdfTable1.WidthPercentage = 100;
            _pdfTable1.HorizontalAlignment = Element.ALIGN_LEFT;

            _fontStyle = FontFactory.GetFont("Arial", 8f, 2);
            PdfWriter.GetInstance(_doc, _memoryStream);
            _doc.Open();
            _pdfTable1.SetWidths(new float[] { 50f, 50f, 50f, 50f, 50f, 50f});
            _itemsTable.SetWidths(new float[] { 50f, 50f, 50f, 50f });
            _aluminumsTable.SetWidths(new float[] { 50f, 50f, 50f});
            _totalsTable.SetWidths(new float[] { 100f, 300f });

            ReportHeader();
            ReportBody();
            ReportFooter();
            _pdfTable1.HeaderRows = 1;

            _pdfTable1.SpacingBefore = 100;


            ////set logo

            //string imageURL = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") + "/images/logo.jpeg";

            //Image png = Image.GetInstance(imageURL);

            //Resize image 

            //png.ScaleToFit(120f, 100f);
            //png.SpacingAfter = 0;
            //png.SpacingBefore = 100;

            //png.Alignment = Element.ALIGN_RIGHT;

            //_doc.Add(png);




            _doc.Add(_pdfTable1);
            _doc.Add(_aluminumsTable);
            _doc.Add(_itemsTable);
         
            _doc.Add(_totalsTable);


            _doc.Close();
            return _memoryStream.ToArray();
        }
        private void ReportHeader()
        {
            //Fonts
            //string fontpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") + "/Fonts/ARIAL.TTF";
            //string fontpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\Fonts\\Arial.ttf";
            //string fontpath =sitePath + "\\Fonts\\ARIAL.TTF";
            BaseFont basefont = BaseFont.CreateFont
            (fontpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font normalFont = new Font(basefont, 10, Font.NORMAL, BaseColor.BLACK);
            Font boldFont = new Font(basefont, 20, Font.BOLD, BaseColor.BLACK);


            //Direction of tables
            _pdfTable1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _itemsTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _aluminumsTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _totalsTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;



            _fontStyle = boldFont;
            _pdfCell = new PdfPCell(new Phrase("", _fontStyle));

            _pdfCell.Colspan = _totalColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.HorizontalAlignment = Element.ALIGN_TOP;
            _pdfCell.Border = 0;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            //_pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 50;
            _pdfTable1.AddCell(_pdfCell);
            _pdfTable1.CompleteRow();


            _fontStyle = new Font(basefont, 17, Font.BOLD, BaseColor.BLACK); ;
            _pdfCell = new PdfPCell(new Phrase(_model.Title, _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            _pdfCell.ExtraParagraphSpace = 50;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.HorizontalAlignment = Element.ALIGN_TOP;
            _pdfCell.Border = 0;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            //_pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable1.AddCell(_pdfCell);
            _pdfTable1.CompleteRow();

            _fontStyle = normalFont;
            _pdfCell = new PdfPCell(new Phrase("صاحب الموقع" + " " + _model.SiteOwnerName, _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            //_pdfCell.PaddingRight = 20;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.Border = 0;
            //_pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 5;
            _pdfTable1.AddCell(_pdfCell);
            _pdfTable1.CompleteRow();

            _fontStyle = normalFont;
            _pdfCell = new PdfPCell(new Phrase("رقم صاحب الموقع:" + " " + _model.SiteOwnerPhone, _fontStyle));
            _pdfCell.Colspan = _totalColumn;
            //_pdfCell.PaddingRight = 20;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.VerticalAlignment = Element.ALIGN_TOP;
            _pdfCell.Border = 0;
            //_pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.ExtraParagraphSpace = 20;
            _pdfTable1.AddCell(_pdfCell);
            _pdfTable1.CompleteRow();





        }

        private void ReportBody()
        {
            //string fontpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\Fonts\\Arial.ttf";
            BaseFont basefont = BaseFont.CreateFont
            (fontpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font normalFont = new Font(basefont, 10, Font.NORMAL, BaseColor.BLACK);
            Font boldFont = new Font(basefont, 12, Font.BOLD, BaseColor.BLACK);

            BaseColor lightgrey = WebColors.GetRGBColor("#f2f2f2");
            BaseColor darkblue = WebColors.GetRGBColor("#3ea3de");
            _fontStyle = new Font(basefont, 11, Font.BOLD, darkblue);

            _pdfCell = new PdfPCell(new Phrase("معلومات الموقع", boldFont));
            _pdfCell.PaddingBottom = 4;
            _pdfCell.Colspan = 6;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("الموقع", boldFont));
            _pdfCell.PaddingBottom = 10;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _pdfTable1.AddCell(_pdfCell);

            //_pdfCell = new PdfPCell(new Phrase("الوصف", boldFont));
            //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            //_pdfCell.BackgroundColor = lightgrey;
            //_pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("اسم صاحب الموقع", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _pdfTable1.AddCell(_pdfCell);



            _pdfCell = new PdfPCell(new Phrase("عدد الأمتار", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("النوافذ", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("عدد الأبواب", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("السعر الإجمالي", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _pdfTable1.AddCell(_pdfCell);
            _pdfTable1.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase(_model.SiteName, normalFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.PaddingBottom = 10;
            _pdfTable1.AddCell(_pdfCell);

           _pdfCell = new PdfPCell(new Phrase(_model.SiteOwnerName, normalFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfCell.PaddingBottom = 10;
            _pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_model.Meters, normalFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_model.WindowsNumber, normalFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_model.DoorsNumber , normalFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_model.SiteTotalPrice + " " + "SAR", normalFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable1.AddCell(_pdfCell);


            _pdfTable1.CompleteRow();

            _pdfTable1.SpacingBefore = 40;


      
            //_pdfTable1.SpacingAfter = 15;
            _fontStyle = new Font(basefont, 11, Font.BOLD, darkblue); ;

            _pdfCell = new PdfPCell(new Phrase("الألمنيوم المستخدم", boldFont));
            _pdfCell.PaddingBottom = 10;
            _pdfCell.Colspan = 3;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _aluminumsTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("نوع الألمنيوم", boldFont));
            _pdfCell.PaddingBottom = 10;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _aluminumsTable.AddCell(_pdfCell);

            //_pdfCell = new PdfPCell(new Phrase("الوصف", boldFont));
            //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            //_pdfCell.BackgroundColor = lightgrey;
            //_pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("سعر المتر", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _aluminumsTable.AddCell(_pdfCell);



            _pdfCell = new PdfPCell(new Phrase("السعر الكلي", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _aluminumsTable.AddCell(_pdfCell);

            foreach (var item in _model.Aluminums)
            {

                _pdfCell = new PdfPCell(new Phrase(item.AluminumName, normalFont));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
                _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfCell.PaddingBottom = 10;
                _aluminumsTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(item.MeterPrice, normalFont));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
                _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _aluminumsTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(item.TotalPrice + " " + "SAR", normalFont));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
                _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _aluminumsTable.AddCell(_pdfCell);



                _aluminumsTable.CompleteRow();

            }
            _aluminumsTable.SpacingBefore = 40;

            _pdfCell = new PdfPCell(new Phrase("المواد المستخدمة", boldFont));
            _pdfCell.PaddingBottom = 4;
            _pdfCell.Colspan = 4;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _itemsTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("المواد", boldFont));
            _pdfCell.PaddingBottom = 10;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _itemsTable.AddCell(_pdfCell);

            //_pdfCell = new PdfPCell(new Phrase("الوصف", boldFont));
            //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            //_pdfCell.BackgroundColor = lightgrey;
            //_pdfTable1.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("الكمية المستخدمة", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _itemsTable.AddCell(_pdfCell);



            _pdfCell = new PdfPCell(new Phrase("سعر الوحدة", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _itemsTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("الإجمالي", boldFont));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            _pdfCell.BackgroundColor = lightgrey;
            _itemsTable.AddCell(_pdfCell);


            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
            int serialNumber = 1;
            foreach (var item in _model.Items)
            {

                _pdfCell = new PdfPCell(new Phrase(item.ItemName, normalFont));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
                _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _pdfCell.PaddingBottom = 10;
                _itemsTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(item.UsedQuantity.ToString(), normalFont));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
                _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _itemsTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(item.UnitPrice + " " + "SAR", normalFont));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
                _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _itemsTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(item.Price + " " + "SAR", normalFont));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
                _pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                _pdfCell.BackgroundColor = BaseColor.WHITE;
                _itemsTable.AddCell(_pdfCell);


                _itemsTable.CompleteRow();

            }
            _itemsTable.SpacingBefore = 40;

        }

        private void ReportFooter()
        {
            //string fontpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\Fonts\\Arial.ttf";
            //Font fontpath = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL));
            BaseFont basefont = BaseFont.CreateFont
            (fontpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font normalFont = new Font(basefont, 10, Font.NORMAL, BaseColor.BLACK);
            Font boldFont = new Font(basefont, 12, Font.BOLD, BaseColor.BLACK);
            Font boldFontsmall = new Font(basefont, 10, Font.BOLD, BaseColor.BLACK);

            BaseColor blue = WebColors.GetRGBColor("#cbe8f9");
            BaseColor gray = WebColors.GetRGBColor("#f2f2f2");
            _fontStyle = new Font(basefont, 10, Font.NORMAL, BaseColor.WHITE); ;



            //_pdfCell = new PdfPCell(new Phrase("المجموع", boldFont));
            //_pdfCell.Colspan = 1;
            //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            //_pdfCell.BackgroundColor = gray;
            //_pdfCell.PaddingBottom = 10;
            //_totalsTable.AddCell(_pdfCell);
            //_pdfCell = new PdfPCell(new Phrase(_model.t + " " + "SAR", boldFontsmall));
            //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.VerticalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            //_pdfCell.BackgroundColor = gray;
            //_totalsTable.AddCell(_pdfCell);

            _totalsTable.CompleteRow();
            _totalsTable.SpacingAfter = 20;

        }

    }
}
