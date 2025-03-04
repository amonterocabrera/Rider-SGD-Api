using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGDPEDIDOS.Api.Controllers.Base;
using SGDPEDIDOS.Application.DTOs.ViewModel.Blob_Storage;
using SGDPEDIDOS.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Api.Controllers
{

    [Route("api/[controller]")]
    public class StorageController : OwnBaseController
    {
        private readonly IAzureStorage _storage;

        public StorageController(IAzureStorage storage)
        {
            _storage = storage;
        }
        [AllowAnonymous]
        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get()
        {

            List<BlobDto>? files = await _storage.ListAsync();

            return StatusCode(StatusCodes.Status200OK, files);
        }

        //[HttpPost(nameof(Upload))]
        //public async Task<IActionResult> Upload(IFormFile file)
        //{
        //    BlobResponseDto? response = await _storage.UploadAsync(file);


        //    if (response.Error == true)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
        //    }
        //    else
        //    {

        //        return StatusCode(StatusCodes.Status200OK, response);
        //    }
        //}
        [AllowAnonymous]
        [HttpGet("{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            BlobDto? file = await _storage.DownloadAsync(filename);


            if (file == null)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
            }
            else
            {

                //  return File(file.Content, file.ContentType, file.Name);

                return Ok(file);
            }
        }
        [AllowAnonymous]
        [HttpGet("FilenameStandard")]
        public async Task<IActionResult> DownloadNormak(string filename)
        {
            BlobDto? file = await _storage.DownloadAsyncNormal(filename);

            // Check if file was found
            if (file == null)
            {
                // Was not, return error message to client
                return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
            }
            else
            {
                // File was found, return it to client
                return File(file.Content, file.ContentType, file.Name);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetBlobUriByNameAsync")]
        public async Task<string> GetBlobUriByNameAsync(string filename)
        {
            string? file = await _storage.GetBlobUriByNameAsync(filename);

           return file == null ? null : file.ToString();
        }

        [HttpDelete("DeleteblobFilename")]
        public async Task<IActionResult> DeleteBlob(string filename)
        {
            try
            {
                BlobResponseDto response = await _storage.DeleteAsync(filename);

                if (response.Error)
                {
                    return NotFound(response); // Retorna un 404 Not Found si el archivo no se encuentra
                }

                return Ok(response); // Retorna un 200 OK si la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                // Maneja otros errores aquí
                return StatusCode(500, new BlobResponseDto { Error = true, Status = "Error interno del servidor." });
            }
        }

        [HttpPut("{blobName}")]
        public async Task<IActionResult> UpdateBlob(string blobName, IFormFile newBlobFile)
        {
            if (newBlobFile == null || newBlobFile.Length == 0)
            {
                return BadRequest("No se proporcionó un archivo válido para la actualización.");
            }

            try
            {
                using (var stream = newBlobFile.OpenReadStream())
                {
                    BlobResponseDto response = await _storage.UpdateAsyncs(blobName, stream);

                    if (response.Error)
                    {
                        if (response.Status.Contains("not found"))
                        {
                            return NotFound(response);
                        }
                        return StatusCode(500, response);
                    }

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BlobResponseDto { Error = true, Status = "Error interno del servidor." });
            }
        }
    }
}
