using System.Diagnostics;
using Azure.Identity;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace RyanOwens.FlexConsumptionFunctionsTest.Functions;

public class TestEventGridFunction(ILogger<TestEventGridFunction> logger)
{
    [Function("TestEventGridFunction")]
    public void Run([EventGridTrigger] CloudEvent cloudEvent)
    {
        logger.LogInformation("Event type: {type}, Event subject: {subject}, Data: {data}", cloudEvent.Type, cloudEvent.Subject, cloudEvent.Data);
        using var activity = Activity.Current?.Source.StartActivity();
        var client = new EventGridPublisherClient(
            new Uri("https://5211919364.centralus-1.eventgrid.azure.net/api/events"),
            new DefaultAzureCredential());
        var response = client.SendEvent(new CloudEvent("test", "test", new { Id = Activity.Current?.Id }));
        logger.LogInformation("Status: {Status}", response.Status);
    }
}