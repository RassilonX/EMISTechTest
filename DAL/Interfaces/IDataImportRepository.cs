using DAL.Dtos;

namespace DAL.Interfaces;

public interface IDataImportRepository
{
    public Task<ImportResultDto> SaveJson(List<ImportJsonDto> data);
}
