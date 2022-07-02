namespace OAuth20Demo.Shared;

public class LineUserProfileModel
{
    public string iss { get; set; }
    public string sub { get; set; }
    public string aud { get; set; }
    public int exp { get; set; }
    public int iat { get; set; }
    public string nonce { get; set; }
    public string[] amr { get; set; }
    public string name { get; set; }
    public string picture { get; set; }
    public string email { get; set; }
}
