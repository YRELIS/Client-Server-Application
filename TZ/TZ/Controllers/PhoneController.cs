using ApplicationTZ.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using TZ.Services;

namespace TZ.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PhoneController : ControllerBase
    {
        private readonly PhoneService _phoneService;
        public PhoneController(PhoneService service)
        {
            _phoneService = service;
        }
        /// <summary>
        /// List all items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            var phones = _phoneService.Index();
            return JsonConvert.SerializeObject(phones);
        }
        /// <summary>
        /// Add items
        /// </summary>
        /// <param name="ModelName"></param>
        /// <param name="base64Image"></param>
        /// <returns></returns>
        [Route("Add")]
        public string Add(string ModelName, string base64Image = null)
        {
            Phone newPhone = new Phone();
            {
                newPhone.base64Image = base64Image;
                newPhone.Model = ModelName;
            }
            return _phoneService.Add(newPhone).ToJson();
        }
        /// <summary>
        /// Delete item by ID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [Route("DeleteByID")]
        public string DeleteByID(string guid)
        {

            _phoneService.DeleteByID(new Guid(guid));
            return "Successfull"; 
        }
        /// <summary>
        /// Edit item
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Route("Edit")]
        public string Edit(string phone)
        {
            var phoneFromJson = JsonConvert.DeserializeObject<Phone>(phone);
            return _phoneService.Edit(phoneFromJson).ToJson();
        }
        /// <summary>
        /// Search by ID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [Route("GetByID")]
        public string GetByID(string guid)
        {
            return _phoneService.GetByID(new Guid(guid)).ToJson();
        }

    }
}
