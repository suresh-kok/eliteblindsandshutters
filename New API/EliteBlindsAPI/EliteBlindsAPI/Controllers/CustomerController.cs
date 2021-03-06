﻿using EliteBlindsAPI.Models;
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

        public string PostCustomer([FromBody]string value)
        {
            var custObj = new JavaScriptSerializer().Deserialize(value, typeof(Customer));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveCustomer((Customer)custObj));
        }

        public void DeleteCustomer(int CustId)
        {
            BusinessObj.DeleteCustomer(CustId);
        }

        public bool CheckLogin([FromBody]string Email, [FromBody]string Password)
        {
            return BusinessObj.LoginCheck(Email, Password);
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

        public string GetOrders(int CustId)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustOrders(CustId));
            return json;
        }

        public string GetOrderDetail(int OrderID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderDetails(OrderID));
            return json;
        }

        public string GetCustOrderDetail(int CustID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetOrderDetails((BusinessObj.GetCustOrders(CustID)).Select(a => a.OrderID).ToList()));
            return json;
        }

        public string GetUtilityOrders(int CustId)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetUtilityOrders(CustId));
            return json;
        }

        public string GetUtilityOrderDetail(int CustID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetCustomerUtilityOrders(CustID));
            return json;
        }

        public string GetAllCustomerOrders(int CustID)
        {
            var json = new JavaScriptSerializer().Serialize(BusinessObj.GetAllCustomerOrders(CustID));
            return json;
        }

        public string PostOrder([FromBody]string value)
        {
            var orderObj = new JavaScriptSerializer().Deserialize(value, typeof(Order));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveOrder((Order)orderObj));
        }

        public string PostOrderDetails([FromBody]string value)
        {
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

        public string PostFabric([FromBody]string value)
        {
            var FabricObj = new JavaScriptSerializer().Deserialize(value, typeof(List<Fabric>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveFabric((List <Fabric>)FabricObj));
        }

        public void DeleteFabric(int FabricID)
        {
            BusinessObj.DeleteFabric(FabricID);
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

        public string PostRollerBlinds([FromBody]string value)
        {
            var RollerBlindsObj = new JavaScriptSerializer().Deserialize(value, typeof(List<RollerBlinds>));
            return new JavaScriptSerializer().Serialize(BusinessObj.SaveRollerBlinds((List<RollerBlinds>)RollerBlindsObj));
        }

        public void DeleteRollerBlinds(int RollerBlindsID)
        {
            BusinessObj.DeleteRollerBlinds(RollerBlindsID);
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

    }
}
