using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EliteBlindsAPI.Models;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Text;

namespace EliteBlindsAPI.Business
{
    public class Business : IBusiness
    {
        IEliteBusiness EliteBusinessObj = new EliteBusiness();

        //Customer Related Methods
        public bool LoginCheck(string Email, string Password)
        {
            return EliteBusinessObj.LoginCheck(Email, Password);
        }

        public bool ForgotPassword(string Email)
        {
            SendMail(Email, "Please Reset the Password using the below link. <br/>" + ConfigurationManager.AppSettings["WebLink"].ToString()+"/api/Coustomer/ResetPassword","Password Reset");
            return EliteBusinessObj.ForgotPassword(Email);
        }

        public bool ResetPassword(string Email, string Password)
        {
            return EliteBusinessObj.ResetPassword(Email, Password);
        }

        public bool SetUserActive(int ID)
        {
            return EliteBusinessObj.SetUserActive(ID);
        }

        public void DeleteCustomer(int ID)
        {
            EliteBusinessObj.DeleteCustomer(ID);
        }

        public Customer GetCustomer(int ID)
        {
            return EliteBusinessObj.GetCustomer(ID);
        }

        public List<Order> GetCustOrders(int ID)
        {
            return EliteBusinessObj.GetCustomerOrders(ID);
        }

        public List<Customer> GetCustomers()
        {
            return EliteBusinessObj.GetCustomers();
        }

        public List<UtilityOrder> GetCustomerUtilityOrders(int CustomerID)
        {
            return EliteBusinessObj.GetCustomerUtilityOrders(CustomerID);
        }

        public Tuple<List<Order>, List<UtilityOrder>> GetAllCustomerOrders(int ID)
        {
            var listOrder = EliteBusinessObj.GetCustomerOrders(ID);
            var listUtilityOrder = EliteBusinessObj.GetCustomerUtilityOrders(ID);

            return Tuple.Create(listOrder, listUtilityOrder);
        }

        public void SaveCustomer(Customer CustData)
        {
            if (!EliteBusinessObj.UserCheck(CustData.Email))
            {
                if (CustData.CustomerID > 0)
                {
                    EliteBusinessObj.UpdateCustomer(CustData);
                }
                else
                {
                    EliteBusinessObj.SaveCustomer(CustData);
                    SendMail(CustData.Email, "Please click the below link to Activate User. <br/>" + ConfigurationManager.AppSettings["WebLink"] + "/api/Customer/ActivateCustomer/" + CustData.CustomerID, "User Activation");
                }
            }
            else
            {
                throw new Exception("User with same Email existing already");
            }
        }


        //Order Related

        public Order GetOrder(int ID)
        {
            return EliteBusinessObj.GetOrder(ID);
        }

        public OrderDetail GetOrderDetail(int ID)
        {
            return EliteBusinessObj.GetOrderDetail(ID);
        }

        public List<OrderDetail> GetOrderDetails(int ID)
        {
            return EliteBusinessObj.GetOrderDetails(ID);
        }

        public UtilityOrder GetUtilityOrder(int ID)
        {
            return EliteBusinessObj.GetUtilityOrder(ID);
        }

        public void SaveOrder(Order OrderData)
        {
            if (OrderData.OrderID > 0)
            {
                EliteBusinessObj.UpdateOrder(OrderData);
            }
            else
            {
                EliteBusinessObj.SaveOrder(OrderData);
            }
            SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), "New Order Has been Created", "New Order");
        }

        public void SaveUtilityOrder(UtilityOrder OrderData)
        {
            if (OrderData.UtilityOrderID > 0)
            {
                EliteBusinessObj.UpdateUtilityOrder(OrderData);
            }
            else
            {
                EliteBusinessObj.SaveUtilityOrder(OrderData);
            }
            SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), "New Utility Order Has been Created", "New Utility Order");
        }

        public void DeleteOrder(int ID)
        {
            EliteBusinessObj.DeleteOrder(ID);
            SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), "Order Has been Deleted", "Order Deleted");
        }

        public void DeleteUtilityOrder(int ID)
        {
            EliteBusinessObj.DeleteUtilityOrder(ID);
            SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), "Utility Order Has been Deleted", "Utility Order Deleted");
        }

        public List<UtilityOrder> GetUtilityOrders(int ID)
        {
            return EliteBusinessObj.GetCustomerUtilityOrders(ID);
        }


        //Related to Utility Orders

        public Fabric GetFabric(int ID)
        {
            return EliteBusinessObj.GetFabric(ID);
        }

        public List<Fabric> GetFabric()
        {
            return EliteBusinessObj.GetFabric();
        }

        public List<Fabric> GetFabrics(int ID)
        {
            return EliteBusinessObj.GetFabrics(ID);
        }

        public void SaveFabric(Fabric FabricData)
        {
            if (FabricData.FabricID > 0)
            {
                EliteBusinessObj.UpdateFabric(FabricData);
            }
            else
            {
                EliteBusinessObj.SaveFabric(FabricData);
            }
        }

        public void DeleteFabric(int ID)
        {
            EliteBusinessObj.DeleteFabric(ID);
        }

        public RollerBlinds GetRollerBlind(int ID)
        {
            return EliteBusinessObj.GetRollerBlind(ID);
        }

        public List<RollerBlinds> GetRollerBlinds()
        {
            return EliteBusinessObj.GetRollerBlinds();
        }

        public List<RollerBlinds> GetRollerBlinds(int ID)
        {
            return EliteBusinessObj.GetRollerBlinds(ID);
        }

        public void SaveRollerBlinds(RollerBlinds RollerBlindsData)
        {
            if (RollerBlindsData.RollerBlindsID > 0)
            {
                EliteBusinessObj.UpdateRollerBlinds(RollerBlindsData);
            }
            else
            {
                EliteBusinessObj.SaveRollerBlinds(RollerBlindsData);
            }
        }

        public void DeleteRollerBlinds(int ID)
        {
            EliteBusinessObj.DeleteRollerBlinds(ID);
        }

        public Valance GetValance(int ID)
        {
            return EliteBusinessObj.GetValance(ID);
        }
        
        public List<Valance> GetValance()
        {
            return EliteBusinessObj.GetValance();
        }

        public List<Valance> GetValances(int ID)
        {
            return EliteBusinessObj.GetValances(ID);
        }

        public void SaveValance(Valance ValanceData)
        {
            if (ValanceData.ValanceID > 0)
            {
                EliteBusinessObj.UpdateValance(ValanceData);
            }
            else
            {
                EliteBusinessObj.SaveValance(ValanceData);
            }
        }

        public void DeleteValance(int ID)
        {
            EliteBusinessObj.DeleteValance(ID);
        }

        public BottomRail GetBottomRail(int ID)
        {
            return EliteBusinessObj.GetBottomRail(ID);
        }

        public List<BottomRail> GetBottomRail()
        {
            return EliteBusinessObj.GetBottomRail();
        }

        public List<BottomRail> GetBottomRails(int ID)
        {
            return EliteBusinessObj.GetBottomRails(ID);
        }

        public void SaveBottomRail(BottomRail BottomRailData)
        {
            if (BottomRailData.BottomRailID > 0)
            {
                EliteBusinessObj.UpdateBottomRail(BottomRailData);
            }
            else
            {
                EliteBusinessObj.SaveBottomRail(BottomRailData);
            }
        }

        public void DeleteBottomRail(int ID)
        {
            EliteBusinessObj.DeleteBottomRail(ID);
        }



        //Other Methods
        public List<Colors> GetColors()
        {
            return EliteBusinessObj.GetColors();
        }

        public List<Control> GetControl()
        {
            return EliteBusinessObj.GetControl();
        }

        public List<CordStyle> GetCordStyle()
        {
            return EliteBusinessObj.GetCordStyle();
        }

        public List<Material> GetMaterial()
        {
            return EliteBusinessObj.GetMaterial();
        }

        public List<SlatStyle> GetSlatStyle()
        {
            return EliteBusinessObj.GetSlatStyle();
        }

        public List<Size> GetSize()
        {
            return EliteBusinessObj.GetSize();
        }

        public List<BlindType> GetBlindType()
        {
            return EliteBusinessObj.GetBlindType();
        }

        public List<Colors> GetColors(string For)
        {
            return EliteBusinessObj.GetColors(For);
        }

        public List<Control> GetControl(string For)
        {
            return EliteBusinessObj.GetControl(For);
        }

        public List<CordStyle> GetCordStyle(string For)
        {
            return EliteBusinessObj.GetCordStyle(For);
        }

        public List<Material> GetMaterial(string For)
        {
            return EliteBusinessObj.GetMaterial(For);
        }

        public List<SlatStyle> GetSlatStyle(string For)
        {
            return EliteBusinessObj.GetSlatStyle(For);
        }

        public List<Size> GetSize(string For)
        {
            return EliteBusinessObj.GetSize(For);
        }

        public void SendMail(string to, string body, string subject, params MailAttachment[] attachments)
        {
            string host = ConfigurationManager.AppSettings["SMTPHost"];
            string fromAddress = ConfigurationManager.AppSettings["SMTPFrom"];
            try
            {
                MailMessage mail = new MailMessage();
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(to));
                mail.From = new MailAddress(fromAddress);
                mail.Subject = subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                foreach (MailAttachment ma in attachments)
                {
                    mail.Attachments.Add(ma.File);
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder(1024);
                sb.Append("\nTo:" + to);
                sb.Append("\nbody:" + body);
                sb.Append("\nsubject:" + subject);
                sb.Append("\nfromAddress:" + fromAddress);
                sb.Append("\nHosting:" + host);
                throw new Exception(sb.ToString(), ex);
            }
        }
        
    }

    public class MailAttachment
    {
        #region Fields
        private MemoryStream stream;
        private string filename;
        private string mediaType;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the data stream for this attachment
        /// </summary>
        public Stream Data { get { return stream; } }
        /// <summary>
        /// Gets the original filename for this attachment
        /// </summary>
        public string Filename { get { return filename; } }
        /// <summary>
        /// Gets the attachment type: Bytes or String
        /// </summary>
        public string MediaType { get { return mediaType; } }
        /// <summary>
        /// Gets the file for this attachment (as a new attachment)
        /// </summary>
        public Attachment File { get { return new Attachment(Data, Filename, MediaType); } }
        #endregion
        #region Constructors
        /// <summary>
        /// Construct a mail attachment form a byte array
        /// </summary>
        /// <param name="data">Bytes to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(byte[] data, string filename)
        {
            this.stream = new MemoryStream(data);
            this.filename = filename;
            this.mediaType = System.Net.Mime.MediaTypeNames.Application.Octet;
        }
        /// <summary>
        /// Construct a mail attachment from a string
        /// </summary>
        /// <param name="data">String to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(string data, string filename)
        {
            this.stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(data));
            this.filename = filename;
            this.mediaType = System.Net.Mime.MediaTypeNames.Text.Html;
        }
        #endregion
    }
}
