using Microsoft.AspNetCore.Http;

namespace Ichiba.Libs.DocumentSdk.Abstractions;

public class ForwardWorkContextHttpMessageDelegateHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public const string X_INTERNAL = "x-ichiba-internal";
    public const string PREFIX_HEADER = "x";

    public ForwardWorkContextHttpMessageDelegateHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
         if (_httpContextAccessor.HttpContext != null &&
            (_httpContextAccessor.HttpContext.Request.Headers?.ContainsKey("authorization") ?? false))
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].ToString();
            if (!string.IsNullOrWhiteSpace(token))
            {
#if DEBUG
                // Console.WriteLine("Forward Token: " + token);
#endif
                if (request.Headers.Contains("authorization"))
                {
                    request.Headers.Remove("authorization");
                }

                request.Headers.TryAddWithoutValidation("authorization", token);
            }
        }

        if (request.Headers.Contains(X_INTERNAL))
        {
            request.Headers.Remove(X_INTERNAL);
        }

        request.Headers.TryAddWithoutValidation(X_INTERNAL, bool.TrueString.ToLower());

        string[] EXCLUDE_HEADER = new[]
            { "Accepted", "Host", "User-Agent", "Accept-Endcoding", "Content-Type", "Content-Length" };
        _httpContextAccessor?.HttpContext?.Request?.Headers
            ?.Where(x => x.Key.StartsWith(PREFIX_HEADER) || !EXCLUDE_HEADER.Contains(x.Key))
            ?.Select(x => new { x.Key, Val = x.Value.FirstOrDefault() ?? "" })
            ?.ToList()
            ?.ForEach(x =>
            {
                if (request.Headers.Contains(x.Key))
                {
                    request.Headers.Remove(x.Key);
                }

                request.Headers.TryAddWithoutValidation(x.Key, new List<string?>() { x.Val });
            });
        return await base.SendAsync(request, cancellationToken);
    }
}
