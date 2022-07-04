using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producer
{
    public class Produce
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Messenger", durable: false, exclusive: false, autoDelete: false, arguments: null);
                int i = 0;
                while (i < 20)
                {
                    string message = DateTime.Now.ToString();
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "", routingKey: "Messenger", basicProperties: null, body: body);
                    Console.WriteLine(message);
                    Thread.Sleep(1000);
                    i++;
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
