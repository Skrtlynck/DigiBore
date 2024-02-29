using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiBore.Model;
using DigiBore.Repositories.Interfaces;

namespace DigiBore.ViewModels;

public class ProjectsViewModel
{
    private readonly IProjectsRepository _projectsRepository;

    public ObservableCollection<Project> Projects => _projectsRepository.Projects;

    public ProjectsViewModel(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }

    public void AddProject(Project projectToAdd) => _projectsRepository.AddProject(projectToAdd);

}
