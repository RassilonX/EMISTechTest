using DAL.Dtos;

namespace DAL;

public interface IDataImportRepository
{
    public Task<ImportResultDto> SaveJson(List<ImportJsonDto> data);
}
