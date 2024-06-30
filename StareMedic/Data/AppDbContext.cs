using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StareMedic.Models;
using StareMedic.Models.Entities;


namespace StareMedic.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CasoClinico> CasoClinicos { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medic> Medics { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Fiador> Fiadores { get; set; }
        public DbSet<Cercano> Cercanos { get; set; }

        //es: localhost // 192.168.3.3 // 26.101.17.190
        //Check config.json
        //$"Server={server};Port={port};User Id={user};Password={pass};Database={db};";
        private string connString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                string confPath = Path.Combine(AppContext.BaseDirectory, "Data/config.json"); // Ruta completa al archivo JSON
                                                                                              // Verificar si el archivo existe
                if (!File.Exists(confPath))
                {
                    return;
                }

                // Leer el contenido del archivo
                string jsonString = File.ReadAllText(confPath);

                // Verificar que el contenido no sea nulo ni vacío
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    return;
                }

                // Deserializar el contenido en un objeto Config
                Config config = JsonConvert.DeserializeObject<Config>(jsonString);

                connString = config.ConnString;

                optionsBuilder.UseNpgsql(connString);
            }
            catch (Exception ex)
            {
                // Manejar la excepción, por ejemplo, escribir un mensaje en el registro
                Console.WriteLine($"Error al configurar la base de datos: {ex.Message}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CasoClinico>()
                .ToTable("casoclinico")
                .HasKey(c => c.IdDB);
            modelBuilder.Entity<CasoClinico>()
            .Ignore(c => c.Id);

            //Cercano entity
            modelBuilder.Entity<Cercano>()
                .ToTable("cercano") // Especifica el nombre de la tabla en la base de datos
                .HasKey(c => c.Id); // Configura la clave primaria

            modelBuilder.Entity<Cercano>()
                .Property(c => c.Id)
                .HasColumnName("_id"); // Mapea el campo "_id" en la base de datos a la propiedad "Id"

            modelBuilder.Entity<Cercano>()
                .Property(c => c.Nombre)
                .HasColumnName("_name");

            modelBuilder.Entity<Cercano>()
                .Property(c => c.Telefono)
                .HasColumnName("_phone");

            modelBuilder.Entity<Cercano>()
                .Property(c => c.Direccion)
                .HasColumnName("_address");

            modelBuilder.Entity<Cercano>()
                .Property(c => c.Ciudad)
                .HasColumnName("_city");

            modelBuilder.Entity<Cercano>()
                .Property(c => c.Estado)
                .HasColumnName("_state");

            modelBuilder.Entity<Cercano>()
                .Property(c => c.Relacion)
                .HasColumnName("_relation");

            //Fiador entity
            modelBuilder.Entity<Fiador>()
                .ToTable("fiador") // Especifica el nombre de la tabla en la base de datos
                .HasKey(f => f.Id); // Configura la clave primaria

            modelBuilder.Entity<Fiador>()
                .Property(f => f.Id)
                .HasColumnName("_id"); // Mapea el campo "_id" en la base de datos a la propiedad "Id"

            modelBuilder.Entity<Fiador>()
                .Property(f => f.Nombre)
                .HasColumnName("_name");

            modelBuilder.Entity<Fiador>()
                .Property(f => f.Telefono)
                .HasColumnName("_phone");

            modelBuilder.Entity<Fiador>()
                .Property(f => f.Direccion)
                .HasColumnName("_address");

            modelBuilder.Entity<Fiador>()
                .Property(f => f.Ciudad)
                .HasColumnName("_city");

            modelBuilder.Entity<Fiador>()
                .Property(f => f.Estado)
                .HasColumnName("_state");



            //patient entity
            modelBuilder.Entity<Patient>()
                .ToTable("patient") // Especifica el nombre de la tabla en la base de datos
                .HasKey(p => p.Id); // Configura la clave primaria

            modelBuilder.Entity<Patient>()
                .Property(p => p.Id)
                .HasColumnName("_id");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Nombre)
                .HasColumnName("_name");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Curp)
                .HasColumnName("_curp");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Domicilio)
                .HasColumnName("_domicilio");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Ciudad)
                .HasColumnName("_ciudad");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Estado)
                .HasColumnName("_estado");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Nacionalidad)
                .HasColumnName("_nacionalidad");

            modelBuilder.Entity<Patient>()
                .Property(p => p.EstadoCivil)
                .HasColumnName("_estadocivil");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Sexo)
                .HasColumnName("_sexo");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Edad)
                .HasColumnName("_edad");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Telefono)
                .HasColumnName("_telefono");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Registered)
                .HasColumnName("_fecharegistrado")
                .HasColumnType("timestamp with time zone")
                .IsRequired(true);

            modelBuilder.Entity<Patient>()
                .Property(p => p.IdCercano)
                .HasColumnName("_idcercano");

            modelBuilder.Entity<Patient>()
                .Property(p => p.IdFiador)
                .HasColumnName("_idfiador");

            // Configura las relaciones con las tablas "cercano" y "fiador"
            modelBuilder.Entity<Patient>()
                .HasOne<Cercano>()
                .WithOne()
                .HasForeignKey<Patient>(p => p.IdCercano);

            modelBuilder.Entity<Patient>()
                .HasOne<Fiador>()
                .WithOne()
                .HasForeignKey<Patient>(p => p.IdFiador);



            //clinical case entity
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.IdDB)
                .HasColumnName("_iddb");
            modelBuilder.Entity<CasoClinico>()
                .ToTable("casoclinico")
                .HasKey(c => c.IdDB);

            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.Id)
                .HasColumnName("_id");

            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.TipoCaso)
                .HasColumnName("_tipocaso");

            //relation to patient
            modelBuilder.Entity<CasoClinico>()
                .HasMany<Patient>()
                .WithMany();
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.IdPaciente)
                .HasColumnName("_idpacient")
                .IsRequired(true);

            //relation to diagnostic
            modelBuilder.Entity<CasoClinico>()
                .HasOne<Diagnostico>()
                .WithOne()
                .HasForeignKey<CasoClinico>(c => c.IdDiagnostico);
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.IdDiagnostico)
                .HasColumnName("_iddiagnostico")
                .IsRequired(true);

            //relation to medic
            modelBuilder.Entity<CasoClinico>()
                .HasMany<Medic>()
                .WithMany();
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.IdDoctor)
                .HasColumnName("_iddoctor");

            //relation to room
            modelBuilder.Entity<CasoClinico>()
                .HasMany<Rooms>()
                .WithMany();
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.IdHabitacion)
                .HasColumnName("_idhabitacion");

            //other properties CC
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.Nombre)
                .HasColumnName("_name");

            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.FechaIngreso)
                .HasColumnName("_fechaingreso")
                .HasColumnType("timestamp with time zone")
                .IsRequired(true);
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.FolioSDK)
                .HasColumnName("_foliosdk");


            //diagnostic entity
            modelBuilder.Entity<Diagnostico>()
                .ToTable("diagnostico")
                .HasKey(d => d.Id);
            modelBuilder.Entity<Diagnostico>()
                .Property(d => d.Id)
                .HasColumnName("_id");

            modelBuilder.Entity<Diagnostico>()
                .Property(d => d.Contenido)
                .HasColumnName("_content");

            //medic entity
            modelBuilder.Entity<Medic>()
                .ToTable("medic")
                .HasKey(d => d.Id);

            modelBuilder.Entity<Medic>()
                .Property(d => d.Id)
                .HasColumnName("_id");

            modelBuilder.Entity<Medic>()
                .Property(d => d.Nombre)
                .HasColumnName("_name");

            modelBuilder.Entity<Medic>()
                .Property(d => d.Telefono)
                .HasColumnName("_phone");

            modelBuilder.Entity<Medic>()
                .Property(p => p.Domicilio)
                .HasColumnName("_domicilio");

            modelBuilder.Entity<Medic>()
                .Property(p => p.Ciudad)
                .HasColumnName("_ciudad");

            modelBuilder.Entity<Medic>()
                .Property(p => p.Estado)
                .HasColumnName("_estado");

            //room entity
            modelBuilder.Entity<Rooms>()
                .ToTable("rooms")
                .HasKey(d => d.Id);
            modelBuilder.Entity<Rooms>()
                .Property(d => d.Id)
                .HasColumnName("_id");

            modelBuilder.Entity<Rooms>()
                .Property(d => d.Nombre)
                .HasColumnName("_name");

            modelBuilder.Entity<Rooms>()
                .Property(d => d.Descripcion)
                .HasColumnName("_description");
        }

    }
}
