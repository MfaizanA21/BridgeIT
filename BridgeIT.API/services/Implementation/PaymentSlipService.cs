using BridgeIT.API.DTOs.PaymentDTOs;
using BridgeIT.API.services.Interface;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;

namespace BridgeIT.API.services.Implementation;

public class PaymentSlipService : IPaymentSlipService
{
    public byte[] GeneratePaymentSlip(PaymentSlipDto paymentSlipDto)
    {
        PdfDocument paymentSlip = new PdfDocument();
        paymentSlip.PageSettings.Margins.All = 30;
        PdfPage page = paymentSlip.Pages.Add();
        PdfGraphics graphics = page.Graphics;

        PdfFont headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 24, PdfFontStyle.Bold);
        PdfFont subHeaderFont = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);
        PdfFont regularFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
        PdfFont boldFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);

        PdfBrush headerBrush = PdfBrushes.DarkBlue;
        PdfBrush textBrush = PdfBrushes.Black;
        PdfBrush accentBrush = PdfBrushes.Gray;

        graphics.DrawString("BridgeIT Payment Slip", headerFont, headerBrush, new PointF(0, 0));
        graphics.DrawLine(new PdfPen(Color.Gray, 1), new PointF(0, 40), new PointF(page.GetClientSize().Width, 40));

        graphics.DrawString("BridgeIT Solutions", subHeaderFont, headerBrush, new PointF(0, 50));
        graphics.DrawString("www.bridgeit.com", regularFont, textBrush, new PointF(0, 70));

        float yPosition = 100;
        graphics.DrawString("Payment Details", subHeaderFont, headerBrush, new PointF(0, yPosition));
        yPosition += 30;

        float labelX = 0;
        float valueX = 200;

        graphics.DrawString("Payee Name:", boldFont, textBrush, new PointF(labelX, yPosition));
        graphics.DrawString(paymentSlipDto.projecteeName, regularFont, textBrush, new PointF(valueX, yPosition));
        yPosition += 20;

        graphics.DrawString("Client Name:", boldFont, textBrush, new PointF(labelX, yPosition));
        graphics.DrawString(paymentSlipDto.ClientName, regularFont, textBrush, new PointF(valueX, yPosition));
        yPosition += 20;

        graphics.DrawString("Project Title:", boldFont, textBrush, new PointF(labelX, yPosition));
        graphics.DrawString(paymentSlipDto.ProjectTitle, regularFont, textBrush, new PointF(valueX, yPosition));
        yPosition += 20;

        graphics.DrawString("Payment Date:", boldFont, textBrush, new PointF(labelX, yPosition));
        graphics.DrawString(paymentSlipDto.PaymentDate.ToString("MMMM dd, yyyy"), regularFont, textBrush, new PointF(valueX, yPosition));
        yPosition += 20;

        graphics.DrawString("Payment Method:", boldFont, textBrush, new PointF(labelX, yPosition));
        graphics.DrawString(paymentSlipDto.PaymentMethod, regularFont, textBrush, new PointF(valueX, yPosition));
        yPosition += 20;

        graphics.DrawString("Payment Status:", boldFont, textBrush, new PointF(labelX, yPosition));
        graphics.DrawString(paymentSlipDto.PaymentStatus, regularFont, textBrush, new PointF(valueX, yPosition));
        yPosition += 20;

        graphics.DrawString("Payment Amount:", boldFont, textBrush, new PointF(labelX, yPosition));
        graphics.DrawString(paymentSlipDto.PaymentAmount, regularFont, textBrush, new PointF(valueX, yPosition));
        yPosition += 20;

        graphics.DrawString("Transaction ID:", boldFont, textBrush, new PointF(labelX, yPosition));
        graphics.DrawString(paymentSlipDto.TransactionId, regularFont, textBrush, new PointF(valueX, yPosition));
        yPosition += 30;

        graphics.DrawLine(new PdfPen(Color.Gray, 1), new PointF(0, yPosition), new PointF(page.GetClientSize().Width, yPosition));
        yPosition += 10;
        graphics.DrawString("Thank you for your business!", regularFont, accentBrush, new PointF(0, yPosition));
        graphics.DrawString("For inquiries, contact: bridgeit.care@gmail.com", regularFont, accentBrush, new PointF(0, yPosition + 20));

        MemoryStream stream = new MemoryStream();
        paymentSlip.Save(stream);
        stream.Position = 0;
        paymentSlip.Close(true);

        return stream.ToArray();
    }
}