using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Geo.Models;

namespace Api_Geo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class geocodificarController : ControllerBase
    {
        private readonly Prueba_AndreaniContext _context;

        public geocodificarController(Prueba_AndreaniContext context)
        {
            _context = context;
        }

        // GET: api/geocodificar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedido()
        {
            return await _context.Pedido.ToListAsync();
        }

        // GET: api/geocodificar?id=5
        [HttpGet("{id}")]
        public async Task<ActionResult<response>> GetPedido(int id)
        {
            response respuesta = new response();
            try
            {
                var pedido = await _context.Pedido.FindAsync(id); //busco el id
                respuesta.Id = id;
                respuesta.lat = pedido.lat;
                respuesta.lon = pedido.lat;
                if (pedido.lat != "" && pedido.lon != "" && pedido.lon != null && pedido.lat != null)
                {
                    respuesta.estado = "Terminado";
                }
                else
                {
                    //armo nuevo json
                    respuesta.lat = "xxxxxxxx";
                    respuesta.lon = "xxxxxxxx";
                    respuesta.estado = "Procesando";
                }
                if (pedido == null)
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
          

            return respuesta;
        }
    }

}

