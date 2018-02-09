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

        #region Customer Related Methods
        public Customer LoginCheck(string Email, string Password)
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

        public List<Order> GetCustOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy)
        {
            int RoleID = GetCustomer(CustId).RoleID;
            if (RoleID != 4)
            {
                return EliteBusinessObj.GetOrdersForRole(RoleID);
            }
            else
            {
                return EliteBusinessObj.GetCustomerOrders(CustId, FilterBy, SearchCriteria, OrderBy);
            }
        }

        public List<Customer> GetCustomers()
        {
            return EliteBusinessObj.GetCustomers();
        }

        public List<UtilityOrder> GetCustomerUtilityOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy)
        {
            int RoleID = GetCustomer(CustId).RoleID;
            if (RoleID != 4)
            {
                return EliteBusinessObj.GetUtilityOrdersForRole(RoleID);
            }
            else
            {
                return EliteBusinessObj.GetCustomerUtilityOrders(CustId, FilterBy,SearchCriteria, OrderBy);

            }
        }

        public Tuple<List<Order>, List<UtilityOrder>> GetAllCustomerOrders(int ID)
        {
            var listOrder = EliteBusinessObj.GetCustomerOrders(ID);
            var listUtilityOrder = EliteBusinessObj.GetCustomerUtilityOrders(ID);

            return Tuple.Create(listOrder, listUtilityOrder);
        }

        public Customer SaveCustomer(Customer CustData)
        {
            if (!EliteBusinessObj.UserCheck(CustData.Email))
            {
                if (CustData.CustomerID>0)
                {
                    return EliteBusinessObj.UpdateCustomer(CustData);
                }
                else
                {
                    Customer Cust = EliteBusinessObj.SaveCustomer(CustData);
                    SendMail(CustData.Email, "Please click the below link to Activate User. <br/>" + ConfigurationManager.AppSettings["WebLink"] + "/api/Customer/ActivateCustomer/" + Cust.CustomerID, "User Activation");
                    return Cust;
                }
            }
            else
            {
                throw new Exception("User with same Email existing already");
            }
        }

        public List<EliteRoles> GetRoles()
        {
            return EliteBusinessObj.GetRoles();
        }
        #endregion

        #region Order Related
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

        public List<OrderDetail> GetOrderDetails(List<int> IDs)
        {
            return EliteBusinessObj.GetOrderDetails(IDs);
        }

        public UtilityOrder GetUtilityOrder(int ID)
        {
            return EliteBusinessObj.GetUtilityOrder(ID);
        }

        public Order SaveOrder(Order OrderData)
        {
            Order OrderObj = new Order();
            if (OrderData.OrderID>0)
            {
                OrderObj = EliteBusinessObj.UpdateOrder(OrderData);
                SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), CreateOrderMailBody(OrderObj) ,"Order Has been Updated");
            }
            else
            {
                OrderObj = EliteBusinessObj.SaveOrder(OrderData);
                SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), CreateOrderMailBody(OrderObj), "New Order Has been Created");
            }
            return OrderObj;
        }

        public List<OrderDetail> SaveOrderDetails(List<OrderDetail> OrderDetailData)
        {
            List<OrderDetail> RetList = new List<OrderDetail>();
            foreach (var orderDetails in OrderDetailData)
            {
                if (orderDetails.OrderDetailID>0)
                {
                    RetList.Add(EliteBusinessObj.UpdateOrderDetail(orderDetails));
                }
                else
                {
                    RetList.Add(EliteBusinessObj.SaveOrderDetail(orderDetails));
                }
            }
            return RetList;
        }

        public UtilityOrder SaveUtilityOrder(UtilityOrder OrderData)
        {
            UtilityOrder UtilityObj = new UtilityOrder();
            if (OrderData.UtilityOrderID>0)
            {
                UtilityObj = EliteBusinessObj.UpdateUtilityOrder(OrderData);
                SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), CreateUtilityOrderMailBody(OrderData) ,"Utility Order Has been Updated");
            }
            else
            {
                UtilityObj = EliteBusinessObj.SaveUtilityOrder(OrderData);
                SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), CreateUtilityOrderMailBody(OrderData), "New Utility Order Has been Created");
            }
            return UtilityObj;
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

        public Boolean ApproveOrders(List<int> OrderIDs)
        {
            bool res = EliteBusinessObj.ApproveOrders(OrderIDs);
            if (res)
            {
                foreach (var item in OrderIDs)
                {
                    var OrderData = GetOrder(item);
                    SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), CreateOrderMailBody(OrderData), "Order Approved");
                }
            }
            return res;
        }

        public Boolean ApproveUtilityOrders(List<int> OrderIDs)
        {
            bool res = EliteBusinessObj.ApproveUtilityOrders(OrderIDs);
            if (res)
            {
                foreach (var item in OrderIDs)
                {
                    var OrderData = GetUtilityOrder(item);
                    SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), CreateUtilityOrderMailBody(OrderData), "Order Approved");
                }
            }
            return res;
        }

        public Boolean ChangeOrderStatus(List<int> OrderIDs, int StatusID)
        {
            bool res = EliteBusinessObj.ChangeOrderStatus(OrderIDs, StatusID);
            if (res)
            {
                foreach (var item in OrderIDs)
                {
                    var OrderData = GetOrder(item);
                    SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), CreateOrderMailBody(OrderData), "Order Status Changed");
                }
            }
            return res;
        }

        public Boolean ChangeUtilityOrderStatus(List<int> OrderIDs, int StatusID)
        {
            bool res = EliteBusinessObj.ChangeUtilityOrderStatus(OrderIDs, StatusID);
            if (res)
            {
                foreach (var item in OrderIDs)
                {
                    var OrderData = GetUtilityOrder(item);
                    SendMail(ConfigurationManager.AppSettings["OrderNotification"].ToString(), CreateUtilityOrderMailBody(OrderData), "Order Approved");
                }
            }
            return res;
        }
        #endregion

        #region Related to Utility Orders
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

        public List<Fabric> SaveFabric(List<Fabric> FabricData)
        {
            List<Fabric> RetList = new List<Fabric>();
            foreach (var Fabrics in FabricData)
            {
                if (Fabrics.FabricID>0)
                {
                    RetList.Add(EliteBusinessObj.UpdateFabric(Fabrics));
                }
                else
                {
                    RetList.Add(EliteBusinessObj.SaveFabric(Fabrics));
                }
            }
            return RetList;
        }

        public void DeleteFabric(int ID)
        {
            EliteBusinessObj.DeleteFabric(ID);
        }

        public RollerBlinds GetRollerBlind(int ID)
        {
            return EliteBusinessObj.GetRollerBlind(ID);
        }

        public RollerBlindType GetRollerBlindType(int ID)
        {
            return EliteBusinessObj.GetRollerBlindType(ID);
        }

        public List<RollerBlinds> GetRollerBlinds()
        {
            return EliteBusinessObj.GetRollerBlinds();
        }

        public List<RollerBlinds> GetRollerBlinds(int ID)
        {
            return EliteBusinessObj.GetRollerBlinds(ID);
        }

        public List<RollerBlinds> SaveRollerBlinds(List<RollerBlinds> RollerBlindsData)
        {
            List<RollerBlinds> RetList = new List<RollerBlinds>();
            foreach (var RollerBlind in RollerBlindsData)
            {
                if (RollerBlind.RollerBlindsID>0)
                {
                    RetList.Add(EliteBusinessObj.UpdateRollerBlinds(RollerBlind));
                }
                else
                {
                    RetList.Add(EliteBusinessObj.SaveRollerBlinds(RollerBlind));
                }
            }
            return RetList;
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

        public List<Valance> SaveValance(List<Valance> ValanceData)
        {
            List<Valance> RetList = new List<Valance>();
            foreach (var Valance in ValanceData)
            {
                if (Valance.ValanceID>0)
                {
                    RetList.Add(EliteBusinessObj.UpdateValance(Valance));
                }
                else
                {
                    RetList.Add(EliteBusinessObj.SaveValance(Valance));
                }
            }
            return RetList;
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

        public List<BottomRail> SaveBottomRail(List<BottomRail> BottomRailData)
        {
            List<BottomRail> RetList = new List<BottomRail>();
            foreach (var BottomRail in BottomRailData)
            {
                if (BottomRail.BottomRailID>0)
                {
                    RetList.Add(EliteBusinessObj.UpdateBottomRail(BottomRail));
                }
                else
                {
                    RetList.Add(EliteBusinessObj.SaveBottomRail(BottomRail));
                }
            }
            return RetList;
        }

        public void DeleteBottomRail(int ID)
        {
            EliteBusinessObj.DeleteBottomRail(ID);
        }
        #endregion

        #region Other Methods
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

        public List<OrderStatus> GetOrderStatus()
        {
            return EliteBusinessObj.GetOrderStatus();
        }

        public List<OrderType> GetOrderType()
        {
            return EliteBusinessObj.GetOrderType();
        }

        public Colors GetColors(int ID)
        {
            return EliteBusinessObj.GetColors(ID);
        }

        public Control GetControl(int ID)
        {
            return EliteBusinessObj.GetControl(ID);
        }

        public CordStyle GetCordStyle(int ID)
        {
            return EliteBusinessObj.GetCordStyle(ID);
        }

        public Material GetMaterial(int ID)
        {
            return EliteBusinessObj.GetMaterial(ID);
        }

        public SlatStyle GetSlatStyle(int ID)
        {
            return EliteBusinessObj.GetSlatStyle(ID);
        }

        public Size GetSize(int ID)
        {
            return EliteBusinessObj.GetSize(ID);
        }

        public BlindType GetBlindType(int ID)
        {
            return EliteBusinessObj.GetBlindType(ID);
        }

        public OrderStatus GetOrderStatus(int ID)
        {
            return EliteBusinessObj.GetOrderStatus(ID);
        }

        public OrderType GetOrderType(int ID)
        {
            return EliteBusinessObj.GetOrderType(ID);
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
        #endregion

        #region Mail Feature
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

        public string CreateOrderMailBody(Order OrderObj)
        {
            StringBuilder strBody = new StringBuilder();
            StringBuilder strOrder = new StringBuilder();
            StringBuilder strOrderDetails = new StringBuilder();

            #region Order Part
            strOrder.AppendLine("<table style='width: 100%'> " +
                "<thead> " +
                "<tr style = 'white-space: nowrap;'>" +
                "<th>Order Status </th>" +
                "<th>Order Number </th>" +
                "<th>Order Date </th>" +
                "<th>Number Of Blinds </th>" +
                "<th>Company </th>" +
                "<th>Reference </th>" +
                "<th>Consign Note Number </th>" +
                "<th>Complete Date </th>" +
                "<th>Delivery Date </th>" +
                "<th>Departure Date </th>" +
                "<th>Arrival Date </th>" +
                "<th>Order M2 </th>" +
                "<th>Edit </th>" +
                "</tr>" +
                "</thead>");

            strOrder.AppendLine("<tr>");
            strOrder.AppendLine("<td>" + OrderObj.OrderStatusName + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.OrderNumber + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.OrderDate + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.NumbOfBlinds + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.Company + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.Reference + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.ConsignNoteNum + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.CompleteDate + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.DeliveryDate + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.DepartureDate + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.ArrivalDate + "</td>");
            strOrder.AppendLine("<td>" + OrderObj.OrderM2 + "</td>");
            strOrder.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetOrder/" + OrderObj.OrderID + "</td>");
            strOrder.AppendLine("</tr>");

            #endregion

            #region Order Details
            List<OrderDetail> OrderDetailsObj = GetOrderDetails(OrderObj.OrderID);

            switch (OrderObj.OrderTypeID)
            {
                case 1:
                    //Venetian
                    strOrderDetails.AppendLine("<table style='width: 100%'>" +
                "<thead>" +
                "<tr style = 'white-space: nowrap;'>" +
                "<th>Slat Style</th>" +
                "<th>Cord Style</th>" +
                "<th>Make Width</th>" +
                "<th>Special Pelmet Width</th>" +
                "<th>Mount</th>" +
                "<th>Return Required</th>" +
                "<th>Make Height</th>" +
                "<th>Ready Made Size</th>" +
                "<th>Width Made By</th>" +
                "<th>Height Made By</th>" +
                "<th>Quality Check By</th>" +
                "<th>Square Meter</th>" +
                "<th>Edit</th>" +
                "</tr>" +
                "</thead>");
                    foreach (var item in OrderDetailsObj)
                    {
                        strOrderDetails.AppendLine("<tr>");
                        strOrderDetails.AppendLine("<td>" + GetSlatStyle(item.SlatStyleID).SlatStyleDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetCordStyle(item.CordStyleID).CordStyleDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Width + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SplPelmetWidth + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.MountType + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Height + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ReadyMadeSize + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.WidthMadeBy + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.HeightMadeBy + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.QualityCheckedBy + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SquareMeter + "</td>");
                        strOrderDetails.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetOrderDetail/" + OrderObj.OrderID + "</td>");
                        strOrderDetails.AppendLine("</tr>");
                    }
                    break;
                case 2:
                    //Roller
                    strOrderDetails.AppendLine("<table style='width: 100%'>" +
                "<thead>" +
                "<tr style = 'white-space: nowrap;'>" +
                "<th>Width</th>" +
                "<th>Height</th>" +
                "<th>Material</th>" +
                "<th>ColorsWidth</th>" +
                "<th>Control Style</th>" +
                "<th>Mount Type</th>" +
                "<th>Square Meter</th>" +
                "<th>Edit</th>" +
                "</tr>" +
                "</thead>");
                    foreach (var item in OrderDetailsObj)
                    {
                        strOrderDetails.AppendLine("<tr>");
                        strOrderDetails.AppendLine("<td>" + item.Width + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Height + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetMaterial(item.MaterialID).MaterialDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ControlStyle + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.MountType + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SquareMeter + "</td>");
                        strOrderDetails.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetOrderDetail/" + OrderObj.OrderID + "</td>");
                        strOrderDetails.AppendLine("</tr>");
                    }
                    break;
                case 3:
                    //Vertical(Oversea)
                    strOrderDetails.AppendLine("<table style='width: 100%'>" +
                "<thead>" +
                "<tr style = 'white-space: nowrap;'>" +
                "<th>Material</th>" +
                "<th>Color</th>" +
                "<th>Control</th>" +
                "<th>Control Style</th>" +
                "<th>Opening Style</th>" +
                "<th>Pelmet Style</th>" +
                "<th>Mount Type</th>" +
                "<th>Width</th>" +
                "<th>Height</th>" +
                "<th>Ready Made Size</th>" +
                "<th>Square Meter</th>" +
                "<th>Edit</th>" +
                "</tr>" +
                "</thead>");
                    foreach (var item in OrderDetailsObj)
                    {
                        strOrderDetails.AppendLine("<tr>");
                        strOrderDetails.AppendLine("<td>" + GetMaterial(item.MaterialID).MaterialDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetControl(item.ControlID).ControlDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ControlStyle + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.OpeningStyle + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.PelmetStyle + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.MountType + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Width + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Height + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ReadyMadeSize + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SquareMeter + "</td>");
                        strOrderDetails.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetOrderDetail/" + OrderObj.OrderID + "</td>");
                        strOrderDetails.AppendLine("</tr>");
                    }
                    break;
                case 4:
                    //Venetian(Oversea)
                    strOrderDetails.AppendLine("<table style='width: 100%'>" +
                    "<thead>" +
                    "<tr style = 'white-space: nowrap;'>" +
                    "<th>Material</th>" +
                    "<th>Color</th>" +
                    "<th>Control</th>" +
                    "<th>Mount</th>" +
                    "<th>Make Width</th>" +
                    "<th>Make Height</th>" +
                    "<th>Special Pelmet Width</th>" +
                    "<th>Cord Style</th>" +
                    "<th>Return Required</th>" +
                    "<th>Ready Made Size</th>" +
                    "<th>Square Meter</th>" +
                    "<th>Edit</th>" +
                    "</tr>" +
                    "</thead>");
                    foreach (var item in OrderDetailsObj)
                    {
                        strOrderDetails.AppendLine("<tr>");
                        strOrderDetails.AppendLine("<td>" + GetMaterial(item.MaterialID).MaterialDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetControl(item.ControlID).ControlDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.MountType + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Width + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Height + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SplPelmetWidth + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetCordStyle(item.CordStyleID).CordStyleDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + (item.ReturnRequired ? "True" : "False") + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ReadyMadeSize + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SquareMeter + "</td>");
                        strOrderDetails.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetOrderDetail/" + OrderObj.OrderID + "</td>");
                        strOrderDetails.AppendLine("</tr>");
                    }
                    break;
                case 5:
                    //Honeycomb(Oversea)
                    strOrderDetails.AppendLine("<table style='width: 100%'>" +
                    "<thead>" +
                    "<tr style = 'white-space: nowrap;'>" +
                    "<th>Material</th>" +
                    "<th>Color</th>" +
                    "<th>Control</th>" +
                    "<th>Mount</th>" +
                    "<th>Make Width</th>" +
                    "<th>Make Height</th>" +
                    "<th>Ready Made Size</th>" +
                    "<th>Square Meter</th>" +
                    "<th>Edit</th>" +
                    "</tr>" +
                    "</thead>");
                    foreach (var item in OrderDetailsObj)
                    {
                        strOrderDetails.AppendLine("<tr>");
                        strOrderDetails.AppendLine("<td>" + GetMaterial(item.MaterialID).MaterialDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetControl(item.ControlID).ControlDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.MountType + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Width + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Height + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ReadyMadeSize + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SquareMeter + "</td>");
                        strOrderDetails.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetOrderDetail/" + OrderObj.OrderID + "</td>");
                        strOrderDetails.AppendLine("</tr>");
                    }
                    break;
                case 6:
                    //Roman(Oversea)
                    strOrderDetails.AppendLine("<table style='width: 100%'>" +
                    "<thead>" +
                    "<tr style = 'white-space: nowrap;'>" +
                    "<th>Material</th>" +
                    "<th>Color</th>" +
                    "<th>Control</th>" +
                    "<th>Control Style</th>" +
                    "<th>Mount</th>" +
                    "<th>Make Width</th>" +
                    "<th>Make Height</th>" +
                    "<th>Ready Made Size</th>" +
                    "<th>Square Meter</th>" +
                    "<th>Edit</th>" +
                    "</tr>" +
                    "</thead>");
                    foreach (var item in OrderDetailsObj)
                    {
                        strOrderDetails.AppendLine("<tr>");
                        strOrderDetails.AppendLine("<td>" + GetMaterial(item.MaterialID).MaterialDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetControl(item.ControlID).ControlDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ControlStyle + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.MountType + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Width + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Height + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ReadyMadeSize + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SquareMeter + "</td>");
                        strOrderDetails.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetOrderDetail/" + OrderObj.OrderID + "</td>");
                        strOrderDetails.AppendLine("</tr>");
                    }
                    break;
                case 7:
                    //Roller(Oversea)
                    strOrderDetails.AppendLine("<table style='width: 100%'>" +
                    "<thead>" +
                    "<tr style = 'white-space: nowrap;'>" +
                    "<th>Material</th>" +
                    "<th>Color</th>" +
                    "<th>Control</th>" +
                    "<th>Control Style</th>" +
                    "<th>Roll</th>" +
                    "<th>Mount</th>" +
                    "<th>Make Width</th>" +
                    "<th>Make Height</th>" +
                    "<th>Ready Made Size</th>" +
                    "<th>Square Meter</th>" +
                    "<th>Edit</th>" +
                    "</tr>" +
                    "</thead>");
                    foreach (var item in OrderDetailsObj)
                    {
                        strOrderDetails.AppendLine("<tr>");
                        strOrderDetails.AppendLine("<td>" + GetMaterial(item.MaterialID).MaterialDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetControl(item.ControlID).ControlDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ControlStyle + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Roll + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.MountType + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Width + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Height + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ReadyMadeSize + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SquareMeter + "</td>");
                        strOrderDetails.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetOrderDetail/" + OrderObj.OrderID + "</td>");
                        strOrderDetails.AppendLine("</tr>");
                    }
                    break;
                case 8:
                    //Zebra(Oversea)
                    strOrderDetails.AppendLine("<table style='width: 100%'>" +
                    "<thead>" +
                    "<tr style = 'white-space: nowrap;'>" +
                    "<th>Material</th>" +
                    "<th>Color</th>" +
                    "<th>Control</th>" +
                    "<th>Control Style</th>" +
                    "<th>Mount</th>" +
                    "<th>Make Width</th>" +
                    "<th>Make Height</th>" +
                    "<th>Ready Made Size</th>" +
                    "<th>Square Meter</th>" +
                    "<th>Edit</th>" +
                    "</tr>" +
                    "</thead>");
                    foreach (var item in OrderDetailsObj)
                    {
                        strOrderDetails.AppendLine("<tr>");
                        strOrderDetails.AppendLine("<td>" + GetMaterial(item.MaterialID).MaterialDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + GetControl(item.ControlID).ControlDesc + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ControlStyle + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.MountType + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Width + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.Height + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.ReadyMadeSize + "</td>");
                        strOrderDetails.AppendLine("<td>" + item.SquareMeter + "</td>");
                        strOrderDetails.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetOrderDetail/" + OrderObj.OrderID + "</td>");
                        strOrderDetails.AppendLine("</tr>");
                    }
                    break;
                default:
                    break;
            }

            #endregion

            strBody.AppendLine("THIS IS AN AUTO-GENERATED MAIL. DO NOT REPLY.");
            strBody.AppendLine("<br/>");
            strBody.AppendLine("Order Information <br/>");
            strBody.AppendLine(strOrder.ToString());
            strBody.AppendLine("<br/>");
            strBody.AppendLine("Item Information <br/>");
            strBody.AppendLine(strOrderDetails.ToString());
            strBody.AppendLine("<br/>");
            strBody.AppendLine("Notes");
            strBody.AppendLine(OrderObj.Notes);

            return strBody.ToString();
        }

        public string CreateUtilityOrderMailBody(UtilityOrder UtilityOrderObj)
        {
            StringBuilder strBody = new StringBuilder();
            StringBuilder strUtilityOrder = new StringBuilder();
            StringBuilder strUtilityOrderDetails = new StringBuilder();

            #region Utility Order Part
            strUtilityOrder.AppendLine("<table style='width: 100%'> " +
                "<thead> " +
                "<tr style = 'white-space: nowrap;'>" +
                "<th>Order Type </th>" +
                "<th>Order Number </th>" +
                "<th>Order Date </th>" +
                "<th>Complete Date </th>" +
                "</tr>" +
                "</thead>");

            strUtilityOrder.AppendLine("<tr>");
            strUtilityOrder.AppendLine("<td>" + UtilityOrderObj.OrderTypeName + "</td>");
            strUtilityOrder.AppendLine("<td>" + UtilityOrderObj.UtilityOrderNumber + "</td>");
            strUtilityOrder.AppendLine("<td>" + UtilityOrderObj.OrderDate + "</td>");
            strUtilityOrder.AppendLine("<td>" + UtilityOrderObj.CompleteDate + "</td>");
            strUtilityOrder.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetUtilityOrder/" + UtilityOrderObj.UtilityOrderID + "</td>");
            strUtilityOrder.AppendLine("</tr>");
            #endregion

            #region Utility Order Details

            switch (UtilityOrderObj.OrderTypeID)
            {

                case 9:
                    //Faric
                    List<Fabric> FabricDetailsObj = GetFabrics(UtilityOrderObj.UtilityOrderID);
                    strUtilityOrder.AppendLine("<table style='width: 100%'>" +
                "<thead>" +
                "<tr style = 'white-space: nowrap;'>" +
                "<th>Fabric Type</th>" +
                "<th>Fabric Color</th>" +
                "<th>Faric Size</th>" +
                "<th>Boxes</th>" +
                "<th>Edit</th>" +
                "</tr>" +
                "</thead>");
                    foreach (var item in FabricDetailsObj)
                    {
                        strUtilityOrder.AppendLine("<tr>");
                        strUtilityOrder.AppendLine("<td>" + item.FabricType + "</td>");
                        strUtilityOrder.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strUtilityOrder.AppendLine("<td>" + GetSize(item.SizeID).SizeDesc + "</td>");
                        strUtilityOrder.AppendLine("<td>" + item.Boxes + "</td>");
                        strUtilityOrder.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetFabric/" + item.FabricID + "</td>");
                        strUtilityOrder.AppendLine("</tr>");
                    }
                    break;
                case 10:
                    //Roller Blinds
                    List<RollerBlinds> RollerDetailsObj = GetRollerBlinds(UtilityOrderObj.UtilityOrderID);
                    strUtilityOrder.AppendLine("<table style='width: 100%'>" +
                "<thead>" +
                "<tr style = 'white-space: nowrap;'>" +
                "<th>Profile</th>" +
                "<th>Description</th>" +
                "<th>Color</th>" +
                "<th>DLX CODE</th>" +
                "<th>PCS/CTN</th>" +
                "<th>MOQ</th>" +
                "<th>CNTS</th>" +
                "<th>Edit</th>" +
                "</tr>" +
                "</thead>");
                    foreach (var item in RollerDetailsObj)
                    {
                        RollerBlindType TypeDetails = GetRollerBlindType(item.RollerBlindTypeID);
                        strUtilityOrder.AppendLine("<tr>");
                        strUtilityOrder.AppendLine("<td>" + TypeDetails.Profile + "</td>");
                        strUtilityOrder.AppendLine("<td>" + TypeDetails.Description + "</td>");
                        strUtilityOrder.AppendLine("<td>" + TypeDetails.RollerColor + "</td>");
                        strUtilityOrder.AppendLine("<td>" + TypeDetails.DLXCODE + "</td>");
                        strUtilityOrder.AppendLine("<td>" + TypeDetails.PCSCTN + "</td>");
                        strUtilityOrder.AppendLine("<td>" + TypeDetails.MOQ + "</td>");
                        strUtilityOrder.AppendLine("<td>" + item.Boxes + "</td>");
                        strUtilityOrder.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetRollerBlinds/" + item.RollerBlindsID + "</td>");
                        strUtilityOrder.AppendLine("</tr>");
                    }
                    break;
                case 11:
                    //Valance
                    List<Valance> ValalnceDetailsObj = GetValances(UtilityOrderObj.UtilityOrderID);
                    strUtilityOrder.AppendLine("<table style='width: 100%'>" +
                "<thead>" +
                "<tr style = 'white-space: nowrap;'>" +
                "<th>Material</th>" +
                "<th>Color</th>" +
                "<th>Size</th>" +
                "<th>Boxes</th>" +
                "<th>Edit</th>" +
                "</tr>" +
                "</thead>");
                    foreach (var item in ValalnceDetailsObj)
                    {
                        strUtilityOrder.AppendLine("<tr>");
                        strUtilityOrder.AppendLine("<td>" + GetMaterial(item.MaterialID).MaterialDesc + "</td>");
                        strUtilityOrder.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strUtilityOrder.AppendLine("<td>" + item.SizeID + "</td>");
                        strUtilityOrder.AppendLine("<td>" + item.Boxes + "</td>");
                        strUtilityOrder.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetValance/" + item.ValanceID + "</td>");
                        strUtilityOrder.AppendLine("</tr>");
                    }
                    break;
                case 12:
                    //Bottom Rail
                    List<BottomRail> BottomRailDetailsObj = GetBottomRails(UtilityOrderObj.UtilityOrderID);
                    strUtilityOrder.AppendLine("<table style='width: 100%'>" +
                "<thead>" +
                "<tr style = 'white-space: nowrap;'>" +
                "<th>Material</th>" +
                "<th>Color</th>" +
                "<th>Size</th>" +
                "<th>Boxes</th>" +
                "<th>Edit</th>" +
                "</tr>" +
                "</thead>");
                    foreach (var item in BottomRailDetailsObj)
                    {
                        strUtilityOrder.AppendLine("<tr>");
                        strUtilityOrder.AppendLine("<td>" + GetMaterial(item.MaterialID).MaterialDesc + "</td>");
                        strUtilityOrder.AppendLine("<td>" + GetColors(item.ColorID).ColorsDesc + "</td>");
                        strUtilityOrder.AppendLine("<td>" + item.SizeID + "</td>");
                        strUtilityOrder.AppendLine("<td>" + item.Boxes + "</td>");
                        strUtilityOrder.AppendLine("<td>" + ConfigurationManager.AppSettings["WebLink"].ToString() + "/GetValance/" + item.BottomRailID + "</td>");
                        strUtilityOrder.AppendLine("</tr>");
                    }
                    break;
            }
            #endregion

            strBody.AppendLine("THIS IS AN AUTO-GENERATED MAIL. DO NOT REPLY.");
            strBody.AppendLine("<br/>");
            strBody.AppendLine("Order Information <br/>");
            strBody.AppendLine(strUtilityOrder.ToString());
            strBody.AppendLine("<br/>");
            strBody.AppendLine("Item Information <br/>");
            strBody.AppendLine(strUtilityOrderDetails.ToString());

            return strBody.ToString();

        }
        #endregion
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
