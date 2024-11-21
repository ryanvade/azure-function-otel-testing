using System.Diagnostics;
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace RyanOwens.FlexConsumptionFunctionsTest.Functions;

public class TestEventGridFunction2(ILogger<TestEventGridFunction2> logger)
{
    [Function("TestEventGridFunction2")]
    public void Run([EventGridTrigger] CloudEvent cloudEvent)
    {
        logger.LogInformation("Event type: {type}, Event subject: {subject}, Data: {data}", cloudEvent.Type, cloudEvent.Subject, cloudEvent.Data);
        using var activity = Activity.Current?.Source.StartActivity();
    }
}