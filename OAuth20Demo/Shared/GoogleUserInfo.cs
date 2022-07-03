using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth20Demo.Shared;

public class GoogleUserInfo
{
    public string? access_token { get; set; }

    public string? name { get; set; }
    
    public string? email { get; set; }

    public string? picture { get; set; }

}
