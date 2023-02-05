using System.Text;
using RabbitMQ.Client;
using Users.Domain.Interfaces;

namespace Users.Infrastructure.RabbitMQClient;
public class RabbitMQClient : IRabbitMQClient
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQClient()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "rabbitmq-clusterip-src", //TODO: Move this to a appsetting file
            Port = 5672
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "topic", type: ExchangeType.Topic);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("--> Connected to MessageBus");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
        }
    }

    public void SendMessage(string message)
    {
        if (_connection.IsOpen)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "topic",
                            routingKey: "topic/log",
                            basicProperties: null,
                            body: body);
            Console.WriteLine($"--> We have sent {message}");
        }
        else
        {
            Console.WriteLine("--> RabbitMQ connectionis closed, not sending");
        }
    }


    public void Dispose()
    {
        Console.WriteLine("MessageBus Disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> RabbitMQ Connection Shutdown");
    }
}
