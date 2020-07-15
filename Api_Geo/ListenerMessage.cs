using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.EntityFrameworkCore;
using Api_Geo.Models;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
namespace Api_Geo
{
    public class ListenerMessage : BackgroundService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private  IConnection connection;
        private  IModel channel;

        public ListenerMessage()
        {
            //injecto lo que voy a usar
            _hostname = "rabbitmq";
            _queueName = "Cola_Andreani3";
            _username = "user";
            _password = "password";            
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {

            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "user", Password = "password" };            
            string flag = "";
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            //declaro cola si no existe
         }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //token campurador de evento 
            stoppingToken.ThrowIfCancellationRequested();
         try
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                Prueba_AndreaniContext _context = new Prueba_AndreaniContext();//contexto de bd
                var consumer = new EventingBasicConsumer(channel);//consumidor
                consumer.Received += async (mod, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    Pedido localizado = JsonConvert.DeserializeObject<Pedido>(response);
                    _context.Entry(localizado).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();//guardo cambios
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    channel.BasicAck(ea.DeliveryTag, false);
                };
                channel.BasicConsume(_queueName, false, consumer);
            }
            catch (Exception)
            {
                throw;
            }
            return Task.CompletedTask;
        }

    }
}
