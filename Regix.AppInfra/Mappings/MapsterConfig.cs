using Mapster;
using Regix.Domain.Entities;
using Regix.Domain.EntitiesSoft;

namespace Regix.AppInfra.Mappings;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<Manager, Manager>()
             .Ignore(dest => dest.Corporation!);

        config.NewConfig<Patient, Patient>()
            .Ignore(dest => dest.Corporation!)
            .Ignore(dest => dest.SexoAsignado!)
            .Ignore(dest => dest.IdentidadGenero!)
            .Ignore(dest => dest.DocumentType!)
            .Ignore(dest => dest.Idioma!)
            .Ignore(dest => dest.Pharmacy!)
            .Ignore(dest => dest.EstadoCivil!);

        config.NewConfig<Patient2, Patient2>()
            .Ignore(dest => dest.Corporation!);

        config.NewConfig<PatientControl, PatientControl>()
            .Ignore(dest => dest.Patients!)
            .Ignore(dest => dest.Patient2s!);
    }
}