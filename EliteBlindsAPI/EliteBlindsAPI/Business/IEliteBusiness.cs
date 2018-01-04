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

        Customer GetCustomer(int CustomerID);
        List<Customer> GetCustomers();
        bool SaveCustomer(Customer CustData);
        bool UpdateCustomer(Customer CustData);
        bool DeleteCustomer(int CustomerID);

        Order GetOrder(int OrderID);
        List<Order> GetOrders(int CustomerID);
        OrderDetail GetOrderDetail(int OrderDetailID);
        List<OrderDetail> GetOrderDetails();
        List<OrderDetail> GetOrderDetails(int OrderID);
        bool DeleteOrder(int OrderID);

        UtilityOrder GetUtilityOrder(int UtilityOrderID);
        List<UtilityOrder> GetUtilityOrders(int CustomerID);
        bool DeleteUtilityOrder(int UtilityOrderID);

        Fabric GetFabric(int FabricID);
        List<Fabric> GetFabric();

        RollerBlinds GetRollerBlinds(int RollerBlindsID);
        List<RollerBlinds> GetRollerBlinds();

        Valance GetValance(int ValanceID);
        List<Valance> GetValance();

        BottomRail GetBottomRail(int BottomRailID);
        List<BottomRail> GetBottomRail();

        List<Colors> GetColors();
        List<SlatStyle> GetSlatStyle();
        List<CordStyle> GetCordStyle();
        List<Control> GetControl();
        List<Material> GetMaterial();
    }
}
