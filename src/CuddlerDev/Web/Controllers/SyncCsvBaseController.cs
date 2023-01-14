using System.Globalization;
using System.Text;
using CsvHelper;
using CuddlerDev.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuddlerDev.Web.Controllers;

public abstract class SyncCsvBaseController<T> : BaseController where T : class
{
    private readonly string _listType;

    protected SyncCsvBaseController(IRepository repository, string listType) : base(repository)
    {
        _listType = listType;
    }

    [HttpGet(nameof(Export))]
    public async Task<IActionResult> Export()
    {
        var products = ExportRecords();

        var fileDownloadName = GetFileDownloadName();

        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
        var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        csvWriter.WriteHeader<T>();
        csvWriter.WriteRecords(products);
        csvWriter.Flush();

        await Task.CompletedTask;

        return File(memoryStream.ToArray(), "text/csv", $"{fileDownloadName}.csv");
    }

    [HttpPost(nameof(Import))]
    public async Task<IActionResult> Import(IEnumerable<IFormFile> files)
    {
        var list = files.ToList();
        if (!list.Any())
        {
            return Json(false);
        }

        var records = new List<T>();

        foreach (var file in list)
        {
            using var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var fileList = csv.GetRecords<T>()
                              .ToList();

            records.AddRange(fileList);
        }

        await ImportRecords(records);
        await Repository.SaveChangesAsync();

        return Json(true);
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
    protected abstract IEnumerable<T> ExportRecords();

    // ReSharper disable once UnusedParameter.Global
    // ReSharper disable once MemberCanBeMadeStatic.Global
    protected abstract Task ImportRecords(List<T> records);

    private string GetFileDownloadName()
    {
        return $"{_listType}-{DateTime.UtcNow.ToShortDateString()}"
               + $"-{DateTime.UtcNow.ToShortTimeString()}".Replace(" ", "_")
                                                          .Replace(".", "");
    }
}