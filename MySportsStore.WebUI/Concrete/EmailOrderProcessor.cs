using System.Net;
using System.Net.Mail;
using System.Text;
using MySportsStore.WebUI.Abstract;

namespace MySportsStore.WebUI.Concrete
{
    public class EmailSettings
    {
        public bool WriteAsFile = true;
        public string FileLocation = @"F:\sports_store_emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        //private EmailSettings emailSettings;

        //public EmailOrderProcessor(EmailSettings settings)
        //{
        //    emailSettings = settings;
        //}

        public void ProcessOrder(Models.Cart cart, Model.ShippingDetail shippingDetail)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress("qdjjx9441@sina.com");
            mailMsg.To.Add(new MailAddress("764190362@qq.com"));
            mailMsg.Subject = "新订单";          

            //邮件内容主体
            StringBuilder body = new StringBuilder();
            body.AppendLine("接收到一个新订单：");
            body.AppendLine("<br />");
            body.AppendLine("订购商品包括：");
            body.AppendLine("<br />");
            foreach (var line in cart.Lines)
            {
                var subTotal = line.Product.Price * line.Quantity;
                body.AppendFormat("{0}*{1}(小计：{2:c})", line.Quantity, line.Product.Name, subTotal);
                body.AppendLine("<br />");
            }
            body.AppendFormat("总计:{0:c}", cart.ComputeTotalValue());
            body.AppendLine("<br />");
            body.AppendLine("收货人信息：");
            body.AppendLine(shippingDetail.Name);
            body.AppendLine(shippingDetail.Line);
            body.AppendLine("<br />");


            mailMsg.Body = body.ToString();
            mailMsg.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.sina.com");
            smtpClient.Credentials = new NetworkCredential("yourusername", "yourpassword");

            //if (emailSettings.WriteAsFile)
            //{
            //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            //    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
            //    smtpClient.EnableSsl = false;
            //    mailMsg.BodyEncoding = Encoding.ASCII;
            //}
            smtpClient.Send(mailMsg);


            //using (SmtpClient smtpClient = new SmtpClient())
            //{
            //    //smtpClient.EnableSsl = emailSettings.UseSsl;
            //    smtpClient.Host = emailSettings.ServerName;
            //    //smtpClient.Port = emailSettings.ServerPort;
            //    //smtpClient.UseDefaultCredentials = false;
            //    smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

            //    if (emailSettings.WriteAsFile)
            //    {
            //        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            //        smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
            //        smtpClient.EnableSsl = false;
            //    }

            //    //邮件内容主体
            //    StringBuilder body = new StringBuilder();
            //    body.AppendLine("接收到一个新订单");
            //    body.AppendLine("------");
            //    body.AppendLine("订购商品包括：");
            //    foreach (var line in cart.Lines)
            //    {
            //        var subTotal = line.Product.Price*line.Quantity;
            //        body.AppendFormat("{0}*{1}(小计：{2:c})", line.Quantity, line.Product.Name, subTotal);
            //    }
            //    body.AppendFormat("总计:{0:c}", cart.ComputeTotalValue());
            //    body.AppendLine("------");
            //    body.AppendLine("收货人信息：");
            //    body.AppendLine(shippingDetail.Name);
            //    body.AppendLine(shippingDetail.Line);
            //    body.AppendLine("------");

            //    MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress, "新订单", body.ToString());
            //    if (emailSettings.WriteAsFile)
            //    {
            //        mailMessage.BodyEncoding = Encoding.ASCII;
            //    }

            //    smtpClient.Send(mailMessage);
            //}
        }
    }
}
