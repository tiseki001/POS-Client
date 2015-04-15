using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.JSON;

namespace Commons.Model
{
    public class BaseReturnResultModel<T> : BasePosModel
    {
        public T data;
    }
}
