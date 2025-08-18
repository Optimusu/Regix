using Regix.Services.ImplementEntties;
using Regix.Services.ImplementGen;
using Regix.Services.ImplementSecure;
using Regix.Services.ImplementSoft;
using Regix.Services.InterfaceEntities;
using Regix.Services.InterfacesGen;
using Regix.Services.InterfaceSoft;
using Regix.Services.InterfacesSecure;
using Regix.UnitOfWork.ImplementEntities;
using Regix.UnitOfWork.ImplementGen;
using Regix.UnitOfWork.ImplementSecure;
using Regix.UnitOfWork.ImplementSoft;
using Regix.UnitOfWork.InterfaceEntities;
using Regix.UnitOfWork.InterfacesGen;
using Regix.UnitOfWork.InterfaceSoft;
using Regix.UnitOfWork.InterfacesSecure;

namespace Trial.AppBack.DependencyInjection
{
    public class UnitOfWorkRegistration
    {
        public static void AddUnitOfWorkRegistration(IServiceCollection services)
        {
            //EntitiesSecurities Software
            services.AddScoped<IAccountUnitOfWork, AccountUnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUsuarioUnitOfWork, UsuarioUnitOfWork>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRoleUnitOfWork, UsuarioRoleUnitOfWork>();
            services.AddScoped<IUsuarioRoleService, UsuarioRoleService>();

            //Entities
            services.AddScoped<ICountryUnitOfWork, CountryUnitOfWork>();
            services.AddScoped<ICountryServices, CountryService>();
            services.AddScoped<IStateUnitOfWork, StateUnitOfWork>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityUnitOfWork, CityUnitOfWork>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ISoftPlanUnitOfWork, SoftPlanUnitOfWork>();
            services.AddScoped<ISoftPlanService, SoftPlanService>();
            services.AddScoped<ICorporationUnitOfWork, CorporationUnitOfWork>();
            services.AddScoped<ICorporationService, CorporationService>();
            services.AddScoped<IManagerUnitOfWork, ManagerUnitOfWork>();
            services.AddScoped<IManagerService, ManagerService>();

            //EntitiesGen
            services.AddScoped<IDocumentTypeUnitOfWork, DocumentTypeUnitOfWork>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<IEstadoCivilUnitOfWork, EstadoCivilUnitOfWork>();
            services.AddScoped<IEstadoCivilService, EstadoCivilService>();
            services.AddScoped<IIdentidadGeneroUnitOfWork, IdentidadGeneroUnitOfWork>();
            services.AddScoped<IIdentidadGeneroService, IdentidadGeneroService>();
            services.AddScoped<ISexoAsignadoUnitOfWork, SexoAsignadoUnitOfWork>();
            services.AddScoped<ISexoAsignadoService, SexoAsignadoService>();
            services.AddScoped<IIdiomaUnitOfWork, IdiomaUnitOfWork>();
            services.AddScoped<IIdiomaService, IdiomaService>();
            services.AddScoped<IPharmacyUnitOfWork, PharmacyUnitOfWork>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IAnticonceptionUnitOfWork, AnticonceptionUnitOfWork>();
            services.AddScoped<IAnticonceptionService, AnticonceptionService>();
            services.AddScoped<IRegularUnitOfWork, RegularUnitOfWork>();
            services.AddScoped<IRegularService, RegularService>();
            services.AddScoped<IEtniaRazaUnitOfWork, EtniaRazaUnitOfWork>();
            services.AddScoped<IEtniaRazaService, EtniaRazaService>();
            services.AddScoped<IDiscapacidadUnitOfWork, DiscapacidadUnitOfWork>();
            services.AddScoped<IDiscapacidadService, DiscapacidadService>();
            services.AddScoped<IVeteranUnitOfWork, VeteranUnitOfWork>();
            services.AddScoped<IVeteranService, VeteranService>();

            //EntitiesSoft
            services.AddScoped<IPatientUnitOfWork, PatientUnitOfWork>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatient2UnitOfWork, Patient2UnitOfWork>();
            services.AddScoped<IPatient2Service, Patient2Service>();
            services.AddScoped<IPatient3UnitOfWork, Patient3UnitOfWork>();
            services.AddScoped<IPatient3Service, Patient3Service>();
            services.AddScoped<IPatientControlUnitOfWork, PatientControlUnitOfWork>();
            services.AddScoped<IPatientControlService, PatientControlService>();
            services.AddScoped<IGinecologoUnitOfWork, GinecologoUnitOfWork>();
            services.AddScoped<IGinecologoService, GinecologoService>();
        }
    }
}