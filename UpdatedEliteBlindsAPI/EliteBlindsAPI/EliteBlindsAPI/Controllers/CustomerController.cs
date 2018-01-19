using EliteBlindsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;


namespace EliteBlindsAPI.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private Business.IBusiness BusinessObj = new Business.Business();

        [Route("GetCustomer")]
        public string GetCustomer()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustomers());
            return json ;
        }
        
        [Route("GetCustomer/{CustId}")]
        public string GetCustomer(int CustId)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustomer(CustId));
            return json;
        }

        [Route("PostCustomer")]
        public string PostCustomer(HttpRequestMessage obj)
        {
            var value = obj.Content.ReadAsStringAsync().Result;
            var custObj = new JavaScriptSerializer().Deserialize(value, typeof(Customer));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveCustomer((Customer)custObj));
        }

        [Route("DeleteCustomer/{CustId}")]
        public void DeleteCustomer(int CustId)
        {
            BusinessObj.DeleteCustomer(CustId);
        }

        [HttpGet]
        [Route("CheckLogin/{Email}/{Password}")]
        public string CheckLogin(string Email, string Password)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.LoginCheck(Email, Password));
            return json;
        }

        [HttpGet]
        [Route("ForgotPassowrd")]
        public void ForgotPassowrd([FromBody]string Email)
        {
            BusinessObj.ForgotPassword(Email);
        }

        [HttpGet]
        [Route("ResetPassword")]
        public void ResetPassword([FromBody]string Email, [FromBody]string Password)
        {
            BusinessObj.ResetPassword(Email,Password);
        }

        [HttpGet]
        [Route("ActivateCustomer/{CustId}")]
        public bool ActivateCustomer(int CustId)
        {
            return BusinessObj.SetUserActive(CustId);
        }

        [Route("SearchOrders/{CustId}/{FilterBy}/{SearchCriteria}/{OrderBy}")]
        public string GetSearchOrders(int CustId,string FilterBy, string SearchCriteria, string OrderBy)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustOrders(CustId,FilterBy, SearchCriteria,OrderBy));
            return json;
        }

        [Route("SearchUtilityOrders/{CustId}/{FilterBy}/{SearchCriteria}/{OrderBy}")]
        public string GetSearchUtilityOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustomerUtilityOrders(CustId, FilterBy, SearchCriteria, OrderBy));
            return json;
        }

        [Route("GetOrder/{OrderID}")]
        public string GetOrder(int OrderID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrder(OrderID));
            return json;
        }

        [Route("GetOrderDetails/{OrderDetailsID}")]
        public string GetOrderDetail(int OrderDetailsID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderDetail(OrderDetailsID));
            return json;
        }

        [Route("GetOrderDetails/{OrderID}")]
        public string GetOrderDetails(int OrderID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderDetails(OrderID));
            return json;
        }

        [Route("GetCustOrderDetails/{CustID}")]
        public string GetCustOrderDetail(int CustID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderDetails((BusinessObj.GetCustOrders(CustID,"","","")).Select(a => a.OrderID).ToList()));
            return json;
        }

        [Route("GetUtilityOrder/{OrderID}")]
        public string GetUtilityOrder(int OrderID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetUtilityOrder(OrderID));
            return json;
        }

        [Route("GetUtilityOrders/{CustID}")]
        public string GetUtilityOrders(int CustId)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetUtilityOrders(CustId));
            return json;
        }

        [Route("GetUtilityOrderDetails/{CustID}")]
        public string GetUtilityOrderDetail(int CustID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustomerUtilityOrders(CustID, "","", ""));
            return json;
        }

        [Route("GetAllCustomerOrders/{CustID}")]
        public string GetAllCustomerOrders(int CustID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetAllCustomerOrders(CustID));
            return json;
        }

        [Route("OrderInitiation")]
        public string PostOrder(HttpRequestMessage order)
        {
            var value = order.Content.ReadAsStringAsync().Result;

            var orderObj = new JavaScriptSerializer().Deserialize(value, typeof(Order));
           
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveOrder((Order)orderObj));
        }

        [Route("OrderDetails")]
        public string PostOrderDetails(HttpRequestMessage orderDetails)
        {
            var value = orderDetails.Content.ReadAsStringAsync().Result;
            var orderDetailsObj = new JavaScriptSerializer().Deserialize(value, typeof(List<OrderDetail>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveOrderDetails((List<OrderDetail>)orderDetailsObj));
        }

        [Route("PostUtilityOrder")]
        public string PostUtilityOrder([FromBody]string value)
        {
            var utilityOrderObj = new JavaScriptSerializer().Deserialize(value, typeof(UtilityOrder));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveUtilityOrder((UtilityOrder)utilityOrderObj));
        }

        [Route("DeleteOrder/{OrderID}")]
        public void DeleteOrder(int OrderID)
        {
            BusinessObj.DeleteOrder(OrderID);
        }

        [Route("DeleteUtilityOrder/{UtilityOrderID}")]
        public void DeleteUtilityOrder(int UtilityOrderID)
        {
            BusinessObj.DeleteUtilityOrder(UtilityOrderID);
        }

        [HttpPost]
        [Route("ApproveOrders/{OrderIDs}")]
        public bool ApproveOrders([FromBody]List<int> OrderIDs)
        {
            return BusinessObj.ApproveOrders(OrderIDs);
        }

        [HttpPost]
        [Route("ApproveUtilityOrders/{OrderIDs}")]
        public bool ApproveUtilityOrders([FromBody]List<int> OrderIDs)
        {
            return BusinessObj.ApproveUtilityOrders(OrderIDs);
        }

        [HttpPost]
        [Route("ChangeOrderStatus/{OrderIDs}/{Status}")]
        public bool ChangeOrderStatus([FromBody]List<int> OrderIDs, int StatusID)
        {
            return BusinessObj.ChangeOrderStatus(OrderIDs, StatusID);
        }

        [HttpPost]
        [Route("ChangeUtilityOrderStatus/{OrderIDs}/{Status}")]
        public bool ChangeUtilityOrderStatus([FromBody]List<int> OrderIDs,int StatusID)
        {
            return BusinessObj.ChangeUtilityOrderStatus(OrderIDs, StatusID);
        }

        [Route("GetFabric/{FabricID}")]
        public string GetFabric(int FabricID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetFabric(FabricID));
        }

        [Route("PostFabric")]
        public string PostFabric([FromBody]string value)
        {
            var FabricObj = new JavaScriptSerializer().Deserialize(value, typeof(List<Fabric>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveFabric((List <Fabric>)FabricObj));
        }

        [Route("DeleteFabric/{FabricID}")]
        public void DeleteFabric(int FabricID)
        {
            BusinessObj.DeleteFabric(FabricID);
        }

        [Route("GetValance/{ValanceID}")]
        public string GetValance(int ValanceID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetValance(ValanceID));
        }

        [Route("PostValance")]
        public string PostValance([FromBody]string value)
        {
            var ValanceObj = new JavaScriptSerializer().Deserialize(value, typeof(List<Valance>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveValance((List<Valance>)ValanceObj));
        }

        [Route("DeleteValance/{ValanceID}")]
        public void DeleteValance(int ValanceID)
        {
            BusinessObj.DeleteValance(ValanceID);
        }

        [Route("GetRollerBlinds/{RollerBlindsID}")]
        public string GetRollerBlinds(int RollerBlindsID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetRollerBlind(RollerBlindsID));
        }

        [Route("GetRollerBlindType/{RollerBlindTypeID}")]
        public string GetRollerBlindType(int RollerBlindTypeID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetRollerBlindType(RollerBlindTypeID));
        }

        [Route("PostRollerBlinds")]
        public string PostRollerBlinds([FromBody]string value)
        {
            var RollerBlindsObj = new JavaScriptSerializer().Deserialize(value, typeof(List<RollerBlinds>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveRollerBlinds((List<RollerBlinds>)RollerBlindsObj));
        }

        [Route("DeleteRollerBlinds/{RollerBlindsID}")]
        public void DeleteRollerBlinds(int RollerBlindsID)
        {
            BusinessObj.DeleteRollerBlinds(RollerBlindsID);
        }

        [Route("GetBottomRail/{BottomRailID}")]
        public string GetBottomRail(int BottomRailID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetBottomRail(BottomRailID));
        }

        [Route("PostBottomRail")]
        public string PostBottomRail([FromBody]string value)
        {
            var BottomRailObj = new JavaScriptSerializer().Deserialize(value, typeof(List<BottomRail>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveBottomRail((List<BottomRail>)BottomRailObj));
        }

        [Route("DeleteBottomRail/{BottomRailID}")]
        public void DeleteBottomRail(int BottomRailID)
        {
            BusinessObj.DeleteBottomRail(BottomRailID);
        }
        
        [Route("GetColors/{For}")]
        public string GetColors(string For)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetColors(For));
            return json; ;
        }

        [Route("GetMaterial/{For}")]
        public string GetMaterial(string For)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetMaterial(For));
            return json; ;
        }

        [Route("GetCordStyle/{For}")]
        public string GetCordStyle(string For)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCordStyle(For));
            return json; ;
        }

        [Route("GetSlatStyle/{For}")]
        public string GetSlatStyle(string For)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetSlatStyle(For));
            return json; ;
        }

        [Route("GetSize/{For}")]
        public string GetSize(string For)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetSize(For));
            return json; ;
        }

        [Route("GetBlindType")]
        public string GetBlindType()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetBlindType());
            return json; ;
        }

        [Route("GetOrderStatus")]
        public string GetOrderStatus()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderStatus());
            return json; ;
        }

        [Route("GetOrderType")]
        public string GetOrderType()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderType());
            return json; ;
        }

        [Route("GetRoles")]
        public string GetRoles()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetRoles());
            return json; ;
        }
        
    }
}
