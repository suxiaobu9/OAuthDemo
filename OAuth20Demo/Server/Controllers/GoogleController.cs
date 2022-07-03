using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OAuth20Demo.Shared;

namespace OAuth20Demo.Server.Controllers;


[ApiController]
[Route("[controller]")]
public class GoogleController : Controller
{
    private readonly HttpClient httpClient;
    private readonly OAuth20ParamModel oAuth20ParamModel;

    public GoogleController(HttpClient httpClient,
        IOptions<OAuth20ParamModel> oAuth20ParamModel)
    {
        this.httpClient = httpClient;
        this.oAuth20ParamModel = oAuth20ParamModel.Value;
    }

    [HttpPost("GetToken")]
    public async Task<IActionResult> GetTokenAsync([FromForm] string code)
    {
        var dic = new Dictionary<string, string>
        {
            { "grant_type","authorization_code"},
            { "code",code},
            { "redirect_uri",oAuth20ParamModel.GoogleCallBack??""},
            { "client_id",oAuth20ParamModel.GoogleOAuthClientId??""},
            { "client_secret",oAuth20ParamModel.GoogleOAuthClientSecret??""},
        };

        var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(dic));

        if (!response.IsSuccessStatusCode)
            return Unauthorized();

        return Ok(await response.Content.ReadFromJsonAsync<GoogleTokenModel>());
    }


    [HttpPost("Logout")]
    public async Task<IActionResult> LogoutAsync([FromForm] string accessToken)
    {
        var response = await httpClient.PostAsync($"https://oauth2.googleapis.com/revoke?token={accessToken}", null);

        if (!response.IsSuccessStatusCode)
            return Unauthorized();

        return Ok(await response.Content.ReadFromJsonAsync<GoogleTokenModel>());
    }

}
