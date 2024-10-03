namespace MajaMayo.API.Test.Infrastructure;

public class EnvironmentVariablesTests
{
    [Fact]
    public void EnvironmentVariables_ShouldBeSet()
    {
        var dbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        var securityKey = Environment.GetEnvironmentVariable("SECURITY_KEY");
        var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");

        Assert.False(string.IsNullOrEmpty(dbConnectionString), "DB_CONNECTION_STRING is not set.");
        Assert.False(string.IsNullOrEmpty(securityKey), "SECURITY_KEY is not set.");
        Assert.False(string.IsNullOrEmpty(smtpPassword), "SMTP_PASSWORD is not set.");
    }
}