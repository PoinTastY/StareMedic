﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using StareMedic.Models.Entities;
using System.Diagnostics;
using Element = iTextSharp.text.Element;

namespace StareMedic.Models
{
    public static class DoCreate
    {
        private readonly static string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Exported");
        public static bool GenerateDocument(CasoClinico CasoReferenciado)
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            Patient patient = MainRepo.GetPatientById(CasoReferenciado.IdPaciente);
            Cercano cercano = MainRepo.GetCercanoById(patient.IdCercano);
            Fiador fiador = MainRepo.GetFiadorById(patient.IdFiador);


            //this thing can be done later with an html or sometghing more simple lol
            Document doc = new(PageSize.LETTER);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(_path + $"\\Admision {CasoReferenciado.Id}.pdf", FileMode.Create));
                doc.Open();

                GenerarDataParaPlantilla(CasoReferenciado, patient, cercano, fiador, doc, writer);

                //                //header
                //                Paragraph HojaAdmision = new(@"HOJA DE ADMISION
                //", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f));
                //                HojaAdmision.Alignment = Element.ALIGN_CENTER;
                //                doc.Add(HojaAdmision);

                //                //Hoja de admision
                //                doc.Add(GenerateContentTable(CasoReferenciado, patient, cercano, fiador));

                //                //Diagnosis
                //                doc.Add(Diagnosis(CasoReferenciado.IdDiagnostico));

                //                //logo and clausules
                //                doc.Add(logoYtal(fiador.Nombre));

                //                //clausulas
                //                doc.Add(Clausule());

                //                //signs
                //                doc.Add(Sign());

                //                //sumario clinico
                //                doc.NewPage();
                //                Paragraph SumarioClinico = new(@"HOJA DE SUMARIO CLINICO
                //", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f));

                //                SumarioClinico.Alignment = Element.ALIGN_CENTER;
                //                doc.Add(SumarioClinico);
                //                doc.Add(GenerateContentTable(CasoReferenciado, patient, cercano, fiador));

                //                doc.Add(SumarioTemplate(CasoReferenciado, MainRepo.GetMedicById(CasoReferenciado.IdDoctor)));


                //close file
                doc.Close();
                OpenPdf(_path + $"\\Admision {CasoReferenciado.Id}.pdf");
                return true;
            }
            catch
            {
                return false;
            }

        }

        private static PdfPTable GenerateContentTable(CasoClinico CasoReferenciado, Patient patient, Cercano cercano, Fiador fiador)
        {

            //font for the table
            //if (patient.IdCercano > 0)
            //{
            //    cercano = MainRepo.GetCercanoById(patient.IdCercano);
            //}
            //if (patient.IdFiador > 0)
            //{
            //    fiador = MainRepo.GetFiadorById(patient.IdFiador);
            //    _pagare = fiador.Nombre;
            //}
            Medic medic = MainRepo.GetMedicById(CasoReferenciado.IdDoctor);
            Rooms room = MainRepo.GetRoomById(CasoReferenciado.IdHabitacion);

            //font set
            iTextSharp.text.Font fuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f);

            //table shit
            PdfPTable displayinfo = new(5);
            displayinfo.DefaultCell.Border = Rectangle.NO_BORDER;

            displayinfo.AddCell(new Phrase("Caso Clinico: ", fuente)); displayinfo.AddCell(new PdfPCell(new Phrase(CasoReferenciado.Id, fuente)) { Colspan = 2, Border = Rectangle.NO_BORDER });
            displayinfo.AddCell(new Phrase($"Fecha de ingreso: {CasoReferenciado.FechaIngreso:dd/MM/yyyy}", fuente));
            displayinfo.AddCell(new Phrase($"Tipo de caso: {CasoReferenciado.TipoCaso}", fuente));//br

            ///////////////////////////////////////////////////////////new row
            displayinfo.AddCell(new Phrase("Nombre del paciente: ", fuente)); PdfPCell Col2 = new(new Phrase(patient.Nombre, fuente)); Col2.Colspan = 2;
            Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            displayinfo.AddCell(new Phrase($"Sexo: {patient.Sexo}", fuente)); displayinfo.AddCell(new Phrase($"Edad: {patient.Edad}", fuente));//br

            ///////////////////////////////////////////////////////////new row
            displayinfo.AddCell(new Phrase("Estado Civil: ", fuente)); Col2 = new(new Phrase(patient.EstadoCivil, fuente)); Col2.Colspan = 2;
            Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            displayinfo.AddCell(new Phrase("Nacionalidad: ", fuente)); displayinfo.AddCell(new Phrase(patient.Nacionalidad, fuente));//br

            ///////////////////////////////////////////////////////////new row
            displayinfo.AddCell(new Phrase("Domicilio: ", fuente)); Col2 = new(new Phrase(patient.SubDomicilio(), fuente)); Col2.Colspan = 2;
            Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            displayinfo.AddCell(new Phrase("Telefono: ", fuente)); displayinfo.AddCell(new Phrase(patient.Telefono, fuente));//br

            ///////////////////////////////////////////////////////////new row
            displayinfo.AddCell(new Phrase("Ciudad: ", fuente)); Col2 = new(new Phrase(patient.Ciudad, fuente)); Col2.Colspan = 2;
            Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            displayinfo.AddCell(new Phrase("Estado: ", fuente)); displayinfo.AddCell(new Phrase(patient.Estado, fuente));//br

            ///////////////////////////////////////////////////////////new row
            displayinfo.AddCell(new Phrase("Contacto Cercano: ", fuente)); Col2 = new(new Phrase(cercano.Nombre, fuente)); Col2.Colspan = 2;
            Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            displayinfo.AddCell(new Phrase("Relacion: ", fuente)); displayinfo.AddCell(new Phrase($"{cercano.Relacion}", fuente));//br

            ///////////////////////////////////////////////////////////new row
            displayinfo.AddCell(new Phrase("Domicilio: ", fuente)); Col2 = new(new Phrase(cercano.SubDireccion(), fuente)); Col2.Colspan = 2;
            Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            displayinfo.AddCell(new Phrase("Telefono: ", fuente)); displayinfo.AddCell(new Phrase(cercano.Telefono, fuente));//br

            ///////////////////////////////////////////////////////////new row
            displayinfo.AddCell(new Phrase("Ciudad: ", fuente)); Col2 = new(new Phrase(cercano.Ciudad, fuente)); Col2.Colspan = 2;
            Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            displayinfo.AddCell(new Phrase("Estado: ", fuente)); displayinfo.AddCell(new Phrase(cercano.Estado, fuente));//br

            ///////////////////////////////////////////////////////////new row
            displayinfo.AddCell(new Phrase("Medico: ", fuente)); Col2 = new(new Phrase(medic.Nombre, fuente)); Col2.Colspan = 2;
            Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            displayinfo.AddCell(new Phrase("Telefono: ", fuente)); displayinfo.AddCell(new Phrase(medic.Telefono, fuente));//br

            ///////////////////////////////////////////////////////////new row
            displayinfo.AddCell(new Phrase("Habitacion: ", fuente)); Col2 = new(new Phrase(room.Nombre, fuente)); Col2.Colspan = 4;
            Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);//br

            ///////////////////////////////////////////////////////////new row REMOVED FIADOR UWU
            //displayinfo.AddCell(new Phrase("Fiador: ", fuente)); Col2 = new(new Phrase(fiador.Nombre, fuente)); Col2.Colspan = 2;
            //Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            //displayinfo.AddCell(new Phrase("Telefono: ", fuente)); displayinfo.AddCell(new Phrase(fiador.Telefono, fuente));//br

            /////////////////////////////////////////////////////////////new row
            //displayinfo.AddCell(new Phrase("Domicilio: ", fuente)); Col2 = new(new Phrase(fiador.Direccion, fuente)); Col2.Colspan = 4;
            //Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);//br

            /////////////////////////////////////////////////////////////new row
            //displayinfo.AddCell(new Phrase("Ciudad: ", fuente)); Col2 = new(new Phrase(fiador.Ciudad, fuente)); Col2.Colspan = 2;
            //Col2.Border = Rectangle.NO_BORDER; displayinfo.AddCell(Col2);
            //displayinfo.AddCell(new Phrase("Estado: ", fuente)); displayinfo.AddCell(new Phrase(fiador.Estado, fuente));//br
            //displayinfo.WidthPercentage = 90f;

            return displayinfo;
        }

        private static Paragraph Diagnosis(int diagnoose)
        {
            Paragraph body = new()
            {
                new Phrase($"Diagnostico:\n{MainRepo.GetDiagnosticoById(diagnoose).Contenido}", FontFactory.GetFont(FontFactory.HELVETICA, 8f)),
                //maybe styling needed here
            };

            return body;
        }

        private static PdfPTable logoYtal(string patient)
        {
            //first paragraph & logo image
            iTextSharp.text.Font fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6f);
            PdfPTable logoClausules = new(5);
            string contratoText = @$"
CONTRATO DE PRESTACION DE SERVICIOS HOSPITALARIOS CELEBRADO POR EL ""HOSPITAL DR. MANUEL MONTERO"" A QUIEN EN LO SUCESIVO SE LE DENOMINARA ""EL HOSPITAL"",
Y POR LA OTRA PARTE DOMICILIO EN ""{patient}"" 
A QUIEN SE LE DENOMINARA ""EL PACIENTE"", Y QUE CELEBRARAN MEDIANTE LAS SIGUIENTES:
";
            //TODO: add logo
            PdfPCell cell = new(new Phrase(contratoText, fnt));
            cell.Colspan = 4; cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            logoClausules.AddCell(cell);
            string baseDirectory = AppContext.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Resources\\Images\\logo_hospital.png");


            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(filePath);


            logo.ScaleToFit(60f, 60f);
            cell = new(); cell.Border = Rectangle.NO_BORDER;
            cell.AddElement(logo);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            logoClausules.AddCell(cell);
            logoClausules.WidthPercentage = 100f;

            return logoClausules;
        }

        private static Paragraph Clausule()
        {
            iTextSharp.text.Font fnt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6f);
            Paragraph body = new();


            //Clausulas subtitle centered
            Paragraph Clausulas = new("CLAUSULAS", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11f));
            Clausulas.Alignment = Element.ALIGN_CENTER;

            //actual clausules
            body.Add(Clausulas);
            body.Add(new Chunk(@"
PRIMERA: ", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 6f)));
            body.Add(new Chunk(@"EL HOSPITAL SE OBLIGA A PRESTAR A ""EL PACIENTE"" LOS SIGUIENTES SERVICIOS HOSPITALARIOS: CUARTOS, SERVICIOS DE ENFERMERIA, DIETA PRESCRITA POR EL MEDICO QUE ATIENDE AL PACIENTE.", fnt));
            body.Add(new Chunk(@"
SEGUNDA: ", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 6f)));
            body.Add(new Chunk(@"""EL PACIENTE"" SE OBLIGA A PAGAR A ""EL HOSPITAL"" EL IMPORTE TOTAL DE LOS SERVICIOS ANTES MENCIONADOS, INCLUYENDO LOS DERIVADOS DE RAYOS X, LABORATORIO, MEDICINAS, MATERIAL DE CURACION, TERAPA INTENSIVA, Y DEMAS QUE SEAN SOLICITADOS OPR EL MEDICO DE ""EL PACIENTE"", CUYOS GASTOS SE CARGARAN EN FORMA ADICIONAL, EN LA CUENTA RESPECTIVA.", fnt));
            body.Add(new Chunk(@"
TERCERA: ", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 6f)));
            body.Add(new Chunk(@"""EL PACIENTE"" SE ENTREGA EN ESTE ACTO A ""EL HOSPITAL"" EN CALIDAD DE ANTICIPO LA CANTIDAD DE: $
EN MONEDA NACIONAL, Y SE OBLIGA A HACER PAGOS SEMANALES POR LOS GASTOS INCURRIDOS Y LIQUIDAR EL REMANENTE TOTAL DE LA CUENTA AL SER DADO DE ALTA POR SU MEDICO O AL RETIRARSE DE ""EL HOSPITAL"" POR CUALQUIER MOTIVO.", fnt));
            body.Add(new Chunk(@"
CUARTA: ", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 6f)));
            body.Add(new Chunk(@"""EL PACIENTE"" SE OBLIGA A CUMPLIR EL REGLAMENTO INTERNO Y DEMAS DISPOSICIONES DE ""EL HOSPITAL"" Y COMO ESTE ES UNA INSTITUCION ABIERTA A CUALQUIER CUERPO MEDICO, LO RELEVA DE CUALQUIER RESPONSABILIDAD MEDICA.", fnt));
            body.Add(new Chunk(@"
QUINTA: ", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 6f)));
            body.Add(new Chunk(@"""EL PACIENTE"" AUTORIZA AL MEDICO Y SUS COLABORADORES PARA QUE PRESCRIBAN, LLEVEN A CABO EL TRATAMIENTO MEDICO Y QUIRURGICO QUE REQUIERA EN ATENCION DE SU PERSONA, ASI COMO LA ADMINISTRACION DE MEDICAMENTOS Y ANESTESICOS PREESCRITOS.", fnt));
            body.Add(new Chunk(@"
SEXTA: ", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 6f)));
            body.Add(new Chunk(@"""AMBAS PARTES CONVIENEN QUE EN CASO DE QUE ""EL PACIENTE"" ESTE INCAPACITADO PARA FIRMAR ESTE CONTRATO, LO HARA EN SU NOMBRE Y REPRESENTACION, DE LA PERSONA QUE SE RESPONSABILICE EN EL CUMPLIMIENTO DE LAS OBLIGACIONES ANTERIORMENTE ESTABLECIDAS.", fnt));

            //sangria
            body.SetLeading(0, 0.8f);

            return body;
        }

        //signatures
        private static Paragraph Sign()
        {
            Paragraph signatures = new();
            signatures.Add(new Chunk(@$"
SAN JUAN DE LOS LAGOS, JAL., A: {DateTime.Now:D}


                                     ______________________________                                         ______________________________       
                                                      ""EL PACIENTE""                                                                            ""EL HOSPITAL""
", FontFactory.GetFont(FontFactory.HELVETICA, 8f)));
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            signatures.Add(new Chunk("PAGARE", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f)));
            string pagareText = @"
POR EL PRESENTE PAGARE DEBO, Y PAGARE INCONDICIONALMENTE AL HOSPITAL DR MANUEL MONTERO, EN EL LUGAR QUE SE ME REQUIERA,
LA CANTIDAD DE:
IMPORTE DE LOS SERVICIOS DETALLADOS EN ESTE TITULO DE CREDITO, QUE GENERA INTERESES A RAZON DEL            % MENSUALES";

            signatures.Add(new Chunk(pagareText, FontFactory.GetFont(FontFactory.HELVETICA, 7f)));
            signatures.Add(new Chunk(@"



                                                                ______________________________
                                                                                      ""FIRMA""
", FontFactory.GetFont(FontFactory.HELVETICA, 10f)));
            signatures.SetLeading(0, 1f);

            return signatures;
        }

        private static Paragraph SumarioTemplate(CasoClinico caso, Medic medico)
        {
            Paragraph template = new();

            //replace Servicio del DR field with the actual doctor name with bold font maybe

            //recreate remaining fields
            Paragraph previos = new("INGRESOS ANTERIORES AL HOSPITAL", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11f));
            previos.Alignment = Element.ALIGN_CENTER;
            template.Add(previos);
            previos = new(" FECHA                      DIAGNOSTICO                                           RESULTADO               FECHA DE INTERNACION", FontFactory.GetFont(FontFactory.HELVETICA, 10f));
            template.Add(previos);
            template.Add(new Chunk(@"

1. ____________________________________________________________________________

2. ____________________________________________________________________________

3. ____________________________________________________________________________

4. ____________________________________________________________________________
"));
            Paragraph actual = new("INGRESO ACTUAL", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11f));
            actual.Alignment = Element.ALIGN_CENTER;
            template.Add(actual);

            template.Add(new Chunk(@$"
Servicio del DR: {medico.Nombre}.
Diagnostico de Admision: ________________________________________________________

Diagnostico Final: ______________________________________________________________

Complicaciones: _______________________________________________________________

Resultado (condicion de incapacidad):______________________________________________

Fecha de ingreso del Paciente:{caso.FechaIngreso:dd/MM/yyyy}

Fecha de alta:________________________  Por orden de:______________________________
Duracion de la                                                  Fecha en que podra 
internacion:__________________________  regresar al trabajo: _________________________
Ameritara ser internado, 
pero fue dado de alta a causa de:__________________________________________________

Documentos
Completos:     ______________________                                 _______________________       
                                ""Jefe de Archivo""                                                 ""Firma del Medico"""));

            return template;
        }
        private static void OpenPdf(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir el PDF: {ex.Message}");
            }
        }

        private static void GenerarDataParaPlantilla(CasoClinico CasoReferenciado, Patient patient, Cercano cercano, Fiador fiador, Document doc, PdfWriter writer)
        {
            PdfContentByte ct = writer.DirectContent;

            ct.BeginText();

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            ct.SetFontAndSize(bf, 9);

            ct.SetTextMatrix(110, 56);
            ct.ShowText(MainRepo.GetRoomById(CasoReferenciado.IdHabitacion).Nombre);
            ct.SetTextMatrix(227, 56);
            //the current hour:
            DateTime now = DateTime.Now;
            ct.ShowText(now.ToString("t"));
        }

    }

}
