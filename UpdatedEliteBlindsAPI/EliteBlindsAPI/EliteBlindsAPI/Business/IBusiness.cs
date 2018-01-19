using EliteBlindsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlindsAPI.Business
{
    interface IBusiness
    {
        Customer LoginCheck(string Email, string Password);
        bool SetUserActive(int ID);
        bool ForgotPassword(string Email);
        bool ResetPassword(string Email,string Password);
        Customer GetCustomer(int ID);
        List<Customer> GetCustomers();
        Customer SaveCustomer(Customer CustData);
        void DeleteCustomer(int ID);
        Order SaveOrder(Order OrderData);
        List<OrderDetail> SaveOrderDetails(List<OrderDetail> OrderDetailData);
        UtilityOrder SaveUtilityOrder(UtilityOrder OrderData);
        void DeleteOrder(int ID);
        void DeleteUtilityOrder(int ID);
        Order GetOrder(int ID);
        List<Order> GetCustOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy);
        UtilityOrder GetUtilityOrder(int ID);
        List<UtilityOrder> GetUtilityOrders(int ID);
        List<UtilityOrder> GetCustomerUtilityOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy);
        Tuple<List<Order>, List<UtilityOrder>> GetAllCustomerOrders(int ID);
        Boolean ApproveOrders(List<int> OrderIDs);
        Boolean ApproveUtilityOrders(List<int> OrderIDs);
        Boolean ChangeOrderStatus(List<int> OrderIDs,int StatusID);
        Boolean ChangeUtilityOrderStatus(List<int> OrderIDs,int StatusID);
        OrderDetail GetOrderDetail(int ID);
        Fabric GetFabric(int ID);
        List<Fabric> SaveFabric(List<Fabric> FabricData);
        void DeleteFabric(int ID);
        RollerBlinds GetRollerBlind(int ID);
        RollerBlindType GetRollerBlindType(int ID);
        List<RollerBlinds> SaveRollerBlinds(List<RollerBlinds> RollerBlindsData);
        void DeleteRollerBlinds(int ID);
        Valance GetValance(int ID);
        List<Valance> SaveValance(List<Valance> ValanceData);
        void DeleteValance(int ID);
        BottomRail GetBottomRail(int ID);
        List<BottomRail> SaveBottomRail(List<BottomRail> BottomRailData);
        void DeleteBottomRail(int ID);
        List<OrderDetail> GetOrderDetails(int ID);
        List<OrderDetail> GetOrderDetails(List<int> IDs);
        List<Colors> GetColors();
        List<SlatStyle> GetSlatStyle();
        List<CordStyle> GetCordStyle();
        List<Control> GetControl();
        List<Material> GetMaterial();
        List<Size> GetSize();
        List<Colors> GetColors(string For);
        List<SlatStyle> GetSlatStyle(string For);
        List<CordStyle> GetCordStyle(string For);
        List<Control> GetControl(string For);
        List<Material> GetMaterial(string For);
        List<Size> GetSize(string For);
        List<Fabric> GetFabric();
        List<BlindType> GetBlindType();
        List<OrderStatus> GetOrderStatus();
        List<OrderType> GetOrderType();
        List<EliteRoles> GetRoles();
        List<RollerBlinds> GetRollerBlinds();
        List<Valance> GetValance();
        List<BottomRail> GetBottomRail();
        List<Fabric> GetFabrics(int ID);
        List<RollerBlinds> GetRollerBlinds(int ID);
        List<Valance> GetValances(int ID);
        List<BottomRail> GetBottomRails(int ID);
        void SendMail(string to, string body, string subject, params MailAttachment[] attachments);

    }
}
