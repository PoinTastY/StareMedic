using iText;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using StareMedic.Models.Entities;
using System.Text;
using Path = iText.Kernel.Geom.Path;

namespace StareMedic.Models.Documents
{
    public static class GenerateAdmisionDoc
    {
        private readonly static string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Exported";
        private readonly static int line_length = 68;
        public static string GenerateDocument(CasoClinico caso)
        {
            try
            {
                Rooms room = MainRepo.GetRoomById(caso.IdHabitacion);
                Medic doctor = MainRepo.GetMedicById(caso.IdDoctor);
                Patient patient = MainRepo.GetPatientById(caso.IdPaciente);
                Diagnostico diag = MainRepo.GetDiagnosticoById(caso.IdDiagnostico);

                var writer = new PdfWriter(_path + $"\\Admision_{caso.Id}.pdf");

                var pdf = new PdfDocument(writer);

                var document = new Document(pdf, PageSize.A4);

                PdfCanvas pdfCanvas = new PdfCanvas(pdf.AddNewPage());

                Canvas canvas = new Canvas(pdfCanvas, new Rectangle(0, 0, 595, 842));
                canvas.SetFontSize(8);

                var no_de_cuarto = new Paragraph($"{room.Nombre}").SetMaxWidth(80).SetFixedPosition(90, 785, 80);
                canvas.Add(no_de_cuarto);

                var hora = new Paragraph($"{DateTime.Now.ToShortTimeString()}").SetMaxWidth(60).SetFixedPosition(220, 785, 60);
                canvas.Add(hora);

                var no_expediente = new Paragraph($"{caso.Id}").SetMaxWidth(200).SetFixedPosition(345, 785, 200);
                canvas.Add(no_expediente);

                var fecha_ingreso = new Paragraph($"{caso.FechaIngreso:dd/MM/yyyy}").SetMaxWidth(150).SetFixedPosition(90, 770, 150);
                canvas.Add(fecha_ingreso);

                var sexo = new Paragraph($"{patient.Sexo}").SetMaxWidth(50).SetFixedPosition(335, 770, 50);
                canvas.Add(sexo);

                var edad = new Paragraph($"{patient.Edad}").SetMaxWidth(150).SetFixedPosition(420, 770, 150);
                canvas.Add(edad);

                var nombre = new Paragraph($"{patient.Nombre}").SetMaxWidth(150).SetFixedPosition(90, 755, 150);
                canvas.Add(nombre);

                var estado_civil = new Paragraph($"{patient.EstadoCivil}").SetMaxWidth(50).SetFixedPosition(340, 755, 50);
                canvas.Add(estado_civil);

                var nacionalidad = new Paragraph($"{patient.Nacionalidad}").SetMaxWidth(150).SetFixedPosition(420, 755, 150);
                canvas.Add(nacionalidad);

                document.Close();
                return "Exito";
            }
            catch(Exception e)
            {
                return e.ToString();
            }

        }
    }
}
