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
        private BlindType_Mapper BlindTypeDB;
        private Size_Mapper SizeDB;
        private OrderType_Mapper OrderTypeDB;
        private OrderStatus_Mapper OrderStatusDB;
        private EliteRoles_Mapper EliteRolesDB;
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
            BlindTypeDB = new BlindType_Mapper();
            SizeDB = new Size_Mapper();
            OrderStatusDB = new OrderStatus_Mapper();
            OrderTypeDB = new OrderType_Mapper();
            EliteRolesDB = new EliteRoles_Mapper();
            CustomException = new Exception();
        }

        //Customer Related
        public Customer LoginCheck(string Email, string Password)
        {
            return CustDB.LoginCheck(Email, Password, out CustomException);
        }

        public bool ForgotPassword(string Email)
        {
            return CustDB.ForgotPassword(Email, out CustomException);
        }

        public bool ResetPassword(string Email, string Password)
        {
            return CustDB.ResetPassword(Email, Password, out CustomException);
        }

        public bool UserCheck(string Email)
        {
            return CustDB.UserCheck(Email, out CustomException);
        }

        public bool SetUserActive(int ID)
        {
            return CustDB.SetUserActive(ID, out CustomException);
        }

        public Customer SaveCustomer(Customer CustData)
        {
            return CustDB.Create(CustData, out CustomException);
        }

        public Customer UpdateCustomer(Customer CustData)
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
            return OrderDB.Select(OrderID, out CustomException);
        }

        public List<Order> GetAllOrders()
        {
            return OrderDB.SelectAll(out CustomException);
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
            return OrderDetailDB.SelectedOrderID(new List<int>() { OrderID }, out CustomException);
        }

        public List<OrderDetail> GetOrderDetails(List<int> OrderIDs)
        {
            return OrderDetailDB.SelectedOrderID(OrderIDs, out CustomException);
        }

        public List<Order> GetCustomerOrders(int CustomerID)
        {
            return OrderDB.SelectedCustomer(CustomerID, out CustomException);
        }

        public UtilityOrder GetUtilityOrder(int UtilityOrderID)
        {
            return UtilityOrderDB.Select(UtilityOrderID, out CustomException);
        }
        
        public List<UtilityOrder> GetCustomerUtilityOrders(int CustomerID)
        {
            return UtilityOrderDB.SelectedCustomer(CustomerID, out CustomException);
        }

        public List<UtilityOrder> GetAllUtilityOrders()
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

        public Boolean ApproveOrders(List<int> OrderIDs)
        {
            return OrderDB.ApproveOrders(OrderIDs, out CustomException);
        }

        public Boolean ApproveUtilityOrders(List<int> OrderIDs)
        {
            return UtilityOrderDB.ApproveUtilityOrders(OrderIDs, out CustomException);
        }

        public List<Order> GetCustomerOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy)
        {
            return OrderDB.GetCustomerOrders(CustId, FilterBy,SearchCriteria, OrderBy, out CustomException);
        }

        public List<UtilityOrder> GetCustomerUtilityOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy)
        {
            return UtilityOrderDB.GetCustomerUtilityOrders(CustId, FilterBy,SearchCriteria, OrderBy, out CustomException);
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

        public List<BottomRail> GetBottomRails(int UtilityOrderID)
        {
            return BottomRailDB.SelectAll(UtilityOrderID, out CustomException);
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

        public List<Material> GetMaterial()
        {
            return MaterialDB.SelectAll(out CustomException);
        }

        public List<SlatStyle> GetSlatStyle()
        {
            return SlatStyleDB.SelectAll(out CustomException);
        }

        public List<Size> GetSize()
        {
            return SizeDB.SelectAll(out CustomException);
        }

        public List<Colors> GetColors(string For)
        {
            return ColorsDB.SelectFor(For, out CustomException);
        }

        public List<Control> GetControl(string For)
        {
            return ControlDB.SelectFor(For, out CustomException);
        }

        public List<CordStyle> GetCordStyle(string For)
        {
            return CordStyleDB.SelectFor(For, out CustomException);
        }

        public List<Material> GetMaterial(string For)
        {
            return MaterialDB.SelectFor(For, out CustomException);
        }

        public List<SlatStyle> GetSlatStyle(string For)
        {
            return SlatStyleDB.SelectFor(For, out CustomException);
        }

        public List<Size> GetSize(string For)
        {
            return SizeDB.SelectFor(For, out CustomException);
        }

        public List<BlindType> GetBlindType()
        {
            return BlindTypeDB.SelectAll(out CustomException);
        }

        public List<OrderStatus> GetOrderStatus()
        {
            return OrderStatusDB.SelectAll(out CustomException);
        }

        public List<OrderType> GetOrderType()
        {
            return OrderTypeDB.SelectAll(out CustomException);
        }

        public List<EliteRoles> GetRoles()
        {
            return EliteRolesDB.SelectAll(out CustomException);
        }

        public Colors GetColors(int ID)
        {
            return ColorsDB.Select(ID,out CustomException);
        }
        public SlatStyle GetSlatStyle(int ID)
        {
            return SlatStyleDB.Select(ID, out CustomException);
        }
        public CordStyle GetCordStyle(int ID)
        {
            return CordStyleDB.Select(ID, out CustomException);
        }
        public Control GetControl(int ID)
        {
            return ControlDB.Select(ID, out CustomException);
        }
        public Material GetMaterial(int ID)
        {
            return MaterialDB.Select(ID, out CustomException);
        }
        public BlindType GetBlindType(int ID)
        {
            return BlindTypeDB.Select(ID, out CustomException);
        }
        public OrderStatus GetOrderStatus(int ID)
        {
            return OrderStatusDB.Select(ID, out CustomException);
        }
        public OrderType GetOrderType(int ID)
        {
            return OrderTypeDB.Select(ID, out CustomException);
        }
        public EliteRoles GetRoles(int ID)
        {
            return EliteRolesDB.Select(ID, out CustomException);
        }
        public Size GetSize(int ID)
        {
            return SizeDB.Select(ID, out CustomException);
        }
        public Fabric GetFabric(int FabricID)
        {
            return FabricDB.Select(FabricID, out CustomException);
        }

        public List<Fabric> GetFabric()
        {
            return FabricDB.SelectAll(out CustomException);
        }

        public List<Fabric> GetFabrics(int UtilityOrderID)
        {
            return FabricDB.SelectAll(UtilityOrderID, out CustomException);
        }

        public RollerBlinds GetRollerBlind(int RollerBlindsID)
        {
            return RollerBlindsDB.Select(RollerBlindsID, out CustomException);
        }

        public RollerBlindType GetRollerBlindType(int RollerBlindTypeID)
        {
            return RollerBlindTypeDB.Select(RollerBlindTypeID, out CustomException);
        }

        public List<RollerBlinds> GetRollerBlinds()
        {
            return RollerBlindsDB.SelectAll(out CustomException);
        }

        public List<RollerBlinds> GetRollerBlinds(int UtilityOrderID)
        {
            return RollerBlindsDB.SelectAll(UtilityOrderID, out CustomException);
        }

        public Valance GetValance(int ValanceID)
        {
            return ValanceDB.Select(ValanceID, out CustomException);
        }

        public List<Valance> GetValance()
        {
            return ValanceDB.SelectAll(out CustomException);
        }

        public List<Valance> GetValances(int UtilityOrderID)
        {
            return ValanceDB.SelectAll(UtilityOrderID, out CustomException);
        }

        public Order SaveOrder(Order OrderData)
        {
            return OrderDB.Create(OrderData, out CustomException);
        }

        public Order UpdateOrder(Order OrderData)
        {
            return OrderDB.Update(OrderData, out CustomException);
        }

        public OrderDetail SaveOrderDetail(OrderDetail OrderDetailData)
        {
            return OrderDetailDB.Create(OrderDetailData, out CustomException);
        }

        public OrderDetail UpdateOrderDetail(OrderDetail OrderDetailData)
        {
            return OrderDetailDB.Update(OrderDetailData, out CustomException);
        }

        public UtilityOrder SaveUtilityOrder(UtilityOrder UtilityOrderData)
        {
            return UtilityOrderDB.Create(UtilityOrderData, out CustomException);
        }

        public UtilityOrder UpdateUtilityOrder(UtilityOrder UtilityOrderData)
        {
            return UtilityOrderDB.Update(UtilityOrderData, out CustomException);
        }

        public Fabric SaveFabric(Fabric FabricData)
        {
            return FabricDB.Create(FabricData, out CustomException);
        }

        public RollerBlinds SaveRollerBlinds(RollerBlinds RollerBlindsData)
        {
            return RollerBlindsDB.Create(RollerBlindsData, out CustomException);
        }

        public Valance SaveValance(Valance ValanceData)
        {
            return ValanceDB.Create(ValanceData, out CustomException);
        }

        public BottomRail SaveBottomRail(BottomRail BottomRailData)
        {
            return BottomRailDB.Create(BottomRailData, out CustomException);
        }

        public Fabric UpdateFabric(Fabric FabricData)
        {
            return FabricDB.Update(FabricData, out CustomException);
        }

        public RollerBlinds UpdateRollerBlinds(RollerBlinds RollerBlindsData)
        {
            return RollerBlindsDB.Update(RollerBlindsData, out CustomException);
        }

        public Valance UpdateValance(Valance ValanceData)
        {
            return ValanceDB.Update(ValanceData, out CustomException);
        }

        public BottomRail UpdateBottomRail(BottomRail BottomRailData)
        {
            return BottomRailDB.Update(BottomRailData, out CustomException);
        }

        public bool DeleteFabric(int FabricID)
        {
            return FabricDB.Delete(FabricID, out CustomException);
        }

        public bool DeleteRollerBlinds(int RollerBlindsID)
        {
            return RollerBlindsDB.Delete(RollerBlindsID, out CustomException);
        }

        public bool DeleteValance(int ValanceID)
        {
            return ValanceDB.Delete(ValanceID, out CustomException);
        }

        public bool DeleteBottomRail(int BottomRailID)
        {
            return BottomRailDB.Delete(BottomRailID, out CustomException);
        }
    }
}
