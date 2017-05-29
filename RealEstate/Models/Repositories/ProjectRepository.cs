﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstate.Models.Abstract;
using System.IO;
using System.Collections;
using System.Data.Entity;

namespace RealEstate.Models.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public void Delete(Project project)
        {
            var Images = _db.Images.Where(x => x.ProjectBelongsId == project.ProjectId);
            foreach (var item in Images)
            {
                _db.Images.Remove(item);
            }
            _db.Projects.Remove(project);
            _db.SaveChanges();
        }

      /*  public void Edit(Project project, IEnumerable<HttpPostedFileBase> images)
        {
            if(images != null)
            {
                var imagesList = new List<Image>();
                foreach (var image in images)
                {
                    using (var br = new BinaryReader(image.InputStream))
                    {
                        var data = br.ReadBytes(image.ContentLength);
                        var img = new Image { ProjectBelongsId = project.ProjectId };
                        img.ImageData = data;
                        imagesList.Add(img);
                    }
                }
                project.Images = imagesList;
                _db.Entry(project).State = EntityState.Modified;
            }
        } */

        public void InsertOrUpdate(Project project, IEnumerable<HttpPostedFileBase> images)
        {
            _db.Projects.Add(project);
            _db.SaveChanges();
            if (images != null)
            {
                var imagesList = new List<Image>();
                foreach (var image in images)
                {
                    using (var br = new BinaryReader(image.InputStream))
                    {
                        var data = br.ReadBytes(image.ContentLength);
                        var img = new Image { ProjectBelongsId = project.ProjectId };
                        img.ImageData = data;
                        imagesList.Add(img);
                    }
                }
                project.Images = imagesList;
            }

            _db.SaveChanges();
           // return RedirectToAction("Index");
        }

        Project IProjectRepository.Find(int? id)
        {
            return _db.Projects.Find(id);
        }

        IEnumerable<Project> IProjectRepository.GetAll()
        {
            return _db.Projects;
        }

        public IEnumerable<Project> GetProjectsWithTypeOfBuilding(string typeOfBuilding)
        {
            return _db.Projects.Where((s => s.TypeOfBuilding == typeOfBuilding));
        }

        public IEnumerable<Project> GetProjectsWithYear(string fromYear, string toYear)
        {
            int year1 = Int32.Parse(fromYear);
            int year2 = Int32.Parse(toYear);
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectYear = Int32.Parse(project.Year); ;
                if ((projectYear >= year1) && (projectYear <= year2))
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

        public IEnumerable<Project> GetProjectsWithPrice(string fromPrice, string toPrice )
        {
            int price1 = Int32.Parse(fromPrice);
            int price2 = Int32.Parse(toPrice);
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {  
                int projectPrice = Int32.Parse(project.Price); ;
                    if ((projectPrice >= price1) && (projectPrice <= price2))
                    {
                        projects.Add(project);
                    }
            }
            return projects;
        }

        public IEnumerable<Project> GetProjectsWithTypeOfBuildingAndFromYear(string typeOfBuilding, string fromYear)
        {
            int year1 = Int32.Parse(fromYear);
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectYear = Int32.Parse(project.Year); ;
                if ((project.TypeOfBuilding == typeOfBuilding) && (projectYear >= year1))
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

        public IEnumerable<Project> GetProjectsWithTypeOfBuildingAndToYear(string typeOfBuilding, string toYear)
        {
            int year1 = Int32.Parse(toYear);
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectYear = Int32.Parse(project.Year); ;
                if ((project.TypeOfBuilding == typeOfBuilding) && (projectYear <= year1))
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

        public IEnumerable<Project> GetProjectsWithTypeOfBuildingAndFromPrice(string typeOfBuilding, string fromPrice)
        {
            int price1 = Int32.Parse(fromPrice);
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectPrice = Int32.Parse(project.Price); ;
                if ((project.TypeOfBuilding == typeOfBuilding) && (projectPrice >= price1))
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

        public IEnumerable<Project> GetProjectsWithTypeOfBuildingAndToPrice(string typeOfBuilding, string toPrice)
        {
            int price1 = Int32.Parse(toPrice);
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectPrice = Int32.Parse(project.Price); ;
                if ((project.TypeOfBuilding == typeOfBuilding) && (projectPrice <= price1))
                {
                    projects.Add(project);
                }
            }
            return projects;
        }




        public IEnumerable<Project> GetProjectsWithTypeOfBuildingAndYear(string typeOfBuilding, string fromYear, string toYear)
        {
            int year1 = Int32.Parse(fromYear);
            int year2 = Int32.Parse(toYear);
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectYear = Int32.Parse(project.Year); ;
                if ((project.TypeOfBuilding == typeOfBuilding) && (projectYear >= year1) && (projectYear <= year2))
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

        public IEnumerable<Project> GetProjectsWithTypeOfBuildingAndPrice(string typeOfBuilding, string fromPrice, string toPrice)
        {
            int price1 = Int32.Parse(fromPrice);
            int price2 = Int32.Parse(toPrice);
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectPrice = Int32.Parse(project.Price); ;
                if ((project.TypeOfBuilding == typeOfBuilding) && (projectPrice >= price1) && (projectPrice <= price2))
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

        public IEnumerable<Project> GetProjectsWithTypeOfBuildingYearAndPrice(string typeOfBuilding,
            string fromYear, string toYear, string fromPrice, string toPrice)
        {
            int year1 = Int32.Parse(fromYear);
            int year2 = Int32.Parse(toYear);
            int price1 = Int32.Parse(fromPrice);
            int price2 = Int32.Parse(toPrice);
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectYear = Int32.Parse(project.Year);
                int projectPrice = Int32.Parse(project.Price);
                if ((project.TypeOfBuilding == typeOfBuilding) && (projectYear >= year1)
                    && (projectYear <= year2) && (projectPrice >= price1) && (projectPrice <= price2))
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

       public IEnumerable<Project> GetProjectsWithYearFrom(string fromYear)
        {
            int year1 = Int32.Parse(fromYear);
           
            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectYear = Int32.Parse(project.Year); ;
                if (projectYear >= year1)
                {
                    projects.Add(project);
                }
            }
            return projects;
        }
        public IEnumerable<Project> GetProjectsWithYearTo(string toYear)
        {
            int year2 = Int32.Parse(toYear);

            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectYear = Int32.Parse(project.Year); ;
                if (projectYear <= year2)
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

        public IEnumerable<Project> GetProjectsWithPriceFrom(string fromPrice)
        {
            int price1 = Int32.Parse(fromPrice);

            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectPrice = Int32.Parse(project.Price); ;
                if (projectPrice >= price1)
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

        public IEnumerable<Project> GetProjectsWithPriceTo(string toPrice)
        {
            int price2 = Int32.Parse(toPrice);

            List<Project> projects = new List<Project>();
            foreach (Project project in _db.Projects)
            {
                int projectPrice = Int32.Parse(project.Price); ;
                if (projectPrice <= price2)
                {
                    projects.Add(project);
                }
            }
            return projects;
        }

        IEnumerable<Image> IProjectRepository.GetAllImages()
        {   
            return _db.Images.ToList();
        }

        public IEnumerable<Image> GetImagesWithProjectId(Project project)
        {
           return _db.Images.Where(x => x.ProjectBelongsId == project.ProjectId).ToList();
        }


    }
}