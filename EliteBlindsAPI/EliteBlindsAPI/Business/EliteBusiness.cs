using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EliteBlindsAPI.Models;

namespace EliteBlindsAPI.Business
{
    public class EliteBusiness : IEliteBusiness
    {
        private Customer_Mapper CustDB;
        private Order_Mapper OrderDB;
        private OrderDetail_Mapper OrderDetailDB;
        private UtilityOrder_Mapper UtilityOrderDB;
        private Fabric_Mapper FabricDB;
        private RollerBlinds_Mapper RollerBlindsDB;
        private RollerBlindType_Mapper RollerBlindTypeDB;
        private Valance_Mapper ValanceDB;
        private BottomRail_Mapper BottomRailDB;
        private SlatStyle_Mapper SlatStyleDB;
        private CordStyle_Mapper CordStyleDB;
        private Control_Mapper ControlDB;
        private Material_Mapper MaterialDB;
        private Colors_Mapper ColorsDB;
        private Exception CustomException;
        public EliteBusiness()
        {
            CustDB = new Customer_Mapper();
            OrderDB = new Order_Mapper();
            OrderDetailDB = new OrderDetail_Mapper();
            UtilityOrderDB = new UtilityOrder_Mapper();
            FabricDB = new Fabric_Mapper();
            RollerBlindsDB = new RollerBlinds_Mapper();
            RollerBlindTypeDB = new RollerBlindType_Mapper();
            ValanceDB = new Valance_Mapper();
            BottomRailDB = new BottomRail_Mapper();
            SlatStyleDB = new SlatStyle_Mapper();
            CordStyleDB = new CordStyle_Mapper();
            ControlDB = new Control_Mapper();
            MaterialDB = new Material_Mapper();
            ColorsDB = new Colors_Mapper();
            CustomException = new Exception();
        }

        //Customer Related
        public bool LoginCheck(string Email, string Password)
        {
            return CustDB.LoginCheck(Email, Password, out CustomException);
        }

        public bool SaveCustomer(Customer CustData)
        {
            return CustDB.Create(CustData, out CustomException);
        }

        public bool UpdateCustomer(Customer CustData)
        {
            return CustDB.Update(CustData, out CustomException);
        }
        public Customer GetCustomer(int CustomerID)
        {
            return CustDB.Select(CustomerID, out CustomException);
        }

        public List<Customer> GetCustomers()
        {
            return CustDB.SelectAll(out CustomException);
        }
        public bool DeleteCustomer(int CustomerID)
        {
            return CustDB.Delete(CustomerID, out CustomException);
        }

        //Order Related

        public Order GetOrder(int OrderID)
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetOrderDetail(int OrderDetailID)
        {
            return OrderDetailDB.Select(OrderDetailID, out CustomException);
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return OrderDetailDB.SelectAll(out CustomException);
        }

        public List<OrderDetail> GetOrderDetails(int OrderID)
        {
            return OrderDetailDB.SelectAll(out CustomException);
        }

        public List<Order> GetOrders(int CustomerID)
        {
            return OrderDB.SelectAll(out CustomException);
        }

        public UtilityOrder GetUtilityOrder(int UtilityOrderID)
        {
            return UtilityOrderDB.Select(UtilityOrderID, out CustomException);
        }

        public List<UtilityOrder> GetUtilityOrders(int CustomerID)
        {
            return UtilityOrderDB.SelectAll(out CustomException);
        }

        public bool DeleteOrder(int OrderID)
        {
            return OrderDB.Delete(OrderID, out CustomException);
        }

        public bool DeleteUtilityOrder(int UtilityOrderID)
        {
            return UtilityOrderDB.Delete(UtilityOrderID, out CustomException);
        }


        //Other Details
        public BottomRail GetBottomRail(int BottomRailID)
        {
            return BottomRailDB.Select(BottomRailID, out CustomException);
        }

        public List<BottomRail> GetBottomRail()
        {
            return BottomRailDB.SelectAll(out CustomException);
        }

        public List<Colors> GetColors()
        {
            return ColorsDB.SelectAll(out CustomException);
        }

        public List<Control> GetControl()
        {
            return ControlDB.SelectAll(out CustomException);
        }

        public List<CordStyle> GetCordStyle()
        {
            return CordStyleDB.SelectAll(out CustomException);
        }

        public Fabric GetFabric(int FabricID)
        {
            return FabricDB.Select(FabricID, out CustomException);
        }

        public List<Fabric> GetFabric()
        {
            return FabricDB.SelectAll(out CustomException);
        }

        public List<Material> GetMaterial()
        {
            return MaterialDB.SelectAll(out CustomException);
        }
        public RollerBlinds GetRollerBlinds(int RollerBlindsID)
        {
            return RollerBlindsDB.Select(RollerBlindsID, out CustomException);
        }

        public List<RollerBlinds> GetRollerBlinds()
        {
            return RollerBlindsDB.SelectAll(out CustomException);
        }

        public List<SlatStyle> GetSlatStyle()
        {
            return SlatStyleDB.SelectAll(out CustomException);
        }

        public Valance GetValance(int ValanceID)
        {
            return ValanceDB.Select(ValanceID, out CustomException);
        }

        public List<Valance> GetValance()
        {
            return ValanceDB.SelectAll(out CustomException);
        }

    }
}
