using DigitalStore.Base.Entity;
using DigitalStore.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Schema
{

    public class CategoryRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; }

    }


    public class CategoryResponse : BaseResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; }
    }
   
}
