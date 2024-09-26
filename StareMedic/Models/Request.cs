using Microsoft.UI.Windowing;
using Newtonsoft.Json;
using StareMedic.Models.DTOs;
using StareMedic.Models.Entities;
using System.Net.Sockets;
using System.Text;

namespace StareMedic.Models
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public double FolioDocumento { get; set; }
        public int IdDocumento { get; set; }
    }
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

        public async Task<double> FillPackAndPush(CasoClinico caso, Patient paciente, Rooms room, Medic medico, Diagnostico diagnostico)
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

            DocumentDTO documentRequest = new();
            var httpClient = new HttpClient();

            string url = "http://" + config.ServerSDK + "/addDocumentWithMovementSDK";


            documentRequest.CTEXTOEXTRA1 = new StringBuilder(paciente.Nombre, 20).ToString();
            documentRequest.CTEXTOEXTRA2 = new StringBuilder(paciente.Telefono + ", Edad: " + paciente.Edad, 20).ToString();
            documentRequest.CTEXTOEXTRA3 = new StringBuilder(paciente.Domicilio, 20).ToString();
            documentRequest.COBSERVACIONES = $"Medico: {medico.Nombre} Diagnostico: {diagnostico.Contenido}";
            documentRequest.aSerie = "RH";
            documentRequest.aCodConcepto = "REM3";
            documentRequest.aCodigoCteProv = room.Descripcion;
            documentRequest.aTipoCambio = 1;
            documentRequest.aFecha = caso.FechaIngreso.ToString("MM/dd/yyyy");
            documentRequest.aReferencia = $"{room.Nombre} {caso.Id}";
            documentRequest.aCodAlmacen = "1";
            documentRequest.aCodProdSer = "_HPENSION01";

            //MemoryStream memory = new MemoryStream(); this is just in case the response is extend

            try
            {
                // Serializa el objeto DocumentoDTO a JSON
                var jsonData = JsonConvert.SerializeObject(documentRequest);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Realiza una solicitud POST
                var response = await httpClient.PostAsync(url, content);

                // Asegúrate de que la respuesta sea exitosa
                response.EnsureSuccessStatusCode();

                // Lee el contenido de la respuesta
                var responseBody = await response.Content.ReadAsStringAsync();
                     var apiresponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

                // Accede al único valor en el diccionario y retórnalo
                return apiresponse.FolioDocumento;
            }
            catch
            {
                return 0;
            }

        }
    }
}

