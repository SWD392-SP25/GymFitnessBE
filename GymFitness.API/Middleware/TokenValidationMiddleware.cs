using GymFitness.API.Services.Abstractions;
using GymFitness.Application.Abstractions.Services;
using System.IdentityModel.Tokens.Jwt;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRedisService _redisService;
    private readonly IServiceScopeFactory _scopeFactory;

    public TokenValidationMiddleware(RequestDelegate next, IRedisService redisService, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _redisService = redisService;
        _scopeFactory = scopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var userId = jwtToken.Subject;

                // ✅ Kiểm tra nếu token bị revoke trong Redis
                var isRevoked = await _redisService.GetAsync($"revoke:{userId}");
                if (!string.IsNullOrEmpty(isRevoked))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token has been revoked.");
                    return;
                }

                using (var scope = _scopeFactory.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                    // ✅ Kiểm tra nếu user bị ban
                    var user = await userService.GetUserById(Guid.Parse(userId));
                    if (user != null && user.Status == "Banned")
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync("Your account has been banned.");
                        return;
                    }
                }
            }
        }

        await _next(context);
    }
}
