//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MetalPerformanceShaders;
using StareMedic.Data;
using StareMedic.Models.Entities;

namespace StareMedic.Models
{
    public static class MainRepo
    {
        private static Patient _tmpPatient = new();

        //dbshit
        static AppDbContext _db = new();

        //private readonly static List<Patient> _patients = _db.patients.ToList();
        //private readonly static List<Fiador> _fiadores = _db.fiadores.ToList();
        //private readonly static List<Cercano> _cercanos = _db.cercanos.ToList();
        //private readonly static List<Rooms> _rooms = _db.rooms.ToList();
        //private readonly static List<Medic> _medicos = _db.medics.ToList();
        //private readonly static List<CasoClinico> _casos = _db.casoClinicos.ToList();
        //private readonly static List<Diagnostico> _diagnosticos = _db.diagnosticos.ToList();

        //getters
        public static List<Patient> GetPatients() { return _db.Patients.ToList(); }
        public static List<Fiador> GetFiadores() => _db.Fiadores.ToList();//igual, 2 formas de declararse xd
        public static List<Cercano> GetCercanos() { return _db.Cercanos.ToList(); }
        public static List<Rooms> GetRooms() { return _db.Rooms.ToList(); }
        public static List<CasoClinico> GetCasos() => _db.CasoClinicos.ToList();
        public static List<Medic> GetMedics() => _db.Medics.ToList();
        public static List<Diagnostico> GetDiagnosticos() => _db.Diagnosticos.ToList();

        //setters
        public static void AddPatient(Patient patient)
        {
            Patient patient1 = patient;
            // Agrega el paciente al contexto de la base de datos
            _db.Patients.Add(patient);
            // Guarda los cambios en la base de datos
            _db.SaveChanges();
            //exception of encoding mans that some fields cant be null bcs it violates encoding stuff

            //solved: fix null shit
        }

        public static void AddFiador(Fiador fiador)
        {
            _db.Fiadores.Add(fiador);
            _db.SaveChanges();
        }

        public static void AddCercano(Cercano cercano)
        {
            _db.Cercanos.Add(cercano);
            _db.SaveChanges();
        }

        public static void AddRoom(Rooms room)
        {
            _db.Rooms.Add(room);
            _db.SaveChanges();
        }

        public static void AddCaso(CasoClinico caso)
        {
            _db.CasoClinicos.Add(caso);
            _db.SaveChanges();
        }

        public static void AddMedic(Medic medic)
        {
            _db.Medics.Add(medic);
            _db.SaveChanges();
        }

        public static void AddDiagnostico(Diagnostico diagnostico)
        {
            _db.Diagnosticos.Add(diagnostico);
            _db.SaveChanges();
        }

        //finders
        public static Patient GetPatientById(int patientId)
        {
            return _db.Patients.FirstOrDefault(x => x.Id == patientId);
        }

        public static Fiador GetFiadorById(int? fiadorId)
        {
            if (fiadorId == null) { return new Fiador(); }
            return _db.Fiadores.FirstOrDefault(x => x.Id == fiadorId);
        }

        public static Cercano GetCercanoById(int? cercanoId)
        {
            if (cercanoId == null) { return new Cercano(); }
            return _db.Cercanos.FirstOrDefault(x => x.Id == cercanoId);
        }

        public static Rooms GetRoomById(int roomId)
        {
            Rooms roomreturned = _db.Rooms.FirstOrDefault(x => x.Id == roomId);
            if (roomreturned == null)
            {
                roomreturned = new();
                return roomreturned;
            }
            else
                return roomreturned;
        }

        public static CasoClinico GetCasoById(string caseId)
        {
            return _db.CasoClinicos.FirstOrDefault(x => x.Id == caseId);
        }

        public static Medic GetMedicById(int medicId)
        {
            Medic medreturned = _db.Medics.FirstOrDefault(x => x.Id == medicId);
            if (medreturned == null)
            {
                medreturned= new();
                return medreturned;
            }else
                return medreturned;
        }

        public static Diagnostico GetDiagnosticoById(int diagnosticoId)
        {
            return _db.Diagnosticos.FirstOrDefault(x => x.Id == diagnosticoId);
        }

        //indexers
        public static int GetCurrentPatientIndex()//Implement exception handling here
        {
            if (!_db.Patients.Any()) { return 1; }
            // Obtén el valor máximo actual de la clave primaria (ID) en la tabla de pacientes
            var maxId = _db.Patients.Max(p => (int?)p.Id);

            // Si no hay registros en la tabla, el valor máximo será nulo, así que devuelve 1 como valor predeterminado
            return (maxId ?? 0) + 1;//check dis out
        }

        public static int GetCurrentCercanoIndex()
        {
            if (!_db.Cercanos.Any()) { return 1; }
            var maxId = _db.Cercanos.Max(p => (int?)p.Id);
            return (maxId ?? 0) + 1;
        }

        public static int GetCurrentFiadorIndex()
        {
            if(!_db.Fiadores.Any()) { return 1; }//check dis out
            var maxId = _db.Fiadores.Max(p => (int?)p.Id);
            return (maxId ?? 0) + 1;
        }
        
        public static int GetCurrentRoomIndex()
        {
            if(!_db.Rooms.Any()) { return 1; }
            var maxId = _db.Rooms.Max(p => (int?)p.Id);
            return (maxId ?? 0) + 1;
        }

        public static int GetCurrentCaseIndex()
        {
            if(!_db.CasoClinicos.Any()) { return 1; }//check dis out
            var maxId = _db.CasoClinicos.Max(p => (int?)p.IdDB);
            return (maxId ?? 0) + 1;
        }

        public static int GetCurrentMedicIndex()
        {
            if(!_db.Medics.Any()) { return 1; }//check dis out
            var maxId = _db.Medics.Max(p => (int?)p.Id);
            return (maxId ?? 0) + 1;
        }

        public static int GetCurrentDiagnosticoIndex()
        {
            if(!_db.Diagnosticos.Any()) { return 1; }//check dis out
            var maxId = _db.Diagnosticos.Max(p => (int?)p.Id);
            return (maxId ?? 0) + 1;
        }

        //solvers
        public static Patient PatientIdSolver
        {
            get { return _tmpPatient; }
            set { _tmpPatient = value; }
        }

        //updaters
        public static void UpdatePatientStatus(bool stts,int id)
        {
            var patient2update = _db.Patients.FirstOrDefault(x => x.Id == id);
            patient2update.Status = stts;
            _db.Entry(patient2update).CurrentValues.SetValues(patient2update);
            _db.SaveChanges();

        }

        public static void UpdateRoomStatus(bool stts, int id)
        {
            var room2update = _db.Rooms.FirstOrDefault(x => x.Id == id);
            room2update.Status = stts;
            _db.Entry(room2update).CurrentValues.SetValues(room2update);
            _db.SaveChanges();
        }

        public static void UpdatePatient(Patient patient)
        {

            var patient2update = _db.Patients.FirstOrDefault(x => x.Id == patient.Id);
            //Confirm that dis works with db working
            patient2update.Update(patient);
            _db.Entry(patient2update).CurrentValues.SetValues(patient2update);
            _db.SaveChanges();

        }

        //implement update entities here
        public static void UpdateDiagnoose(Diagnostico diagnostico)
        {
            var diag2update = _db.Diagnosticos.FirstOrDefault(x => x.Id == diagnostico.Id);
            _db.Entry(diag2update).CurrentValues.SetValues(diagnostico);
            _db.SaveChanges();

        }

        public static void UpdateClinicalCase(CasoClinico caso)
        {

            var case2update = _db.CasoClinicos.FirstOrDefault(x => x.Id == caso.Id);
            _db.Entry(case2update).CurrentValues.SetValues(caso);
            _db.SaveChanges();


        }

        //closers
        public static void CloseCase(int id)
        {
            var case2close = _db.CasoClinicos.FirstOrDefault(x => x.IdDB == id);
            case2close.Activo = false;
            case2close.FechaAlta = DateTimeOffset.UtcNow;
            UpdatePatientStatus(false, case2close.IdPaciente);
            _db.Entry(case2close).CurrentValues.SetValues(case2close);
            _db.SaveChanges();
            //maybe implement destructor here? or write on db

        }

        public static void ReopenCase(int id)
        {
            var case2reopen = _db.CasoClinicos.FirstOrDefault(x => x.IdDB == id);
            case2reopen.Activo = true;
            case2reopen.FechaAlta = null;
            UpdatePatientStatus(true, case2reopen.IdPaciente);
            _db.SaveChanges();

        }

        //deleters
        public static void DeleteClinicalCase(CasoClinico casodel)
        {
            var case2delete = _db.CasoClinicos.FirstOrDefault(x => x == casodel);
            _db.CasoClinicos.Remove(case2delete);
            DeleteDiagnoose(casodel.IdDiagnostico);
            UpdatePatientStatus(false, casodel.IdPaciente);
            _db.SaveChanges();
        }

        private static void DeleteDiagnoose(int id)
        {
            var diag2delete = _db.Diagnosticos.FirstOrDefault(x => x.Id == id);
            _db.Diagnosticos.Remove(diag2delete);
            _db.SaveChanges();
        }
    }
}
