using CuddlerDev.Data.Context;
using CuddlerDev.Web.Links;
using CuddlerDev.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CuddlerDev.Web.Controllers;

public abstract class DocumentsController : BaseController
{
    protected readonly IDocumentsService DocumentsService;

    protected DocumentsController(IRepository repository, IDocumentsService documentsService) : base(repository)
    {
        DocumentsService = documentsService;
    }

    [HttpPost(nameof(Archive))]
    public async Task<IActionResult> Archive([FromQuery] string id)
    {
        await DocumentsService.ArchiveDocument(id);
        return Json(new { id });
    }

    [HttpGet(nameof(Download))]
    [Route("Download/{id}")]
    public async Task<IActionResult> Download([FromRoute] string id)
    {
        var content = DocumentsService.GetFileMemoryStream(id, out var title);
        const string contentType = "APPLICATION/octet-stream";
        await Task.CompletedTask;
        return File(content, contentType, title);
    }

    [HttpGet(nameof(Image))]
    [Route("Image/{id}")]
    public async Task<IActionResult> Image([FromRoute] string id, int w)
    {
        if (string.IsNullOrEmpty(id) || "null".Equals(id, StringComparison.InvariantCultureIgnoreCase))
        {
            return Redirect(ThumbnailLinks.Package);
        }

        if (w < 1)
        {
            w = 400;
        }

        try
        {
            var inMemoryDocument = await DocumentsService.GetImage(id, w);

            Response.Headers.Add("Content-Disposition", $"inline; filename={inMemoryDocument.Document.Title}");

            var contentType = inMemoryDocument.Document.Extension is "gif"
                ? "image/gif"
                : "image/jpeg";

            return File(inMemoryDocument.FileBytes, contentType);
        }
        catch (FileNotFoundException)
        {
            return Redirect(ThumbnailLinks.Package);
        }
    }

}