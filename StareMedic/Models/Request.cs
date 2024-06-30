using Newtonsoft.Json;
using StareMedic.Models.Entities;
using System.Net.Sockets;
using System.Text;

namespace StareMedic.Models
{
    public class Request
    {

        public Request(int work)
        {
            Work = work;
        }
        public int Work { get; set; }
        public string SerialDocto { get; set; }
        public string Observaciones { get; set; }
        public string cTextoExtra1 { get; set; }
        public string cTextoExtra2 { get; set; }
        public string cTextoExtra3 { get; set; }

        public double FillPackAndPush(CasoClinico caso, Patient paciente, Rooms room, Medic medico, Diagnostico diagnostico)
        {
            string confPath = Path.Combine(AppContext.BaseDirectory, "Data/config.json"); // Ruta completa al archivo JSON
                                                                                          // Verificar si el archivo existe
            if (!File.Exists(confPath))
            {
                return 0;
            }

            // Leer el contenido del archivo
            string jsonString = File.ReadAllText(confPath);

            // Verificar que el contenido no sea nulo ni vacío
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return 0;
            }

            // Deserializar el contenido en un objeto Config
            Config config = JsonConvert.DeserializeObject<Config>(jsonString);

            Request req = new(Work);
            TcpClient client = new TcpClient(config.ServerSDK, 42069);
            NetworkStream stream = client.GetStream();
            SDK.tDocumento docto = new SDK.tDocumento();
            docto.aCodConcepto = "REM3";
            docto.aCodigoCteProv = room.Descripcion;
            docto.aSerie = "RH";
            docto.aTipoCambio = 1;
            docto.aFecha = caso.FechaIngreso.ToString("MM/dd/yyyy");
            docto.aReferencia = $"{room.Nombre} {caso.Id}";

            req.cTextoExtra1 = new StringBuilder(paciente.Nombre, 20).ToString();
            req.cTextoExtra2 = new StringBuilder(paciente.Telefono + ", Edad: " + paciente.Edad, 20).ToString();
            req.cTextoExtra3 = new StringBuilder(paciente.Domicilio, 20).ToString();


            req.SerialDocto = JsonConvert.SerializeObject(docto);
            req.Observaciones = $"Medico: {medico.Nombre} Diagnostico: {diagnostico.Contenido}";

            //MemoryStream memory = new MemoryStream(); this is just in case the response is extend

            try
            {
                byte[] requestBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(req));
                stream.Write(requestBuffer, 0, requestBuffer.Length);
                byte[] responseBuffer = new byte[4096];
                int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
                string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                Response objresponse = JsonConvert.DeserializeObject<Response>(response);
                if (objresponse.ResponseCode != 0)
                    return (double)objresponse.ResponseCode;
                return 0;
            }
            catch
            {
                return 0;
            }

        }
    }
}

