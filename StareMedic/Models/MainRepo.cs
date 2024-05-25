using StareMedic.Data;
using StareMedic.Models.Entities;

namespace StareMedic.Models
{
    public static class MainRepo
    {

        //dbcontext lul
        static AppDbContext _db = new();

        #region Get Lists
        //getters
        public static List<Patient> GetPatients(int cantidadElementos, int paginaActual) 
        {
            return _db.Patients.OrderBy(p => p.Nombre).Skip((paginaActual - 1) * cantidadElementos).Take(cantidadElementos).ToList(); 
        }//the framework sents an order by clause to the db, so eficiency is not impacted
        //public static List<Fiador> GetFiadores() => _db.Fiadores.ToList();//igual, 2 formas de declararse xd // not used
        //public static List<Cercano> GetCercanos() { return _db.Cercanos.ToList(); } not used 4 now
        public static List<Rooms> GetRooms() 
        {
            //return _db.Rooms.Skip((paginaActual - 1) * cantidadElementos).Take(cantidadElementos).ToList(); not 4 now
            return _db.Rooms.ToList();
        }
        public static List<CasoClinico> GetCasos(int cantidadElementos, int paginaActual)
        {
            return _db.CasoClinicos
              .OrderByDescending(c => c.IdDB)  // Ordenar por el campo deseado en orden descendente
              .Skip((paginaActual - 1) * cantidadElementos)
              .Take(cantidadElementos)
              .ToList(); ;
        }
        public static List<Medic> GetMedics()
        {
            //return _db.Medics.Skip((paginaActual - 1) * cantidadElementos).Take(cantidadElementos).ToList(); we need it full 4 now
            return _db.Medics.ToList();
        }

        public static List<Medic> GetMedicsLight(int paginaActual, int cantidadElementos)
        {
            return _db.Medics.OrderBy(m => m.Nombre).Skip((paginaActual - 1) * cantidadElementos).Take(cantidadElementos).ToList();
        }
        //public static List<Diagnostico> GetDiagnosticos() => _db.Diagnosticos.ToList(); no needed

        #endregion

        #region AddRows
        public static bool AddPatient(Patient patient)
        {
            try
            {
                if (!(patient.Id <= GetCurrentPatientIndex() - 1))
                {
                    _db.Patients.Add(patient);
                    _db.SaveChanges();
                }
                else
                {
                    UpdatePatient(patient);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddFiador(Fiador fiador)
        {
            try
            {
                if (!(fiador.Id <= GetCurrentFiadorIndex() - 1))
                {
                    _db.Fiadores.Add(fiador);
                    _db.SaveChanges();
                }
                else
                {
                    UpdateFiador(fiador);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddCercano(Cercano cercano)
        {
            try
            {
                if (!(cercano.Id <= GetCurrentCercanoIndex() - 1))
                {
                    _db.Cercanos.Add(cercano);
                    _db.SaveChanges();
                }
                else
                {
                    UpdateCercano(cercano);
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static void AddRoom(Rooms room)//this thing is not used 4 now
        {
            if(!(room.Id <= GetCurrentRoomIndex()-1))
            {
                _db.Rooms.Add(room);
                _db.SaveChanges();
            }
            else
            {
                UpdateRoom(room.Id);
            }
        }

        public static bool AddCaso(CasoClinico caso)
        {
            try
            {
                _db.CasoClinicos.Add(caso);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static bool AddMedic(Medic medic)
        {
            try
            {
                if (!(medic.Id <= GetCurrentMedicIndex() - 1))
                {
                    _db.Medics.Add(medic);
                    _db.SaveChanges();
                }
                else
                {
                    UpdateMedic(medic);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddDiagnostico(Diagnostico diagnostico)
        {
            try
            {
                _db.Diagnosticos.Add(diagnostico);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Getters
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

        public static CasoClinico GetCasoById(int caseId)
        {
            return _db.CasoClinicos.FirstOrDefault(x => x.IdDB == caseId);
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
        #endregion

        #region GetIndexes
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

        #endregion


        #region UpdateRows

        public static void UpdatePatient(Patient patient)
        {

            var patient2update = _db.Patients.FirstOrDefault(x => x.Id == patient.Id);
            patient2update.Update(patient);
            _db.Entry(patient2update).CurrentValues.SetValues(patient2update);
            _db.SaveChanges();

        }

        public static void UpdateFiador(Fiador fiador)
        {
            var fiador2update = _db.Fiadores.FirstOrDefault(x => x.Id == fiador.Id);
            _db.Entry(fiador2update).CurrentValues.SetValues(fiador);
            _db.SaveChanges();

        }

        public static void UpdateCercano(Cercano cercano)
        {
            var cercano2update = _db.Cercanos.FirstOrDefault(x => x.Id == cercano.Id);
            _db.Entry(cercano2update).CurrentValues.SetValues(cercano);
            _db.SaveChanges();

        }

        public static void UpdateRoom( int id)
        {
            var room2update = _db.Rooms.FirstOrDefault(x => x.Id == id);
            _db.Entry(room2update).CurrentValues.SetValues(room2update);
            _db.SaveChanges();

        }

        public static void UpdateMedic(Medic medic)
        {
            var medic2update = _db.Medics.FirstOrDefault(x => x.Id == medic.Id);
            _db.Entry(medic2update).CurrentValues.SetValues(medic);
            _db.SaveChanges();

        }

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
        #endregion


        #region DeleteRows
        //deleters

        public static bool DeletePatient(Patient patient)
        {
            var validate = SearchCasoClinico(patient.Nombre, 1);
            if(validate.Count > 0)
            {
                return false;
            }
            var cercano2delete = _db.Cercanos.FirstOrDefault(x => x.Id == patient.IdCercano);
            var fiador2delete = _db.Fiadores.FirstOrDefault(x => x.Id == patient.IdFiador);
            var patient2delete = _db.Patients.FirstOrDefault(x => x.Id == patient.Id);
            _db.Cercanos.Remove(cercano2delete);
            _db.Fiadores.Remove(fiador2delete);
            _db.Patients.Remove(patient2delete);
            _db.SaveChanges();
            return true;
        }

        public static void DeleteClinicalCase(CasoClinico casodel)
        {
            var case2delete = _db.CasoClinicos.FirstOrDefault(x => x == casodel);
            _db.CasoClinicos.Remove(case2delete);
            DeleteDiagnoose(casodel.IdDiagnostico);
            _db.SaveChanges();
        }

        private static void DeleteDiagnoose(int id)
        {
            var diag2delete = _db.Diagnosticos.FirstOrDefault(x => x.Id == id);
            _db.Diagnosticos.Remove(diag2delete);
            _db.SaveChanges();
        }

        public static bool DeleteMedic(int id)
        {
            var validate = SearchCasoClinico(GetMedicById(id).Nombre, 4);
            if(validate.Count > 0)
                return false;
            var medic2delete = _db.Medics.FirstOrDefault(x => x.Id == id);
            _db.Medics.Remove(medic2delete);
            _db.SaveChanges();
            return true;
        }

        public static void DeleteRoom(int id)
        {
            var room2delete = _db.Rooms.FirstOrDefault(x => x.Id == id);
            _db.Rooms.Remove(room2delete);
            _db.SaveChanges();
        }
        #endregion

        #region Search

        public static List<Patient> SearchPatient(string nombrePaciente)
        {
            return _db.Patients.Where(p => p.Nombre.Contains(nombrePaciente)).ToList();
        }

        public static List<Medic> SearchMedic(string nombreMedico)
        {
            return _db.Medics.Where(m => m.Nombre.Contains(nombreMedico)).ToList();
        }

        public static List<CasoClinico> SearchCasoClinico(string datoCasoClinico, int searchCriteria)
        {
            if(searchCriteria == 1)
            {
                //search by paciente we need this join
                var query = from casoclinico in _db.CasoClinicos
                            join patient in _db.Patients
                            on casoclinico.IdPaciente equals patient.Id
                            where patient.Nombre.Contains(datoCasoClinico)
                            select casoclinico;

                return query.ToList();
            }
            else if (searchCriteria == 2)
            {
                //search by name
                return _db.CasoClinicos.Where(c => c.Nombre.Contains(datoCasoClinico)).ToList();
            }
            else if (searchCriteria == 3)
            {
                //search by id
                return _db.CasoClinicos.Where(c => c.Id.Contains(datoCasoClinico)).ToList();
            }
            else
            {
                var query = from casoclinico in _db.CasoClinicos
                            join medic in _db.Medics
                            on casoclinico.IdDoctor equals medic.Id
                            where medic.Nombre.Contains(datoCasoClinico)
                            select casoclinico;

                return query.ToList();
            }
        }
        #endregion
    }
}
