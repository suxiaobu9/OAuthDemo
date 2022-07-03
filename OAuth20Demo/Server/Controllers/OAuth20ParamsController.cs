using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OAuth20Demo.Shared;

namespace OAuth20Demo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class OAuth20ParamsController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly OAuth20ParamModel oAuth20ParamModel;

    public OAuth20ParamsController(ILogger<WeatherForecastController> logger,
        IOptions<OAuth20ParamModel> oAuth20ParamModel)
    {
        _logger = logger;
        this.oAuth20ParamModel = oAuth20ParamModel.Value;
    }

    [HttpGet("GetLineClientId")]
    public string? GetLineClientId()
    {
        return oAuth20ParamModel.LineOAuthClientId;
    }

    [HttpGet("GetLineCallBack")]
    public string? GetLineCallBack()
    {
        return oAuth20ParamModel.LineCallBack;
    }

    [HttpGet("GetLineNotifyClientId")]
    public string? GetLineNotifyClientId()
    {
        return oAuth20ParamModel.LineNotifyClientId;
    }

    [HttpGet("GetLineNotifyCallBack")]
    public string? GetLineNotifyCallBack()
    {
        return oAuth20ParamModel.LineNotifyCallBack;
    }

    [HttpGet("GetGoogleClientId")]
    public string? GetGoogleClientId()
    {
        return oAuth20ParamModel.GoogleOAuthClientId;
    }

    [HttpGet("GetGoogleCallBack")]
    public string? GetGoogleCallBack()
    {
        return oAuth20ParamModel.GoogleCallBack;
    }
}