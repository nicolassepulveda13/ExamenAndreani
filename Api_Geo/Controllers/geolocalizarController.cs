using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Geo.Models;
using RabbitMQ.Client;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using System.Text;  

namespace Api_Geo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class geolocalizarController : ControllerBase
    {
        Prueba_AndreaniContext _context;

        // GET: x
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedido()
        {
            _context = new Prueba_AndreaniContext();
            return await _context.Pedido.ToListAsync();
        }

        // POST: api/geolocalizar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostGeolocalizar(Pedido pedido)
        {
            _context = new Prueba_AndreaniContext();
            try
            {
                _context.Pedido.Add(pedido);
                await _context.SaveChangesAsync();              
                if ( pedido.Id != 0)
                {
                    //establesco valores para la cola 
                    var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "user", Password = "password" };
                    using (var connection = factory.CreateConnection())
                    {
                        using (var channel = connection.CreateModel())
                        {
                            channel.QueueDeclare(queue: "Cola_Andreani2", durable: false, exclusive: false, autoDelete: false, arguments: null);
                            var json = JsonConvert.SerializeObject(pedido);
                            var body = Encoding.UTF8.GetBytes(json);
                            channel.BasicPublish(exchange: "", routingKey: "Cola_Andreani2", basicProperties: null, body: body);
                        }
                    }
                }     
                return Accepted("GetPedido", new { id = pedido.Id });
            }
            catch (Exception ex )
            {
                return BadRequest();
            }
        }


        // GET: api/geolocalizar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            _context = new Prueba_AndreaniContext();

            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

    }
}
