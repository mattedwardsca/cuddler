using Cuddler.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Core.Services.Documents;

public interface IDocumentsService
{
    Task<DocumentEntity> AddDocument(string ContextId, string? Description, string? Extension, MemoryStream FileContent, string? FileName);

    bool DeleteDocument(string documentId);

    List<DocumentEntity> ListAllDocuments(string contextId, string? folderId);

    List<DocumentEntity> ListArchivedDocuments(string contextId, string folderId);

    Task<DocumentEntity> UploadDocument(string documentId, AccountEntity loggedInUser, string contextId, string contextType, string? description, string? title, IFormFile file);

    Task<DocumentEntity> UploadDocument(AccountEntity loggedInUser, string contextId, string contextType, string? description, string? title, IFormFile file);

    Task<List<DocumentEntity>> UploadDocuments(AccountEntity loggedInUser, string contextId, string contextType, string? description, string? title, List<IFormFile> allFiles);
}