using FilesStorage.Grpc.Cotracts.Arguments;
using FilesStorage.Sdk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SeedWork.Gateway.Extensions;
using Shared.Common.Extensions;
using Shared.Common.Net;
using Shared.Grpc.Models;
using Shared.Rest.Common.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Eas.Gate.Images.Controllers;

/// <summary>
/// API по работе со картинками
/// </summary>
[Route("images")]
[ApiVersion("1.0")]
[EnableCors(CorsPolicyName.ALLOW_ANONYMOUS_POLICY)]
public class ImagesController : ApiControllerBase
{
    private readonly FilesStorageClient filesStorageClient;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ImagesController"/>
    /// </summary>
    public ImagesController(FilesStorageClient filesStorageClient)
    {
        this.filesStorageClient = filesStorageClient;
    }

    /// <summary>
    /// Вернуть изображение
    /// </summary>
    /// <param name="imageId">Имя изображения</param>
    /// <remarks>
    /// Возвращает изображение
    /// </remarks>
    [HttpGet("{imageId}")]
    [AllowAnonymous]
    public async Task GetImage([FromRoute, Required] Guid imageId)
    {
        var args = new DownloadGrpcArgs
        {
            FileId = imageId,
        };
        var asyncResponse = filesStorageClient.Download(args);

        await HttpContext.PushFileFromGrpc(asyncResponse, (resp) => resp.FileName);
    }

    [HttpPost()]
    [AllowAnonymous]
    [Consumes(MediaTypeNamesEx.Multipart.FormData)]
    public async Task<Guid> SaveImage([Required] IFormFile file, [FromForm, Required] string imgName)
    {
        var meta = new SaveGrpcArgs
        {
            FileName = imgName,
        };

        await using var stream = file.OpenReadStream();
        var chunks = stream.GetChunks(meta, (buffer, meta) =>
            new StreamChunk<SaveGrpcArgs>
            {
                Chunk = buffer,
                Meta = meta,
            });

        var resp = await filesStorageClient.Save(chunks);

        return resp.FileId;
    }

}