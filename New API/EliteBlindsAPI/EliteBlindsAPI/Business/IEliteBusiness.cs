using EliteBlindsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlindsAPI.Business
{
    interface IEliteBusiness
    {
        bool LoginCheck(string Email, string Password);
        bool UserCheck(string Email);
        bool ForgotPassword(string Email);
        bool ResetPassword(string Email, string Password);
        Customer GetCustomer(int CustomerID);
        List<Customer> GetCustomers();
        bool SaveCustomer(Customer CustData);
        bool UpdateCustomer(Customer CustData);
        bool DeleteCustomer(int CustomerID);
        bool SetUserActive(int ID);

        Order GetOrder(int OrderID);
        bool SaveOrder(Order OrderData);
        bool UpdateOrder(Order OrderData);
        bool SaveOrderDetail(OrderDetail OrderDetailData);
        bool UpdateOrderDetail(OrderDetail OrderDetailData);
        List<Order> GetCustomerOrders(int CustomerID);
        OrderDetail GetOrderDetail(int OrderDetailID);
        List<OrderDetail> GetOrderDetails();
        List<OrderDetail> GetOrderDetails(int OrderID);
        bool DeleteOrder(int OrderID);

        UtilityOrder GetUtilityOrder(int UtilityOrderID);
        bool SaveUtilityOrder(UtilityOrder UtilityOrderData);
        bool UpdateUtilityOrder(UtilityOrder UtilityOrderData);
        List<UtilityOrder> GetCustomerUtilityOrders(int CustomerID);
        bool DeleteUtilityOrder(int UtilityOrderID);

        Fabric GetFabric(int FabricID);
        bool SaveFabric(Fabric FabricData);
        bool UpdateFabric(Fabric FabricData);
        bool DeleteFabric(int FabricID);
        List<Fabric> GetFabric();
        List<Fabric> GetFabrics(int UtilityOrderID);

        RollerBlinds GetRollerBlind(int RollerBlindsID);
        bool SaveRollerBlinds(RollerBlinds RollerBlindsData);
        bool UpdateRollerBlinds(RollerBlinds RollerBlindsData);
        bool DeleteRollerBlinds(int RollerBlindsID);
        List<RollerBlinds> GetRollerBlinds();
        List<RollerBlinds> GetRollerBlinds(int UtilityOrderID);

        Valance GetValance(int ValanceID);
        bool SaveValance(Valance ValanceData);
        bool UpdateValance(Valance ValanceData);
        bool DeleteValance(int ValanceID);
        List<Valance> GetValance();
        List<Valance> GetValances(int UtilityOrderID);

        BottomRail GetBottomRail(int BottomRailID);
        bool SaveBottomRail(BottomRail BottomRailData);
        bool UpdateBottomRail(BottomRail BottomRailData);
        bool DeleteBottomRail(int BottomRailID);
        List<BottomRail> GetBottomRail();
        List<BottomRail> GetBottomRails(int UtilityOrderID);

        List<Colors> GetColors();
        List<SlatStyle> GetSlatStyle();
        List<CordStyle> GetCordStyle();
        List<Control> GetControl();
        List<Material> GetMaterial();
        List<BlindType> GetBlindType();
        List<Size> GetSize();
        List<Colors> GetColors(string For);
        List<SlatStyle> GetSlatStyle(string For);
        List<CordStyle> GetCordStyle(string For);
        List<Control> GetControl(string For);
        List<Material> GetMaterial(string For);
        List<Size> GetSize(string For);
    }
}
