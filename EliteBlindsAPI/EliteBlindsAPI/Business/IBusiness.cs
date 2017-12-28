using EliteBlindsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlindsAPI.Business
{
    interface IBusiness
    {
        Customer GetCustomer(int ID);
        Customer GetCustomer(string Name);
        bool SaveCustomer(Customer CustData);
        bool UpdateCustomer(Customer CustData);
        bool DeleteCustomer(int ID);
        Order GetOrder(int ID);
        List<Order> GetCustOrders(int ID);
        OrderDetail GetOrderDetail(int ID);
        Fabric GetFabric(int ID);
        RollerBlinds GetRollerBlinds(int ID);
        Valance GetValance(int ID);
        BottomRail GetBottomRail(int ID);
        List<OrderDetail> GetOrderDetails(int ID);
        List<Colors> GetColors();
        List<SlatStyle> GetSlatStyle();
        List<CordStyle> GetCordStyle();
        List<Control> GetControl();
        List<Material> GetMaterial();
        List<Fabric> GetFabric();
        List<RollerBlinds> GetRollerBlinds();
        List<Valance> GetValance();
        List<BottomRail> GetBottomRail();
    }
}
