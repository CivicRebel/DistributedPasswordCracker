using MassTransit;
using QueueMessageTypes;
using QueuePublisher;
using QueuePublisher.Consumers;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PasswordMatchedConsumer, PasswordMatchedConsumerDefinition>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        var factory = new CrackingContextFactory(ctx.Configuration);
        services.AddSingleton(provider =>
        {
            return factory.GetResultsWriter();
        });
        services.AddSingleton(provider =>
        {
            return factory.GetDictionaryReader();
        }
        );
        services.AddSingleton<IHostedService>(provider =>
        {
            return factory.GetQueuePublisherService(provider);
        }
        );
    });

EndpointConvention.Map<PasswordsChunk>(new Uri("queue:passwordsQueue"));

var host = builder.Build();
host.Run();
