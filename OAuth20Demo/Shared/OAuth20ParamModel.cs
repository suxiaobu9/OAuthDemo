using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth20Demo.Shared;

public class OAuth20ParamModel
{
    public string? LineOAuthClientId { get; set; }
    public string? LineOAuthClientSecret { get; set; }
    public string? LineCallBack { get; set; }
    public string? LineNotifyClientId { get; set; }
    public string? LineNotifyClientSecret { get; set; }
    public string? LineNotifyCallBack { get; set; }
    public string? GoogleOAuthClientId { get; set; }
    public string? GoogleOAuthClientSecret { get; set; }
    public string? GoogleCallBack { get; set; }
}
