using iText;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using StareMedic.Models.Entities;
using System.Text;
using Path = iText.Kernel.Geom.Path;

namespace StareMedic.Models
{
    public static class GenerateAdmisionDoc
    {
        private readonly static string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Exported";
        private readonly static int line_length = 68;
        public static bool GenerateDocument(CasoClinico caso)
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
                document.SetMargins(42, 100, 76, 100);

                document.SetFontSize(8);

                var no_de_cuarto = new StringBuilder($"{room.Nombre}".PadRight(14), 14);
                var space = new StringBuilder("      ", 6);
                var hora = new StringBuilder($"{DateTime.Now.ToShortTimeString()}".PadRight(8), 8);
                var space2 = new StringBuilder("           ", 11);
                var no_expediente = new StringBuilder($"{caso.Id}".PadRight(29), 29);

                var first_line = new Paragraph(no_de_cuarto.ToString() + space.ToString() + hora.ToString() + space2.ToString() + no_expediente.ToString());
                var second_line = new Paragraph("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");


                document.Add(first_line);
                document.Add(second_line);

                document.Close();
                return true;
            }
            catch
            {
                return false;
            } 

        }
    }
}
