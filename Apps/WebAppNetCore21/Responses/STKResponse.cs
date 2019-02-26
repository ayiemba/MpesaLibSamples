using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppNetCore.Responses
{

    public class STKResponse
    {
        public Body Body { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public class CallbackMetadata
    {
        public CallbackMetadata()
        {
            Item = new List<Item>();
        }
        public List<Item> Item { get; set; }
    }

    public class StkCallback
    {
        public StkCallback()
        {
            CallbackMetadata = new CallbackMetadata();
        }
        public string MerchantRequestID { get; set; }
        public string CheckoutRequestID { get; set; }
        public int ResultCode { get; set; }
        public string ResultDesc { get; set; }
        public CallbackMetadata CallbackMetadata { get; set; }
    }


    public class Body
    {
        public Body()
        {
            StkCallback = new StkCallback();
        }

        [JsonProperty("stkCallBack")]
        public StkCallback StkCallback { get; set; }
    }

    
}
