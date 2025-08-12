using Microsoft.AspNetCore.Http;
using Regix.DomainLogic.FileHandler;

namespace Regix.AppInfra.FileHelper;

public interface IFileStorage
{
    //Manejo de Imagenes para AZURE Containers

    Task<bool> RemoveFileAsync(string containerName, string fileName);

    Task<FileBase64Result?> GetFileBase64Async(string? fileName, string containerName);

    //Para Guardado en Disco

    Task<string> UploadImage(IFormFile imageFile, string ruta, string guid);

    Task<string> UploadImage(byte[] imageFile, string ruta, string guid);

    bool DeleteImage(string ruta, string guid);

    Task<string> SaveImageAsync(byte[] content, string fileName, string containerName);

    Task<string> SaveFileAsync(byte[] content, string fileName, string containerName, string fileNameOriginal, string mimeTypeEnviado);
}