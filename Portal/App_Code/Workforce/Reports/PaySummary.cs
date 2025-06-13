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

    public class PaySummary
    {
        BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\calibrib.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        int iPage;
        int left_margin;

        public PaySummary()
        {
        }

        public byte[] GenerateReport(string user_id, string pgid, string id, string sFilter)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //dynamic x = serializer.Deserialize(json, typeof(object));

            string filter = SPA.spaGlobals.ProcessJsonFilters(sFilter);

            DataLayer.pay_period oPeriod = new DataLayer.pay_period();
            DataSet pp = oPeriod.GetPayPeriodByID(id);

            DataLayer.pay_group oGroup = new DataLayer.pay_group();
            if (pgid == "")
            {
                pgid = Guid.Empty.ToString();
            }
            DataSet pg = oGroup.GetPayGroupByID(pgid);

            DataLayer.pay_detail oDetail = new DataLayer.pay_detail();
            DataSet ds = oDetail.GetPaySummaryReport(user_id, pgid, id, filter);

            int top_margin = 535;
            int lastwriteposition = 80;
            left_margin = 40;
            iPage = 1;

            using (var memoryStream = new MemoryStream())
            {

                using (var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate()))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    writer.CloseStream = false;

                    // Add meta information to the document
                    document.AddAuthor("PFC");
                    document.AddTitle("Pay Summary Report");

                    // Open the document to enable you to write to the document
                    document.Open();
                    // Add a simple and wellknown phrase to the document in a flow layout manner

                    PdfContentByte cb = writer.DirectContent;
                    PageHeader(cb, pp, pg, filter);

                    top_margin = 528;

                    foreach (DataRow drItem in ds.Tables[0].Rows)
                    {
                        writeText(cb, drItem["site_code"].ToString(), left_margin + 0, top_margin, f_cn, 10);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["reg_hours"].ToString(), left_margin + 100, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["ot_hours"].ToString(), left_margin + 145, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["dbltm_hours"].ToString(), left_margin + 190, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["tip_amount"].ToString(), left_margin + 235, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["hday_hours"].ToString(), left_margin + 280, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["rptme_hours"].ToString(), left_margin + 325, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["rptme_amount"].ToString(), left_margin + 370, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["spsft_hours"].ToString(), left_margin + 415, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["spsft_amount"].ToString(), left_margin + 460, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["mlbrk_hours"].ToString(), left_margin + 505, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["jury_hours"].ToString(), left_margin + 550, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["breav_hours"].ToString(), left_margin + 595, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["other_hours"].ToString(), left_margin + 640, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["total_hours"].ToString(), left_margin + 685, top_margin, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["total_amount"].ToString(), left_margin + 730, top_margin, 0);


                        // This is the line spacing, if you change the font size, you might want to change this as well.
                        top_margin -= 12;

                        // Implement a page break function, checking if the write position has reached the lastwriteposition
                        if (top_margin <= lastwriteposition)
                        {
                            PageFooter(cb);

                            // Make the page break
                            document.NewPage();
                            iPage++;

                            // Start the writing again
                            PageHeader(cb, pp, pg, filter);

                            top_margin = 538;
                        }
                    }

                    PageFooter(cb);

                    // Close the document
                    document.Close();
                    writer.Close();
                }

                byte[] docArray = memoryStream.ToArray();
                memoryStream.Close();

                return docArray;
            }
        }

        // This is the method writing text to the content byte
        private void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }

        private void PageHeader(PdfContentByte cb, DataSet pp, DataSet pg, string filter)
        {
            cb.BeginText();

            cb.SetFontAndSize(f_cn, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Week No:", left_margin + 0, 575, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Start Date:", left_margin + 0, 567, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "End Date:", left_margin + 0, 559, 0);

            pp.Tables[0].Columns["start_date"].DateTimeMode = DataSetDateTime.Unspecified;
            pp.Tables[0].Columns["end_date"].DateTimeMode = DataSetDateTime.Unspecified;

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, pp.Tables[0].Rows[0]["week_no"].ToString(), left_margin + 50, 575, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, DateTime.Parse(pp.Tables[0].Rows[0]["start_date"].ToString()).ToShortDateString(), left_margin + 50, 567, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, DateTime.Parse(pp.Tables[0].Rows[0]["end_date"].ToString()).ToShortDateString(), left_margin + 50, 559, 0);

            cb.SetFontAndSize(f_cb, 14);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PAY SUMMARY REPORT", left_margin + 300, 570, 0);

            cb.SetFontAndSize(f_cn, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Filter:", left_margin + 580, 575, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Pay Group:", left_margin + 580, 567, 0);

            string sFilter = "All Employees";
            if (filter.IndexOf("'1'") > 0)
            {
                sFilter = "Hourly Manager";
            }
            if (filter.IndexOf("'0'") > 0)
            {
                sFilter = "Hourly Team Member";
            }
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sFilter, left_margin + 620, 575, 0);

            string gFilter = "All Pay Groups";
            if (pg.Tables[0].Rows.Count > 0)
            {
                gFilter = pg.Tables[0].Rows[0]["pay_group_code"].ToString() + " - " + pg.Tables[0].Rows[0]["filter_description"].ToString();
            }
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, gFilter, left_margin + 620, 567, 0);

            cb.SetFontAndSize(f_cb, 10);

            // Write out details from the payee table
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Location", left_margin + 0, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Reg Hrs", left_margin + 100, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "OT Hrs", left_margin + 145, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "DBT Hrs", left_margin + 190, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Tip Amt", left_margin + 235, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Hday Hrs", left_margin + 280, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Rpt Hrs", left_margin + 325, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Rpt Pay", left_margin + 370, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "SS Hrs", left_margin + 415, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "SS Pay", left_margin + 460, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "MB Hrs", left_margin + 505, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Jury Hrs", left_margin + 550, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Bmt Hrs", left_margin + 595, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Other Hrs", left_margin + 640, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Total Hrs", left_margin + 685, 545, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Total Pay", left_margin + 730, 545, 0);

            cb.EndText();

            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 540);
            cb.LineTo(800, 540);
            cb.Stroke();

            cb.BeginText();
        }

        private void PageFooter(PdfContentByte cb)
        {
            cb.EndText();

            // Move to the bottom left corner of the template
            cb.MoveTo(1, 1);
            cb.Stroke();
            cb.SetFontAndSize(f_cn, 8);

            cb.BeginText();

            // Write out details from the payee table
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Page: " + iPage, left_margin + 10, 52, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Date Generated: " + DateTime.Now.ToShortDateString(), left_margin + 650, 52, 0);

            cb.EndText();

            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 60);
            cb.LineTo(800, 60);
            cb.Stroke();
        }
    }
}