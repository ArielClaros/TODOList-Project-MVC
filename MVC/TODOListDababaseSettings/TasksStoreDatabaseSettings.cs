using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC;

public class TasksStoreDatabaseSettings
{
    public string? ConnectionString { get; set; }

    public string? DatabaseName { get; set; }

    public string? TODOListCollectionName { get; set; }
}
