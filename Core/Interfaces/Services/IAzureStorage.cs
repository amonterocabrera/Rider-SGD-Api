using SGDPEDIDOS.Application.DTOs.ViewModel.Blob_Storage;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IAzureStorage
    {
        Task<BlobResponseDto> UploadAsync(Stream blob, string extension);
        Task<BlobResponseDto> UpdateAsyncs(string blobName, Stream newBlobData);
        Task<BlobResponseDto> Update64Asyncs(string blobName, string base64Data);
        Task<BlobDto> DownloadAsync(string blobFilename);
        Task<BlobDto> DownloadAsyncNormal(string blobFilename);
        Task<BlobResponseDto> DeleteAsync(string blobFilename);
        Task<List<BlobDto>> ListAsync();
        Task<string> GetBlobUriByNameAsync(string blobName);
    }
}
