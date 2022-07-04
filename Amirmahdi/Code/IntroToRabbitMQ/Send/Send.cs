using RabbitMQ.Client;
using System.Text;

internal class Send
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "TimeLogger",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            while (true)
            {
                string message = Chronometer();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "TimeLogger",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($"Producer Sent {message}");
                Thread.Sleep(1000);
            }
        }
    }

    public static string Chronometer()
    {
        return DateTime.Now.ToString();
    }
}