using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth20Demo.Shared;

public class GoogleTokenModel
{
    public string? access_token { get; set; }
    public string? refresh_token { get; set; }
    public string? scope { get; set; }
}
