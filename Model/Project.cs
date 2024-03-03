using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBore.Model;

public class Project
{
    public string ProjectPath { get; set; }

    public string ProjectNumber { get; set; }

    public string Customer { get; set; }

    public string Location { get; set; }

    public DateTime LastEdited { get; set; }

    public string ProjectYear { get; set; }

    public Project()
    {

    }
}
