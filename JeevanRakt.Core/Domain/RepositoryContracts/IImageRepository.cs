

using JeevanRakt.Core.Domain.Entities;

namespace JeevanRakt.Core.Domain.RepositoryContracts
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
