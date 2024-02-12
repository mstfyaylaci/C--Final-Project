using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        

        public Result(bool success, string message) : this(success) // tek parametreli olanıda çalıştır eğer tek succes gelirse
                                                                    // gelmez ise sadece mesaj olan consractor çalışsın
        {
            
            this.Message = message;
        }
        public Result(bool success)
        {
            this.Success = success;
            
        }
        
        public string Message { get;  }

        public bool Success { get; }
    }
}
