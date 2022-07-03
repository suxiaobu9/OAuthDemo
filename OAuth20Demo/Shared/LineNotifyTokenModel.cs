using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth20Demo.Shared;

public class LineNotifyTokenModel
{
    public int status { get; set; }
    public string message { get; set; }
    public string access_token { get; set; }
}
