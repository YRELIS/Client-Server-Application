using ApplicationTZ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TZ.Infrastructure
{
    public class JsonPhoneRepository : IRepository<Phone>
    {
        private readonly PhoneContext db;
        public JsonPhoneRepository()
        {
            this.db = new PhoneContext();
        }
        public void Add(Phone phone)
        {
            db.Phones.Add(phone);
        }
        public void DeleteByID(Guid guid)
        {
            var phone = db.Phones.FirstOrDefault(x => x.Id == guid);
            if (phone != null)
                db.Phones.Remove(phone);
        }
        public Phone Edit(Phone newInfo)
        {
            var obj = db.Phones.First(x => x.Id == newInfo.Id);
            obj.Model = newInfo.Model;
            obj.base64Image = newInfo.base64Image;
            obj.UpdatedOn = DateTime.Now;
            return obj;
        }
        public IEnumerable<Phone> List()
        {
            return db.Phones;
        }
        public Phone GetByID(Guid guid)
        {
            return db.Phones.First(x => x.Id == guid);
        }
        public void SaveAll()
        {
            db.SaveChanges();
        }
    }
}
