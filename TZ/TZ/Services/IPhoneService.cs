using ApplicationTZ.Core.Models;
using System;
using System.Collections.Generic;

namespace TZ.Services
{
    public interface IPhoneService
    {
        /// <summary>
        /// Return all items
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Phone> Index();
        /// <summary>
        /// Adding item
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Phone Add(Phone phone);
        /// <summary>
        /// Delete item by ID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool DeleteByID(Guid guid);
        /// <summary>
        /// Search item by ID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Phone GetByID(Guid guid);
        /// <summary>
        /// Change item
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Phone Edit(Phone phone);

    }
}