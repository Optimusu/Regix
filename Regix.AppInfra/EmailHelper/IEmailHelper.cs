﻿using Regix.DomainLogic.ResponsesSec;
using Regix.DomainLogic.TrialResponse;

namespace Regix.AppInfra.EmailHelper;

public interface IEmailHelper
{
    //Sistema para el envio de Correos Electronicos
    Task<bool> EnviarAsync(ContactViewDTO contacto);

    //Sistema para Confirmar las Cuentas de Usuario desde el Correo
    Task<Response> ConfirmarCuenta(string to, string NameCliente, string subject, string body);
}