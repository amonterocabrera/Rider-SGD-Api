using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGDPEDIDOS.Application.DTOs.ViewModel.Blob_Storage;
using SGDPEDIDOS.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Infrastructure.Services
{
    public class AzureStorage : IAzureStorage
    {
        #region Dependency Injection / Constructor

        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly ILogger<AzureStorage> _logger;

        public AzureStorage(IConfiguration configuration, ILogger<AzureStorage> logger)
        {
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
            _logger = logger;
        }

        #endregion

        public async Task<List<BlobDto>> ListAsync()
        {
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            List<BlobDto> files = new List<BlobDto>();

            await foreach (BlobItem file in container.GetBlobsAsync())
            {

                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new BlobDto
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }
            return files;
        }

        public async Task<string> GetBlobUriByNameAsync(string blobName)
        {
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            BlobItem? blobItem = null;
            await foreach (BlobItem item in container.GetBlobsAsync())
            {
                if (item.Name.Equals(blobName, StringComparison.OrdinalIgnoreCase))
                {
                    blobItem = item;
                    break;
                }
            }

            if (blobItem != null)
            {
                string uri = container.Uri.ToString();
                return $"{uri}/{blobItem.Name}";
            }

            return "assets/img/proveedor/upload-image.svg"; 
        }




        public async Task<BlobResponseDto> UploadAsync(Stream blob, string extension)
        {
            BlobResponseDto response = new();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            string blobName = Guid.NewGuid().ToString();
            try
            {

                BlobClient client = container.GetBlobClient(blobName + extension);
                await client.UploadAsync(blob);

                response.Status = $"File {blobName} Uploaded Successfully";
                response.Error = false;
                response.Blob.Uri = client.Uri.AbsoluteUri;
                response.Blob.Name = client.Name;
            }

            catch (RequestFailedException ex)
               when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                _logger.LogError($"File with name {blobName} already exists in container. Set another name to store the file in the container: '{_storageContainerName}.'");
                response.Status = $"File with name {blobName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }

            catch (RequestFailedException ex)
            {

                _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }
            return response;
        }


        public async Task<BlobDto> DownloadAsync(string blobFilename)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            try
            {

                BlobClient file = client.GetBlobClient(blobFilename);

                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();
                    Stream blobContent = data;

                    var content = await file.DownloadContentAsync();


                    string name = blobFilename;
                    string contentType = content.Value.Details.ContentType;

                    string xd = ConvertToBase64(blobContent);
                    return new BlobDto { Base64Content = xd, Name = name, ContentType = contentType };
                }
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {

                _logger.LogError($"File {blobFilename} was not found.");
            }

            return null;
        }
        public async Task<BlobDto> DownloadAsyncNormal(string blobFilename)
        {
            // Get a reference to a container named in appsettings.json
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                // Get a reference to the blob uploaded earlier from the API in the container from configuration settings
                BlobClient file = client.GetBlobClient(blobFilename);

                // Check if the file exists in the container
                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();
                    Stream blobContent = data;

                    // Download the file details async
                    var content = await file.DownloadContentAsync();

                    // Add data to variables in order to return a BlobDto
                    string name = blobFilename;
                    string contentType = content.Value.Details.ContentType;

                    // Create new BlobDto with blob data from variables
                    return new BlobDto { Content = blobContent, Name = name, ContentType = contentType };
                }
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                // Log error to console
                _logger.LogError($"File {blobFilename} was not found.");
            }

            // File does not exist, return null and handle that in requesting method
            return null;
        }

        private string ConvertToBase64(Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return Convert.ToBase64String(memoryStream.ToArray());
            }

            var bytes = new Byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }
        public async Task<BlobResponseDto> UploadAsync(IFormFile blob)
        {
         
            BlobResponseDto response = new();

           
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
              
                BlobClient client = container.GetBlobClient(blob.FileName);

             
                await using (Stream? data = blob.OpenReadStream())
                {
          
                    await client.UploadAsync(data);
                }

              
                response.Status = $"File {blob.FileName} Uploaded Successfully";
                response.Error = false;
                response.Blob.Uri = client.Uri.AbsoluteUri;
                response.Blob.Name = client.Name;

            }
        
            catch (RequestFailedException ex)
               when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                _logger.LogError($"File with name {blob.FileName} already exists in container. Set another name to store the file in the container: '{_storageContainerName}.'");
                response.Status = $"File with name {blob.FileName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
      
            catch (RequestFailedException ex)
            {
          
                _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }
 
            return response;
        }
        public async Task<BlobResponseDto> DeleteAsync(string blobFilename)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            BlobClient file = client.GetBlobClient(blobFilename);

            try
            {

                await file.DeleteAsync();
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {

                _logger.LogError($"File {blobFilename} was not found.");
                return new BlobResponseDto { Error = true, Status = $"File with name {blobFilename} not found." };
            }


            return new BlobResponseDto { Error = false, Status = $"File: {blobFilename} has been successfully deleted." };

        }

        public async Task<BlobResponseDto> UpdateAsyncs(string blobName, Stream newBlobData)
        {
            BlobResponseDto response = new BlobResponseDto();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                BlobClient client = container.GetBlobClient(blobName);

                if (await client.ExistsAsync())
                {
                    // Carga el nuevo contenido en el blob existente
                    await client.UploadAsync(newBlobData, true);

                    response.Status = $"File {blobName} Updated Successfully";
                    response.Error = false;
                    response.Blob.Uri = client.Uri.AbsoluteUri;
                    response.Blob.Name = client.Name;
                }
                else
                {
                    response.Status = $"File {blobName} not found.";
                    response.Error = true;
                }
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError($"Error updating file {blobName}: {ex.Message}");
                response.Status = $"Error updating file {blobName}: {ex.Message}";
                response.Error = true;
            }

            return response;
        }

        public async Task<BlobResponseDto> Update64Asyncs(string blobName, string base64Data)
        {
            BlobResponseDto response = new BlobResponseDto();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                BlobClient client = container.GetBlobClient(blobName);

                if (await client.ExistsAsync())
                {

                    var newBlobData = new MemoryStream(Convert.FromBase64String(base64Data));


                    await client.UploadAsync(newBlobData, true);

                    response.Status = $"File {blobName} Updated Successfully";
                    response.Error = false;
                    response.Blob.Uri = client.Uri.AbsoluteUri;
                    response.Blob.Name = client.Name;
                }
                else
                {
                    response.Status = $"File {blobName} not found.";
                    response.Error = true;
                }
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError($"Error updating file {blobName}: {ex.Message}");
                response.Status = $"Error updating file {blobName}: {ex.Message}";
                response.Error = true;
            }

            return response;
        }

    }

}
