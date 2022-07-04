using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    public class Consume
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Messenger", durable: false, exclusive: false, autoDelete: false, arguments: null);
                
                Console.WriteLine(" Consumer Waiting for messages.");


                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    Thread.Sleep(500);
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                     
                    Console.WriteLine(" Consumer Received message : {0}",message);
                    
                };

                channel.BasicConsume(queue: "Messenger", autoAck: true, consumer: consumer);
                Console.WriteLine("Acknowleged finished at : {0} ",DateTime.Now.ToString());
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
