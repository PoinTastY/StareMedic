using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using StareMedic.Models.Entities;

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
        public string SerialEntity1 { get; set; }
        public string SerialEntity2 { get; set; }
        public string SerialEntity3 { get; set; }
        public string SerialEntity4 { get; set; }
        public string SerialEntity5 { get; set; }
        public string SerialEntity6 { get; set; }
        public string RawRequest { get; set; }

        public int FillPackAndPush(CasoClinico caso, Patient paciente, Rooms room, Medic medico, Diagnostico diagnostico)
        {
            Request req = new(Work);
            TcpClient client = new TcpClient("26.116.39.19", 42069);
            NetworkStream stream = client.GetStream();
            SDK.tDocumento docto = new SDK.tDocumento();
            docto.aCodConcepto = "REM3";
            docto.aCodigoCteProv = "CU" + room.Id;
            docto.aSerie = "RH";
            docto.aTipoCambio = 1;
            docto.aFecha = DateTime.Today.ToString("MM/dd/yyyy");
            docto.aReferencia = $"{room.Nombre} {caso.Id} tst";

            req.SerialDocto = JsonConvert.SerializeObject(docto);
            req.SerialEntity1 = JsonConvert.SerializeObject(caso);
            req.SerialEntity2 = JsonConvert.SerializeObject(paciente);
            req.SerialEntity3 = JsonConvert.SerializeObject(room);
            req.SerialEntity4 = JsonConvert.SerializeObject(medico);
            req.SerialEntity5 = JsonConvert.SerializeObject(diagnostico);

            byte[] requestBuffer = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(req));
            stream.Write(requestBuffer, 0, requestBuffer.Length);
            byte[] responseBuffer = new byte[1024];
            int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
            string response = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
            Response objresponse = JsonConvert.DeserializeObject<Response>(response);
            if (objresponse.ResponseCode != 0)
                return (int)objresponse.ResponseCode;
            return 0;
        }
    }
}

