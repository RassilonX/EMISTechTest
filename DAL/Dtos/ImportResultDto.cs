using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos;

public class ImportResultDto
{
    public bool Success { get; set; }

    public List<ImportJsonDto> FailedImports { get; set; }
}
