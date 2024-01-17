using MassTransit;
using QueueConsumer.Consumers;
using QueueConsumer.Operations;
using QueueConsumer.Operations.Interfaces;
using QueueMessageTypes;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PasswordChunkConsumer, PasswordChunkConsumerDefinition>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmqcontainer", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddSingleton<PasswordCrackingService>();
        services.AddSingleton<IValidationFactory, ValidationFactory>();

    });

EndpointConvention.Map<PasswordMatchedEvent>(new Uri("queue:passwordMatchedQueue"));
var host = builder.Build();
host.Run();
