using System.Threading.Tasks;
using System.Text;
using System;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeocodificadorService
{
    public class RequestCoordenada
    {


        //funcion para el get de la coordenada
        public  async Task<Localizacion> GetCordenadaAsync(string localizar)
        {                   
            try
            {
                Localizacion Alocalizar = JsonConvert.DeserializeObject<Localizacion>(localizar);     
                string url= "https://nominatim.openstreetmap.org/search?" +  "street=" + Alocalizar.Calle + "+" + Alocalizar.Numero
                + "&country=" + Alocalizar.Pais + "&format=json&city=" + Alocalizar.Ciudad + "&state=" + Alocalizar.Provincia;
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response =  client.Execute(request);        
                JArray jsonArray = JArray.Parse(response.Content);
                var data = JObject.Parse(jsonArray[0].ToString());
                Alocalizar.lat = data["lat"].ToString();
                Alocalizar.lon = data["lon"].ToString();     
                if (Alocalizar.lon != "" && Alocalizar.lat != "")
                {
                    await EnviarCola(Alocalizar);
                }             
                return Alocalizar;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;               
            }
        }

        public async Task<Boolean>  EnviarCola (Localizacion localizacion)
        {
            bool response = false;
            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "user", Password = "password" }; 
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Cola_Andreani3", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var json = JsonConvert.SerializeObject(localizacion);
                    var body = Encoding.UTF8.GetBytes(json);
                     channel.BasicPublish(exchange: "", routingKey: "Cola_Andreani3", basicProperties: null, body: body);
                    response = true;
                }           
            }
             return response;  
        }
    }
}
