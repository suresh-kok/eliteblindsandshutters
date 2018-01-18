using EliteBlindsAPI.Models;
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
        [HttpGet]
        public string GetCustomer()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustomers());
            return json ;
        }

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

        public void DeleteCustomer(int CustId)
        {
            BusinessObj.DeleteCustomer(CustId);
        }

        [Route("CheckLogin/{Email}/{Password}")]
        [HttpGet]
        public string CheckLogin(string Email, string Password)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.LoginCheck(Email, Password));
            return json;
        }

        public void ForgotPassowrd([FromBody]string Email)
        {
            BusinessObj.ForgotPassword(Email);
        }

        public void ResetPassword([FromBody]string Email, [FromBody]string Password)
        {
            BusinessObj.ResetPassword(Email,Password);
        }

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

        public string GetSearchUtilityOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustomerUtilityOrders(CustId, FilterBy, SearchCriteria, OrderBy));
            return json;
        }

        public string GetOrder(int OrderID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrder(OrderID));
            return json;
        }

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

        public string GetCustOrderDetail(int CustID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderDetails((BusinessObj.GetCustOrders(CustID,"","","")).Select(a => a.OrderID).ToList()));
            return json;
        }

        public string GetUtilityOrder(int OrderID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetUtilityOrder(OrderID));
            return json;
        }

        public string GetUtilityOrders(int CustId)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetUtilityOrders(CustId));
            return json;
        }

        public string GetUtilityOrderDetail(int CustID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustomerUtilityOrders(CustID, "","", ""));
            return json;
        }

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

        public string PostUtilityOrder([FromBody]string value)
        {
            var utilityOrderObj = new JavaScriptSerializer().Deserialize(value, typeof(UtilityOrder));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveUtilityOrder((UtilityOrder)utilityOrderObj));
        }

        public void DeleteOrder(int OrderID)
        {
            BusinessObj.DeleteOrder(OrderID);
        }

        public void DeleteUtilityOrder(int UtilityOrderID)
        {
            BusinessObj.DeleteUtilityOrder(UtilityOrderID);
        }

        public bool ApproveOrders(string OrderIDs)
        {
            var IDObj = new JavaScriptSerializer().Deserialize(OrderIDs, typeof(List<int>));
            return BusinessObj.ApproveOrders((List<int>)IDObj);
        }

        public bool ApproveUtilityOrders(string OrderIDs)
        {
            var IDObj = new JavaScriptSerializer().Deserialize(OrderIDs, typeof(List<int>));
            return BusinessObj.ApproveOrders((List<int>)IDObj);
        }

        public string GetFabric(int FabricID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetFabric(FabricID));
        }

        public string PostFabric([FromBody]string value)
        {
            var FabricObj = new JavaScriptSerializer().Deserialize(value, typeof(List<Fabric>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveFabric((List <Fabric>)FabricObj));
        }

        public void DeleteFabric(int FabricID)
        {
            BusinessObj.DeleteFabric(FabricID);
        }

        public string GetValance(int ValanceID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetValance(ValanceID));
        }

        public string PostValance([FromBody]string value)
        {
            var ValanceObj = new JavaScriptSerializer().Deserialize(value, typeof(List<Valance>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveValance((List<Valance>)ValanceObj));
        }

        public void DeleteValance(int ValanceID)
        {
            BusinessObj.DeleteValance(ValanceID);
        }

        public string GetRollerBlinds(int RollerBlindsID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetRollerBlinds(RollerBlindsID));
        }

        public string GetRollerBlindType(int RollerBlindTypeID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetRollerBlindType(RollerBlindTypeID));
        }

        public string PostRollerBlinds([FromBody]string value)
        {
            var RollerBlindsObj = new JavaScriptSerializer().Deserialize(value, typeof(List<RollerBlinds>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveRollerBlinds((List<RollerBlinds>)RollerBlindsObj));
        }

        public void DeleteRollerBlinds(int RollerBlindsID)
        {
            BusinessObj.DeleteRollerBlinds(RollerBlindsID);
        }

        public string GetBottomRail(int BottomRailID)
        {
            return new JavaScriptSerializer().Serialize(BusinessObj.GetBottomRail(BottomRailID));
        }

        public string PostBottomRail([FromBody]string value)
        {
            var BottomRailObj = new JavaScriptSerializer().Deserialize(value, typeof(List<BottomRail>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveBottomRail((List<BottomRail>)BottomRailObj));
        }

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

        public string GetOrderStatus()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderStatus());
            return json; ;
        }

        public string GetOrderType()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderType());
            return json; ;
        }

        public string GetRoles()
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetRoles());
            return json; ;
        }

    }
}
