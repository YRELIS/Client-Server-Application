using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ApplicationTZ.Core.Models
{
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class Phone : Base
    {
        public Phone()
        {

        }
        public Phone(Guid id, DateTime createdOn, string model, DateTime updatedOn, string image)
        {
            Id = id;
            CreatedOn = createdOn;
            Model = model;
            UpdatedOn = updatedOn;
            base64Image = image;
        }
        [JsonProperty]
        public string Model { get; set; }
        [JsonProperty]

        public string base64Image { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
