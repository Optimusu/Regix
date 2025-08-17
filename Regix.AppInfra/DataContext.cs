using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Regix.Domain.EntitesSoftSec;
using Regix.Domain.Entities;
using Regix.Domain.EntitiesGen;
using Regix.Domain.EntitiesSoft;

namespace Regix.AppInfra;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    //EntitiesSoftSec

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<UsuarioRole> UsuarioRoles => Set<UsuarioRole>();

    //Manejo de UserRoles por Usuario

    public DbSet<UserRoleDetails> UserRoleDetails => Set<UserRoleDetails>();

    //Entities

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<State> States => Set<State>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<SoftPlan> SoftPlans => Set<SoftPlan>();
    public DbSet<Manager> Managers => Set<Manager>();
    public DbSet<Corporation> Corporations => Set<Corporation>();

    //EntitiesGen

    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
    public DbSet<EstadoCivil> EstadoCivils => Set<EstadoCivil>();
    public DbSet<IdentidadGenero> IdentidadGeneros => Set<IdentidadGenero>();
    public DbSet<SexoAsignado> SexoAsignados => Set<SexoAsignado>();
    public DbSet<Idioma> Idiomas => Set<Idioma>();
    public DbSet<Pharmacy> Pharmacies => Set<Pharmacy>();
    public DbSet<Anticonception> Anticonceptions => Set<Anticonception>();
    public DbSet<Regular> Regulars => Set<Regular>();

    //EntitiesSoft

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Patient2> Patient2s => Set<Patient2>();
    public DbSet<PatientControl> PatientControls => Set<PatientControl>();
    public DbSet<Ginecologico> Ginecologicos => Set<Ginecologico>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Para tomar los calores de ConfigEntities
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}