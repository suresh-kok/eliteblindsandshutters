﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EliteBlindsAPI.Business;

namespace EliteBlindsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private IEliteBusiness BusinessObj = new EliteBusiness();
        // GET: api/Order
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Order/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return BusinessObj.GetCustomer(id).ToString();
        }
        
        // POST: api/Order
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Order/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
