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

            var domicilio_paciente = new Paragraph($"{s_patient.Domicilio}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 730, normal_width);
            canvas.Add(domicilio_paciente);

            var telefono_paciente = new Paragraph($"{s_patient.Telefono}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_2, 730, normal_width);
            canvas.Add(telefono_paciente);

            var ciudad_estado = new Paragraph($"{s_patient.Ciudad}, {s_patient.Estado}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 715, normal_width);
            canvas.Add(ciudad_estado);

            var relacion = new Paragraph($"{s_cercano.Relacion}").SetMaxWidth(50).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_2, 715, 50);
            canvas.Add(relacion);

            var telefono_cercano = new Paragraph($"{s_cercano.Telefono}").SetMaxWidth(normal_width).SetFixedPosition(columna_3, 715, normal_width);
            canvas.Add(telefono_cercano);

            var nombre_cercano = new Paragraph($"{s_cercano.Nombre}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 695, normal_width);
            canvas.Add(nombre_cercano);

            var domicilio_cercano = new Paragraph($"{s_cercano.Direccion}").SetMaxWidth(200).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 685, 200);
            canvas.Add(domicilio_cercano);

            var ciudad_estado_cercano = new Paragraph($"{s_cercano.Ciudad}, {s_cercano.Estado}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 665, normal_width);
            canvas.Add(ciudad_estado_cercano);

            var caso_clinico = new Paragraph($"{caso.Nombre}").SetMaxWidth(170).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_2, 665, 170);
            canvas.Add(caso_clinico);

            var nombre_doctor = new Paragraph($"{s_medic.Nombre}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 650, normal_width);
            canvas.Add(nombre_doctor);

            var nombre_fiador = new Paragraph($"{s_cercano.Nombre}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 630, normal_width);
            canvas.Add(nombre_fiador);

            var telefono_fiador = new Paragraph($"{s_cercano.Telefono}").SetMaxWidth(normal_width).SetFixedPosition(columna_2, 635, normal_width);
            canvas.Add(telefono_fiador);

            var domicilio_fiador = new Paragraph($"{s_cercano.Direccion}").SetMaxWidth(200).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 625, 200);
            canvas.Add(domicilio_fiador);

            var ciudad_estado_fiador = new Paragraph($"{s_cercano.Ciudad}, {s_cercano.Estado}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 600, normal_width);
            canvas.Add(ciudad_estado_fiador);

            var diagnostico = new Paragraph($"{s_diag.Contenido}").SetMaxWidth(400).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 580, 400);
            canvas.Add(diagnostico);

            var denominacion_paciente = new Paragraph($"{s_patient.Nombre}").SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 510, 200);
            canvas.Add(denominacion_paciente);

            var fecha_hoy = new Paragraph($"{DateTime.Now:dd/MM/yyyy}").SetMaxHeight(altura_parrafo).SetMaxWidth(200).SetFixedPosition(220, 240, 200);
            canvas.Add(fecha_hoy);
        }

        private static void SumarioClinico(Canvas canvas, CasoClinico caso)
        {
            int columna_1 = 90;
            int columna_2 = 350;
            int columna_3 = 440;
            int altura_parrafo = 20;
            int normal_width = 160;

            var no_de_cuarto = new Paragraph($"{s_room.Nombre}").SetMaxWidth(80).SetFixedPosition(columna_1, 785, 80);
            canvas.Add(no_de_cuarto);

            var hora = new Paragraph($"{DateTime.Now.ToShortTimeString()}").SetMaxWidth(60).SetFixedPosition(220, 785, 60);
            canvas.Add(hora);

            var no_expediente = new Paragraph($"{caso.Id}").SetMaxWidth(200).SetFixedPosition(345, 785, 200);
            canvas.Add(no_expediente);

            var fecha_ingreso = new Paragraph($"{caso.FechaIngreso:dd/MM/yyyy}").SetMaxWidth(normal_width).SetFixedPosition(columna_1, 770, normal_width);
            canvas.Add(fecha_ingreso);

            var sexo = new Paragraph($"{s_patient.Sexo}").SetMaxWidth(50).SetFixedPosition(columna_2, 770, 50);
            canvas.Add(sexo);

            var edad = new Paragraph($"{s_patient.Edad}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_3, 770, normal_width);
            canvas.Add(edad);

            var nombre_paciente = new Paragraph($"{s_patient.Nombre}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 755, normal_width);
            canvas.Add(nombre_paciente);

            var estado_civil = new Paragraph($"{s_patient.EstadoCivil}").SetMaxWidth(50).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_2, 755, 50);
            canvas.Add(estado_civil);

            var nacionalidad = new Paragraph($"{s_patient.Nacionalidad}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_3, 755, normal_width);
            canvas.Add(nacionalidad);

            var domicilio_paciente = new Paragraph($"{s_patient.Domicilio}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 730, normal_width);
            canvas.Add(domicilio_paciente);

            var telefono_paciente = new Paragraph($"{s_patient.Telefono}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_2, 730, normal_width);
            canvas.Add(telefono_paciente);

            var ciudad_estado = new Paragraph($"{s_patient.Ciudad}, {s_patient.Estado}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 715, normal_width);
            canvas.Add(ciudad_estado);

            var relacion = new Paragraph($"{s_cercano.Relacion}").SetMaxWidth(50).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_2, 715, 50);
            canvas.Add(relacion);

            var telefono_cercano = new Paragraph($"{s_cercano.Telefono}").SetMaxWidth(normal_width).SetFixedPosition(columna_3, 715, normal_width);
            canvas.Add(telefono_cercano);

            var nombre_cercano = new Paragraph($"{s_cercano.Nombre}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 695, normal_width);
            canvas.Add(nombre_cercano);

            var domicilio_cercano = new Paragraph($"{s_cercano.Direccion}").SetMaxWidth(200).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 685, 200);
            canvas.Add(domicilio_cercano);

            var ciudad_estado_cercano = new Paragraph($"{s_cercano.Ciudad}, {s_cercano.Estado}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 665, normal_width);
            canvas.Add(ciudad_estado_cercano);

            var caso_clinico = new Paragraph($"{caso.Nombre}").SetMaxWidth(170).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_2, 665, 170);
            canvas.Add(caso_clinico);

            var nombre_doctor = new Paragraph($"{s_medic.Nombre}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 650, normal_width);
            canvas.Add(nombre_doctor);

            var nombre_fiador = new Paragraph($"{s_cercano.Nombre}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 630, normal_width);
            canvas.Add(nombre_fiador);

            var telefono_fiador = new Paragraph($"{s_cercano.Telefono}").SetMaxWidth(normal_width).SetFixedPosition(columna_2, 635, normal_width);
            canvas.Add(telefono_fiador);

            var domicilio_fiador = new Paragraph($"{s_cercano.Direccion}").SetMaxWidth(200).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 625, 200);
            canvas.Add(domicilio_fiador);

            var ciudad_estado_fiador = new Paragraph($"{s_cercano.Ciudad}, {s_cercano.Estado}").SetMaxWidth(normal_width).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 600, normal_width);
            canvas.Add(ciudad_estado_fiador);

            var diagnostico = new Paragraph($"{s_diag.Contenido}").SetMaxWidth(400).SetMaxHeight(altura_parrafo).SetFixedPosition(columna_1, 580, 400);
            canvas.Add(diagnostico);

            //TODO: Add remaining body content
        }

        private static void OpenPdf(string filePath)
        {
            const int maxRetries = 5;
            const int delay = 500;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    // Verificar si el archivo existe
                    if (File.Exists(filePath))
                    {
                        // Intentar abrir el archivo para lectura y luego cerrarlo inmediatamente
                        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            // Si se puede abrir el archivo, continuar para abrirlo con Process.Start
                            break;
                        }
                    }
                }
                catch (IOException)
                {
                    // Si hay una excepción de IO, esperar antes de reintentar
                    Thread.Sleep(delay);
                }
            }

            try
            {
                // Verificar si el archivo existe
                if (File.Exists(filePath))
                {
                    // Intentar abrir el archivo para lectura y luego cerrarlo inmediatamente
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        // Si se puede abrir el archivo, continuar para abrirlo con Process.Start
                    }
                }
                else
                {
                    //si este ultimo intento falla, no se puede abrir el archivo
                    return;
                }
            }
            catch (IOException)
            {
                return;
            }

        }
    }
}
