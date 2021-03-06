using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Persistence.Repositories.Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> ListProjects();
        INotification SaveProject(Project project);
        Project Load(Guid Id);
        Project GetProjectFromName(string name);
        Project CreateProject(string projectName, string projectDescription, Company company, User owner);
        Project GetProjectFromId(Guid id);

        
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IValidator _validator;
        private readonly IPermissionRepository _permissionRepository;

        public ProjectService(IProjectRepository projectRepository, 
            IValidator validator,
            IPermissionRepository permissionRepository)
        {
            _projectRepository = projectRepository;
            _validator = validator;
            _permissionRepository = permissionRepository;
        }

        public IEnumerable<Project> ListProjects()
        {
            return _projectRepository.Query().OrderBy(x => x.Name).ToList();

        }

        public INotification SaveProject(Project project)
        {
            var notification = _validator.Validate(project);

            if(notification.IsValid())
            {
                _projectRepository.Save(project);
                ValueObjectRegistry.AddValueObject<Project>(new ValueObject(project.Id.ToString(), project.Name));
            }

            return notification;

        }

        public Project Load(Guid Id)
        {
           return  _projectRepository.Get(Id);
        }

        public Project GetProjectFromName(string name)
        {
            return _projectRepository.Query().Where(c => c.Name == name).FirstOrDefault();
        }

        public Project CreateProject(string projectName, string projectDescription, Company company, User owner)
        {
            var project = new Project();
            project.Name = projectName;
            project.Description = projectDescription;
            project.Backlog = new BoardColumn {Name = BoardColumn.BacklogName, Description = "This is the project Backlog"};
            project.Archive = new BoardColumn {Name = BoardColumn.ArchiveName, Description = "This queue contains all finished tasks"};

            project.AddBoardColumn(new CustomBoardColumn { ColumnOrder = 1, Name = "Ready", Description = "Items in this column are ready"});
            project.AddBoardColumn(new CustomBoardColumn { ColumnOrder = 2, Name = "Working", Description = "Items in this column are being worked on"});
            project.AddBoardColumn(new CustomBoardColumn { ColumnOrder = 3, Name = "Done", Description = "Items in this column are Done"});

            project.Company = company;

            project.Owner = owner;

            project.Status = ProjectStatus.Active;

            var permissions = _permissionRepository.Query().ToArray();

            project.AddRole(GetAdminRole(permissions));
            project.AddRole(GetUserRole(permissions));

            return project;
        }

        private Role GetAdminRole(Permission[] permissions)
        {
            var adminRole = new Role("Adminstrator");

            permissions.Each(x => adminRole.AddPermission(x));
            return adminRole;
        }

         private Role GetUserRole(Permission[] permissions)
         {
             var userRole = new Role("User");

             userRole.AddPermission(permissions
                                        .Where(x => x.Name == PermissionName.CanViewProject)
                                        .FirstOrDefault());
             userRole.AddPermission(permissions
                                        .Where(x => x.Name == PermissionName.CanListDailyTime)
                                        .FirstOrDefault());
             userRole.AddPermission(permissions
                                        .Where(x => x.Name == PermissionName.CanListProjects)
                                        .FirstOrDefault());
             userRole.AddPermission(permissions
                                        .Where(x => x.Name == PermissionName.CanListTimeRecords)
                                        .FirstOrDefault());
             userRole.AddPermission(permissions
                                        .Where(x => x.Name == PermissionName.CanListTasks)
                                        .FirstOrDefault());

             return userRole;
            
         }

        public Project GetProjectFromId(Guid id)
        {
            return _projectRepository.Get(id);
        }

       
     
    }
}