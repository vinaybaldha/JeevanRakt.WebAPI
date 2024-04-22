using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;


namespace JeevanRakt.Core.DTO
{
    public class ImageDTO
    {
        public Guid ImageId { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }
    }
}
