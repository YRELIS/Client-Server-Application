using ApplicationTZ.Core.Models;
using System;
using System.Collections.Generic;
using TZ.Infrastructure;

namespace TZ.Services
{
    public class PhoneService : IPhoneService
    {
        /// <summary>
        /// our repo
        /// </summary>
        private readonly JsonPhoneRepository _storage;
        public PhoneService(JsonPhoneRepository storage)
        {
            this._storage = storage;
        }
        public IEnumerable<Phone> Index()
        {
            return _storage.List();
        }

        public Phone Add(Phone phone)
        {
            _storage.Add(phone);
            _storage.SaveAll();
            return phone;
        }

        public bool DeleteByID(Guid guid)
        {
            _storage.DeleteByID(guid);
            _storage.SaveAll();
            return true;
        }

        public Phone Edit(Phone phone)
        {
            _storage.Edit(phone);
            _storage.SaveAll();
            return phone;
        }

        public Phone GetByID(Guid guid)
        {
            return _storage.GetByID(guid);
        }
    }
}
