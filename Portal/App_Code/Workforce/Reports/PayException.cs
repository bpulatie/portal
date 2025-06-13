using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for member
/// </summary>
/// 
namespace Reports
{

    public class PayException
    {
        public PayException()
        {
        }

        public byte[] GenerateReport(string user_id, string id, string sFilter)
        {
            BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\calibrib.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font hdr = new Font(f_cb, 8);
            Font dtl = new Font(f_cn, 8);


            string filter = SPA.spaGlobals.ProcessJsonFilters(sFilter);

            DataLayer.pay_period oPeriod = new DataLayer.pay_period();
            DataSet pp = oPeriod.GetPayPeriodByID(id);

            DataLayer.pay_detail oDetail = new DataLayer.pay_detail();
            DataSet ds = oDetail.GetAllExceptionsByPayPeriodReport(user_id, id, filter);

            using (var memoryStream = new MemoryStream())
            {

                using (var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 40f, 40f, 60f, 70f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    writer.CloseStream = false;

                    ITextEvents x = new ITextEvents();
                    x.Header = "PAY EXCEPTION REPORT";
                    x.Filter = filter;
                    x.pp = pp;
                    writer.PageEvent = x;
                    
                    // Add meta information to the document
                    document.AddAuthor("PFC");
                    document.AddTitle("Pay Exception Report");

                    // Open the document to enable you to write to the document
                    document.Open();

                    PdfPTable myTable = new PdfPTable(7);
            
                    // Set the overall width of the table to render
                    myTable.TotalWidth = 760f;
                    myTable.HorizontalAlignment = 0;
                    myTable.SpacingAfter = 10;

                    float[] sglTblHdWidths = new float[7];
                    sglTblHdWidths[0] = 50f;
                    sglTblHdWidths[1] = 75f;
                    sglTblHdWidths[2] = 75f;
                    sglTblHdWidths[3] = 150f;
                    sglTblHdWidths[4] = 50f;
                    sglTblHdWidths[5] = 100f;
                    sglTblHdWidths[6] = 260f;
            
                    // The following two settings are also important to set the overall width of the table to render
                    myTable.SetWidths(sglTblHdWidths);
                    myTable.LockedWidth = true;

                    foreach (DataRow drItem in ds.Tables[0].Rows)
                    {
                        PdfPCell data = new PdfPCell(new Phrase(drItem["site_code"].ToString(), dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase(drItem["last_name"].ToString(), dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase(drItem["first_name"].ToString(), dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase(drItem["exception_code_name"].ToString(), dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase(drItem["status_code_name"].ToString(), dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase(drItem["reason_code"].ToString(), dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase(drItem["comment"].ToString(), dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);
                    }

                    document.Add(myTable);
                    document.Close();
            
                    writer.Close();
                }

                byte[] docArray = memoryStream.ToArray();
                memoryStream.Close();

                return docArray;
            }
        }

    }

    public class ITextEvents : PdfPageEventHelper
    {
        PdfContentByte cb;
        PdfTemplate headerTemplate, footerTemplate;
        BaseFont bf = null;
        DateTime PrintTime = DateTime.Now;

        int left_margin = 40;

        private string _header;
        private string _filter;
        private DataSet _pp;

        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public string Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        public DataSet pp
        {
            get { return _pp; }
            set { _pp = value; }
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {

            }
            catch (System.IO.IOException ioe)
            {

            }
        }
        
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\calibrib.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font baseFontBig = new Font(f_cb, 8);
            Font baseFontNormal = new Font(f_cn, 8);

            cb.BeginText();

            cb.SetFontAndSize(f_cn, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Week No:", left_margin + 0, 575, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Start Date:", left_margin + 0, 567, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "End Date:", left_margin + 0, 559, 0);

            _pp.Tables[0].Columns["start_date"].DateTimeMode = DataSetDateTime.Unspecified;
            _pp.Tables[0].Columns["end_date"].DateTimeMode = DataSetDateTime.Unspecified;

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, _pp.Tables[0].Rows[0]["week_no"].ToString(), left_margin + 50, 575, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, DateTime.Parse(_pp.Tables[0].Rows[0]["start_date"].ToString()).ToShortDateString(), left_margin + 50, 567, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, DateTime.Parse(_pp.Tables[0].Rows[0]["end_date"].ToString()).ToShortDateString(), left_margin + 50, 559, 0);

            cb.SetFontAndSize(f_cb, 14);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, _header, left_margin + 300, 570, 0);

            cb.SetFontAndSize(f_cn, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Employee Filter:", left_margin + 620, 575, 0);

            string sFilter = "All Employees";
            if (_filter.Length > 0)
            {
                sFilter = _filter.Substring(5);
            }

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sFilter, left_margin + 680, 575, 0);

            cb.SetFontAndSize(f_cb, 10);

            // Write out details from the payee table
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Location", left_margin + 0, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Last Name", left_margin + 50, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "First Name", left_margin + 125, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Exception Code", left_margin + 200, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Status", left_margin + 350, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Reason Code", left_margin + 400, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Comment", left_margin + 500, 545, 0);

            cb.EndText();

            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 540);
            cb.LineTo(800, 540);
            cb.Stroke();

            cb.BeginText();
            // Write out details from the payee table
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Page: " + writer.PageNumber, left_margin + 0, 52, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Date Generated: " + DateTime.Now.ToShortDateString(), left_margin + 625, 52, 0);

            cb.EndText();

            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 60);
            cb.LineTo(800, 60);
            cb.Stroke();        
        
        
        }

    }
}