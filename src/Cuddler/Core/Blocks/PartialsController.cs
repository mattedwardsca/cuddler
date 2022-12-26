using System.Text.Json;
using Cuddler.Core.Attributes;
using Cuddler.Core.Context;
using Cuddler.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Core.Blocks;

/// <summary>
///     This API is used to save razor template settings from the browser
/// </summary>
[Authorize]
[Route("Apis/[controller]")]
public class PartialsController : BaseController
{
    private readonly IRazorPartialToStringRenderer _renderer;

    protected PartialsController(IRepository repository, IRazorPartialToStringRenderer renderer) : base(repository)
    {
        _renderer = renderer;
    }

    [WrapOutput]
    [HttpPost(nameof(GetInputs))]
    public async Task<ActionResult> GetInputs(string contextId, string inputs)
    {
        if (string.IsNullOrEmpty(contextId))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(contextId));
        }

        if (string.IsNullOrEmpty(inputs))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(inputs));
        }

        var inputList = DeserializeInputs(inputs);

        var cuddlerForm = Repository.DbSet<BlockEntity>()
                                    .SingleOrDefault(w => w.ContextId == contextId && w.DateArchived == null);

        if (cuddlerForm == null)
        {
            var dateCreated = DateTime.UtcNow;
            cuddlerForm = new BlockEntity
            {
                Id = Guid.NewGuid()
                         .ToString(),
                Inputs = inputs,
                ArchiveReason = null,
                DateArchived = null,
                DateCreated = dateCreated,
                DateUpdated = dateCreated,
                ContextId = contextId
            };

            Repository.Add(cuddlerForm);
            await Repository.SaveChangesAsync();
        }

        var paritalList = DeserializeInputs(cuddlerForm.Inputs);
        CopyInputs(paritalList, inputList);

        var BlockEntity = new BlockModel();
        BlockEntity.Inputs = inputList;
        BlockEntity.SubmitApiUrl = $"/Apis/Partials/{nameof(SaveInputs)}?{nameof(contextId)}={contextId}";

        var model = new HtmlModel
        {
            Html = await _renderer.RenderPartialToStringAsync("Library/Helpers/Partial", BlockEntity)
        };

        return Json(model);
    }

    [WrapOutput]
    [HttpPost(nameof(SaveInputs))]
    public async Task<IActionResult> SaveInputs(string contextId)
    {
        if (string.IsNullOrEmpty(contextId))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(contextId));
        }

        await Task.CompletedTask;

        var cuddlerForm = Repository.DbSet<BlockEntity>()
                                    .Single(w => w.ContextId == contextId && w.DateArchived == null);

        var inputList = DeserializeInputs(cuddlerForm.Inputs)
            .ToList();

        var form = await HttpContext.Request.ReadFormAsync();

        // Update values
        foreach (var inputModel in inputList)
        {
            //if (FormDictionary.ContainsKey(inputModel.Name))
            //{
            //    inputModel.Value = FormDictionary[inputModel.Name] as string;
            //}
            //else
            //{
            //    inputModel.Value = null;
            //}

            if (inputModel.Children != null)
            {
                foreach (var childInput in inputModel.Children)
                {
                    if (form.ContainsKey(childInput.Name))
                    {
                        childInput.Value = form[childInput.Name]
                            .FirstOrDefault();
                    }
                    else
                    {
                        childInput.Value = null;
                    }
                }
            }
        }

        cuddlerForm.Inputs = SerializeInputs(inputList);
        await Repository.SaveChangesAsync();

        return Json(true);
    }

    protected override async Task<bool> HeartbeatTests()
    {
        await Task.CompletedTask;

        return true;
    }

    private static void CopyInputs(List<TemplatesInputModel> sourceList, List<TemplatesInputModel> destinationList)
    {
        if (sourceList == null)
        {
            throw new ArgumentNullException(nameof(sourceList));
        }

        if (destinationList == null)
        {
            throw new ArgumentNullException(nameof(destinationList));
        }

        // Supports only 2 levels
        foreach (var sourceParent in sourceList)
        {
            if (sourceParent.Children != null)
            {
                var destinationParent = destinationList.SingleOrDefault(w => w.Name == sourceParent.Name);
                if (destinationParent?.Children != null)
                {
                    foreach (var sourceChild in sourceParent.Children)
                    {
                        var destinationChild = destinationParent.Children.SingleOrDefault(w => w.Name == sourceChild.Name);
                        if (destinationChild != null)
                        {
                            destinationChild.Value = sourceChild.Value;
                        }
                    }
                }
            }
        }
    }

    private static List<TemplatesInputModel> DeserializeInputs(string? inputs)
    {
        if (string.IsNullOrEmpty(inputs))
        {
            return (List<TemplatesInputModel>)Activator.CreateInstance(typeof(List<TemplatesInputModel>))!;
        }

        var deserializeObject = (List<TemplatesInputModel>)JsonSerializer.Deserialize(inputs, typeof(List<TemplatesInputModel>))!;

        return deserializeObject.ToList();
    }

    private static string SerializeInputs(IEnumerable<TemplatesInputModel> list)
    {
        return JsonSerializer.Serialize(list);
    }
}