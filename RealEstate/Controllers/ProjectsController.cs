﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstate.Models;
using System.IO;
using System.Collections;
using RealEstate.Models.Abstract;

namespace RealEstate.Controllers
{
    public class ProjectsController : Controller
    {
       
        // Projects repository
        private readonly IProjectRepository _projectRepo;

        //Dependency injection
        public ProjectsController(IProjectRepository repo)
        {
            this._projectRepo = repo;
        }

        // GET: Projects
        public ActionResult Index(string typeOfBuilding = "", string fromYear = "", string toYear = "", string fromPrice = "", string toPrice = "")
        {
            var listOfImages = _projectRepo.GetAllImages();
            ViewBag.ImagesList = listOfImages;

            List<SelectListItem> typesOfHouses = new List<SelectListItem>();
            typesOfHouses.Add(new SelectListItem
            {
                Text = Resources.ProjectModelTexts.ProjectModelTexts.House,
                Value = "House"
            });
            typesOfHouses.Add(new SelectListItem
            {
                Text = Resources.ProjectModelTexts.ProjectModelTexts.Cottage,
                Value = "Cottage",
            });
            ViewBag.typeOfBuilding = typesOfHouses;


            if (!string.IsNullOrEmpty(typeOfBuilding) && !string.IsNullOrEmpty(fromYear) && !string.IsNullOrEmpty(toYear) 
                && !string.IsNullOrEmpty(fromPrice) && !string.IsNullOrEmpty(toPrice))
            {
                return View(_projectRepo.GetProjectsWithTypeOfBuildingYearAndPrice(typeOfBuilding, fromYear, toYear, fromPrice, toPrice));
            }

            if (!string.IsNullOrEmpty(typeOfBuilding) && !string.IsNullOrEmpty(fromYear) && !string.IsNullOrEmpty(toYear))
            {
                return View(_projectRepo.GetProjectsWithTypeOfBuildingAndYear(typeOfBuilding, fromYear, toYear));
            }

            if (!string.IsNullOrEmpty(typeOfBuilding) && !string.IsNullOrEmpty(fromPrice) && !string.IsNullOrEmpty(toPrice))
            {
                return View(_projectRepo.GetProjectsWithTypeOfBuildingAndPrice(typeOfBuilding, fromPrice, toPrice));
            }

            if (!string.IsNullOrEmpty(typeOfBuilding) && !string.IsNullOrEmpty(fromYear))
            {
                return View(_projectRepo.GetProjectsWithTypeOfBuildingAndFromYear(typeOfBuilding, fromYear));
            }

            if (!string.IsNullOrEmpty(typeOfBuilding) && !string.IsNullOrEmpty(toYear))
            {
                return View(_projectRepo.GetProjectsWithTypeOfBuildingAndToYear(typeOfBuilding, toYear));
            }

            if (!string.IsNullOrEmpty(typeOfBuilding) && !string.IsNullOrEmpty(fromPrice))
            {
                return View(_projectRepo.GetProjectsWithTypeOfBuildingAndFromPrice(typeOfBuilding, fromPrice));
            }

            if (!string.IsNullOrEmpty(typeOfBuilding) && !string.IsNullOrEmpty(toPrice))
            {
                return View(_projectRepo.GetProjectsWithTypeOfBuildingAndToPrice(typeOfBuilding, toPrice));
            }

            if (!string.IsNullOrEmpty(typeOfBuilding))
            {
                return View(_projectRepo.GetProjectsWithTypeOfBuilding(typeOfBuilding));
            }

            if(!string.IsNullOrEmpty(fromYear) && !string.IsNullOrEmpty(toYear))
            {
                return View(_projectRepo.GetProjectsWithYear(fromYear, toYear));
            }

            if (!string.IsNullOrEmpty(fromPrice) && !string.IsNullOrEmpty(toPrice))
            {
                return View(_projectRepo.GetProjectsWithPrice(fromPrice, toPrice));
            }

            if (!string.IsNullOrEmpty(fromYear))
            {
                return View(_projectRepo.GetProjectsWithYearFrom(fromYear));
            }

            if (!string.IsNullOrEmpty(toYear))
            {
                return View(_projectRepo.GetProjectsWithYearTo(toYear));
            }

            if (!string.IsNullOrEmpty(fromPrice))
            {
                return View(_projectRepo.GetProjectsWithPriceFrom(fromPrice));
            }

            if (!string.IsNullOrEmpty(toPrice))
            {
                return View(_projectRepo.GetProjectsWithPriceTo(toPrice));
            }

            return View(_projectRepo.GetAll());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _projectRepo.Find(id);
            var listOfImages = _projectRepo.GetImagesWithProjectId(project);

            var listOfStrings = new List<byte[]>();
            foreach (var item in listOfImages)
            {
                listOfStrings.Add(item.ImageData);
            }
            ViewBag.ProgList = listOfStrings;
            ViewBag.ProjectTitle = project.Title;

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //Deletes images belonging to project
        public ActionResult DeleteImages(int? id)
        {
            _projectRepo.DeleteImages(id);
            return RedirectToAction("Edit", new { id = id});
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            List<SelectListItem> typesOfHouses = new List<SelectListItem>();
            typesOfHouses.Add(new SelectListItem
            {
                Text = Resources.ProjectModelTexts.ProjectModelTexts.House,
                Value = "House"
            });
            typesOfHouses.Add(new SelectListItem
            {
                Text = Resources.ProjectModelTexts.ProjectModelTexts.Cottage,
                Value = "Cottage",
            });
            ViewBag.typeOfBuilding = typesOfHouses;

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,Title,Address,TypeOfBuilding,Year,Area,PlotArea,NumberOfFloors,NumberOfRooms,Price,AdditionalFacilities,AdditionalInformation")] Project project,
            IEnumerable<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid)
            {
                _projectRepo.InsertOrUpdate(project, images);
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _projectRepo.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = _projectRepo.Find(id);
            _projectRepo.Delete(project);
            return RedirectToAction("Index");
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _projectRepo.Find(id);
            var listOfImages = _projectRepo.GetImagesWithProjectId(project);

            var listOfStrings = new List<byte[]>();
            foreach (var item in listOfImages)
            {
                listOfStrings.Add(item.ImageData);
            }
            //send images data belonging to this project
            ViewBag.ProgList = listOfStrings;

            //send projectId to use for delete images
            int projectId = project.ProjectId;
            ViewBag.IdOfProject = projectId;
            

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,Title,Address,TypeOfBuilding,Year,Area,PlotArea,NumberOfFloors,NumberOfRooms,Price,AdditionalFacilities,AdditionalInformation")] Project project, IEnumerable<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid)
            {
                //_projectRepo.InsertOrUpdate(project, images);
                _projectRepo.Edit(project, images);
                return RedirectToAction("Index");
            } else
            {
                var modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
                var errors = modelErrors;
                
            }
            return View(project);
        }

    }
}
