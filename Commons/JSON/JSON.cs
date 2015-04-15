using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Commons.JSON
{
    //Java与C#间json日期格式互转解决方案
    public static class JSON
    {
        //两端均使用 "yyyy-MM-dd HH:mm:ss.SSS" 的格式进行传输
        private static IsoDateTimeConverter convert = new IsoDateTimeConverter();
        public static string SerializeObjectToJava(object value)
        {            
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            return JsonConvert.SerializeObject(value, convert);
        }
    }
}
