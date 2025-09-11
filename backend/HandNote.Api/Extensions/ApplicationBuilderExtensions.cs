
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();              // Role/Policy check
        return app;
    }
    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app, string PolicyName)
    {
        app.UseCors(PolicyName);
        return app;
    }
}