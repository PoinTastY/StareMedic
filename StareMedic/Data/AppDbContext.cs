using Microsoft.EntityFrameworkCore;
using StareMedic.Models.Entities;

namespace StareMedic.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<CasoClinico> CasoClinicos { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medic> Medics { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Fiador> Fiadores { get; set; }
        public DbSet<Cercano> Cercanos { get; set; }


        static readonly string server = "localhost";
        static readonly string db = "staremedic";
        static readonly string user = "admin";
        static readonly string pass = "staremedic1";
        static readonly string port = "5432";

        readonly string connString = $"Server={server};Port={port};User Id={user};Password={pass};Database={db};";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connString);
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

            modelBuilder.Entity<Fiador>()
                .Property(f => f.Relacion)
                .HasColumnName("_relation");


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
                .Property(p => p.Domicilio)
                .HasColumnName("_domicilio");

            modelBuilder.Entity<Patient>()
                .Property(p => p.TipoSangre)
                .HasColumnName("_tiposangre");

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
                .Property(p => p.RegisteredDate)
                .HasColumnName("_fecharegistrado");

            modelBuilder.Entity<Patient>()
                .Property(p => p.RegisteredHour)
                .HasColumnName("_horaregistrado");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Status)
                .HasColumnName("_active");

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

                //relation to patient
            modelBuilder.Entity<CasoClinico>()
                .HasOne<Patient>()
                .WithOne()
                .HasForeignKey<CasoClinico>(c => c.IdPaciente);
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.IdPaciente)
                .HasColumnName("_idpacient");

            //relation to diagnostic
            modelBuilder.Entity<CasoClinico>()
                .HasOne<Diagnostico>()
                .WithOne()
                .HasForeignKey<CasoClinico>(c => c.IdDiagnostico);
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.IdDiagnostico)
                .HasColumnName("_iddiagnostico");

                //relation to medic
            modelBuilder.Entity<CasoClinico>()
                .HasOne<Medic>()
                .WithOne()
                .HasForeignKey<CasoClinico>(c => c.IdDoctor);
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.IdDoctor)
                .HasColumnName("_iddoctor");

                //relation to room
            modelBuilder.Entity<CasoClinico>()
                .HasOne<Rooms>()
                .WithOne()
                .HasForeignKey<CasoClinico>(c => c.IdHabitacion);
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.IdHabitacion)
                .HasColumnName("_idhabitacion");

            //other properties CC
            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.Nombre)
                .HasColumnName("_name");

            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.Activo)
                .HasColumnName("_status");

            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.FechaIngresoDate)
                .HasColumnName("_fechaingreso");

            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.FechaIngresoHour)
                .HasColumnName("_horaingreso");

            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.FechaAltaDate)
                .HasColumnName("_fechaalta");

            modelBuilder.Entity<CasoClinico>()
                .Property(c => c.FechaAltaHour)
                .HasColumnName("_horaalta");

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

            modelBuilder.Entity<Rooms>()
                .Property(d => d.Status)
                .HasColumnName("_status");

        }

    }
}
