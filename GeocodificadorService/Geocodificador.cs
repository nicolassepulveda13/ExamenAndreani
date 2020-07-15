using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;


namespace GeocodificadorService
{
    class Geocodificador
    {
        static   void Main(string[] args)
        {
            String cola = "Cola_Andreani2";

            //parametros necesarios para rabbit
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "user",
                Password = "password"
            };
            try
            {
                //establece la conexion
                using  (var _connection = factory.CreateConnection())
                {
                    using (var _channel = _connection.CreateModel())
                    {
                        _channel.QueueDeclare(queue: cola, durable: false, exclusive: false, autoDelete: false, arguments: null);
                        var consumer = new EventingBasicConsumer(_channel);              
                        Console.WriteLine("Escuchando");
                        var content = "";
                        consumer.Received +=  async (ch, ea) =>
                        {
                            var body = ea.Body.ToArray(); //cuerpo del mensaje
                            content = Encoding.UTF8.GetString(body);// contenido
                            Console.WriteLine("recibido: {0}" + content); 
                            Thread.Sleep(2 * 1000);
                            Localizacion localizar = new Localizacion();
                            RequestCoordenada rq = new RequestCoordenada();
                            await rq.GetCordenadaAsync(content);
                            Thread.Sleep(2 * 1000);
                            _channel.BasicAck(ea.DeliveryTag, false);
                        };
                
                        _channel.BasicConsume(queue: cola, autoAck: true, consumer: consumer);
                        Console.WriteLine("[enter]" + "para salir");
                        Console.ReadLine();
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message.ToString());
            }

        }
    }
    
}
