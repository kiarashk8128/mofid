using RabbitMQ.Client;
using Serilog;
using Serilog.Sinks.Elasticsearch;
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
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose() 

      .WriteTo.Elasticsearch
                          (new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                          //("localhost:9200")
                          {
                              FailureCallback = e =>
                              {
                                  Console.WriteLine("Unable to submit event " + e.MessageTemplate);
                              },
                              EmitEventFailure = EmitEventFailureHandling.RaiseCallback,

                              AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                              AutoRegisterTemplate = false,
                              DetectElasticsearchVersion = true,
                              IndexFormat = "log_events",
                              BatchPostingLimit = 1000,
                              MinimumLogEventLevel = Serilog.Events.LogEventLevel.Verbose,
                              //RegisterTemplateFailure= RegisterTemplateRecovery.IndexAnyway,
                          })
                          .CreateLogger();
            for(int i=0; i<1000; i++)
            {
                Thread.Sleep(5000);
                logger.Information($"test {i}");
                Console.WriteLine($"test {i}");
            }
           // var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
              //  channel.QueueDeclare(queue: "Messenger", durable: false, exclusive: false, autoDelete: false, arguments: null);
                //int i = 0;
                //while (i < 20)
                //{
                  //  string message = DateTime.Now.ToString();
                    //var body = Encoding.UTF8.GetBytes(message);
                   // channel.BasicPublish(exchange: "", routingKey: "Messenger", basicProperties: null, body: body);
                   // Console.WriteLine(message);
                    //Thread.Sleep(1000);
                    //i++;
                //}
           // }
           

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
