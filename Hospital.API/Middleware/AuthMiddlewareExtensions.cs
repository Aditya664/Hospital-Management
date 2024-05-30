namespace Hospital.API.Middleware
{
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>("YourSuperSecretKeyThatIsAtLeast32CharactersLong");
        }
    }
}
