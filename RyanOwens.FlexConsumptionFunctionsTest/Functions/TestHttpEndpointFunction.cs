using System.Diagnostics;
using System.Net;
using Azure.Identity;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace RyanOwens.FlexConsumptionFunctionsTest.Functions;

public class TestHttpEndpointFunction(ILogger<TestHttpEndpointFunction> logger)
{
    [Function("TestHttpEndpointFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        try
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            var client = new EventGridPublisherClient(
                new Uri("https://5211919363.centralus-1.eventgrid.azure.net/api/events"),
                new DefaultAzureCredential());
            var response = client.SendEvent(new CloudEvent("test", "test", new { Id = Activity.Current?.Id }));
            return new OkObjectResult(response.Status);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Message: {Message}", e.Message);
            Activity.Current?.RecordException(e);
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}