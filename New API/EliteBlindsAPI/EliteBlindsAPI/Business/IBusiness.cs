using EliteBlindsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlindsAPI.Business
{
    interface IBusiness
    {
        bool LoginCheck(string Email, string Password);
        bool SetUserActive(int ID);
        Customer GetCustomer(int ID);
        List<Customer> GetCustomers();
        void SaveCustomer(Customer CustData);
        void DeleteCustomer(int ID);
        void SaveOrder(Order OrderData);
        void SaveUtilityOrder(UtilityOrder OrderData);
        void DeleteOrder(int ID);
        void DeleteUtilityOrder(int ID);
        Order GetOrder(int ID);
        List<Order> GetCustOrders(int ID);
        UtilityOrder GetUtilityOrder(int ID);
        List<UtilityOrder> GetUtilityOrders(int ID);
        List<UtilityOrder> GetCustomerUtilityOrders(int ID);
        Tuple<List<Order>, List<UtilityOrder>> GetAllCustomerOrders(int ID);

        OrderDetail GetOrderDetail(int ID);
        Fabric GetFabric(int ID);
        void SaveFabric(Fabric FabricData);
        void DeleteFabric(int ID);
        RollerBlinds GetRollerBlind(int ID);
        void SaveRollerBlinds(RollerBlinds RollerBlindsData);
        void DeleteRollerBlinds(int ID);
        Valance GetValance(int ID);
        void SaveValance(Valance ValanceData);
        void DeleteValance(int ID);
        BottomRail GetBottomRail(int ID);
        void SaveBottomRail(BottomRail BottomRailData);
        void DeleteBottomRail(int ID);
        List<OrderDetail> GetOrderDetails(int ID);
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
