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

    public class RequirementReport
    {
        public RequirementReport()
        {
        }

        public byte[] GenerateReport(string mode, string id)
        {
            BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\calibrib.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font hdr = new Font(f_cb, 8);
            Font dtl = new Font(f_cn, 8);
/*

            string filter = SPA.spaGlobals.ProcessJsonFilters(sFilter);

            DataLayer.requirement oPeriod = new DataLayer.requirement();
            DataSet pp;// = oPeriod.GetAll("", 1, 1);

            DataLayer.requirement_list oDetail = new DataLayer.requirement_list();
            DataSet ds;// = oDetail.GetAll("", 1, 1);

            using (var memoryStream = new MemoryStream())
            {

                using (var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 40f, 40f, 60f, 70f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    writer.CloseStream = false;

                    ITextEvents x = new ITextEvents();
                    x.Header = "PAY EXCEPTION REPORT";
                    x.Filter = filter;
                    //x.pp = pp;
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

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        PdfPCell data = new PdfPCell(new Phrase("No Data", dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase("", dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase("", dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase("", dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase("", dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase("", dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);

                        data = new PdfPCell(new Phrase("", dtl));
                        data.BorderWidth = 0;
                        myTable.AddCell(data);
                    }

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
*/
            byte[] docArray = {};
            return docArray;
        }
    }

}