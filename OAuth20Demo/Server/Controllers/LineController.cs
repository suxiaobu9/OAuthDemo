using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OAuth20Demo.Shared;

namespace OAuth20Demo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class LineController : Controller
{
    private readonly HttpClient httpClient;
    private readonly OAuth20ParamModel oAuth20ParamModel;

    public LineController(HttpClient httpClient,
        IOptions<OAuth20ParamModel> oAuth20ParamModel)
    {
        this.httpClient = httpClient;
        this.oAuth20ParamModel = oAuth20ParamModel.Value;
    }

    [HttpGet("GetToken")]
    public async Task<IActionResult> GetTokenAsync(string code)
    {
        var dic = new Dictionary<string, string>
        {
            { "grant_type","authorization_code"},
            { "code",code},
            { "redirect_uri",oAuth20ParamModel.LineCallBack??""},
            { "client_id",oAuth20ParamModel.LineOAuthClientId??""},
            { "client_secret",oAuth20ParamModel.LineOAuthClientSecret??""},
        };

        var response = await httpClient.PostAsync("https://api.line.me/oauth2/v2.1/token", new FormUrlEncodedContent(dic));

        if (!response.IsSuccessStatusCode)
            return Unauthorized();

        return Ok(await response.Content.ReadFromJsonAsync<LineTokenModel>());
    }


    [HttpGet("GetUserProfile")]
    public async Task<IActionResult> GetUserProfileAsync()
    {
        var authorization = HttpContext.Request.Headers["Authorization"];

        if (authorization.Count == 0)
            return Unauthorized();

        var token = authorization[0]["bearer".Length..];

        var dic = new Dictionary<string, string>
        {
            { "id_token", token },
            { "client_id", oAuth20ParamModel.LineOAuthClientId??""}
        };

        var response = await httpClient.PostAsync("https://api.line.me/oauth2/v2.1/verify", new FormUrlEncodedContent(dic));

        if (!response.IsSuccessStatusCode)
            return Unauthorized();

        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> LogoutAsync([FromForm] string accessToken)
    {
        var dic = new Dictionary<string, string>
        {
            { "access_token",accessToken},
            { "client_id",oAuth20ParamModel.LineOAuthClientId??""},
            { "client_secret",oAuth20ParamModel.LineOAuthClientSecret??""},
        };

        var response = await httpClient.PostAsync("https://api.line.me/oauth2/v2.1/revoke", new FormUrlEncodedContent(dic));

        if (!response.IsSuccessStatusCode)
            return Unauthorized();

        return Ok();
    }

    [HttpPost("GetNotifyToken")]
    public async Task<IActionResult> GetNotifyTokenAsync([FromForm] string code)
    {
        var dic = new Dictionary<string, string>
        {
            { "grant_type","authorization_code"},
            { "code",code},
            { "redirect_uri",oAuth20ParamModel.LineNotifyCallBack??""},
            { "client_id",oAuth20ParamModel.LineNotifyClientId??""},
            { "client_secret",oAuth20ParamModel.LineNotifyClientSecret??""}
        };

        var response = await httpClient.PostAsync("https://notify-bot.line.me/oauth/token", new FormUrlEncodedContent(dic));

        if (!response.IsSuccessStatusCode)
            return Unauthorized();


        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpPost("RevokeNotify")]
    public async Task<IActionResult> RevokeNotifyAsync([FromForm] string accessToken)
    {

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

        var response = await httpClient.PostAsync("https://notify-api.line.me/api/revoke", null);

        if (!response.IsSuccessStatusCode)
            return Unauthorized();

        return Ok();
    }

    [HttpPost("Notify")]
    public async Task<IActionResult> NotifyAsync([FromForm]string accessToken, [FromForm]string message)
    {
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

        var dic = new Dictionary<string, string>
        {
            { "message",message}
        };

        var response = await httpClient.PostAsync("https://notify-api.line.me/api/notify", new FormUrlEncodedContent(dic)); ;

        return Ok();

    }
}
