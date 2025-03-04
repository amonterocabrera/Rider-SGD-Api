namespace SGDPEDIDOS.Application.DTOs.ViewModel.Blob_Storage
{
    public class AttachmentDto
    {
        public string FileName { get; set; }
        public string Extension { get; set; }

        public string Documentname { get; set; }
        public string File { get; set; } 
        public int CodTipoDocumento { get; set; }
    }
}
