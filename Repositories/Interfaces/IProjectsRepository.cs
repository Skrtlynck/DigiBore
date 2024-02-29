using DigiBore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBore.Repositories.Interfaces;

public interface IProjectsRepository
{
    public ObservableCollection<Project> Projects { get; }

    public void AddProject(Project project);

}
