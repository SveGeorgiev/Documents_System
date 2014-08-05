using Documents_System.Infrastructure;
using Documents_System.Models;
using Documents_Sytem.Domain.Abstract;
using Documents_Sytem.Domain.Concrete;
using Documents_Sytem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Documents_System.Controllers
{
    public class DocumentsController : Controller
    {
        private IRepository<Document> documentRepository;

        public DocumentsController(IRepository<Document> documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        // GET: /Documents/
        public ActionResult Index(string letter)
        {
            if (letter == null || letter == "all")
            {
                DocumentViewModel model = new DocumentViewModel
                {
                    Documents = documentRepository.All()
                };

                return View(model);
            }
            else
            {
                DocumentViewModel model = new DocumentViewModel
                {
                    Documents = documentRepository.All().Where(doc => doc.Name.Trim().ToLower().StartsWith(letter.ToLower()))
                };

                return View(model);
            }
        }

        // GET: /Documents/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: /Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Document document, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                DateTime date = DateTime.Now;
                Guid guid = Guid.NewGuid();
                string uniqueFileName = guid.ToString().Substring(1, 12) + file.FileName;

                documentRepository.Add(new Document
                {
                    Id = document.Id,
                    Category = document.Category.Trim(),
                    Description = document.Description,
                    Name = document.Name.Trim(),
                    uniqueName = uniqueFileName,
                    DateTime = date
                });
                documentRepository.Save();

                try
                {
                    if (file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Documents/"), uniqueFileName);
                        file.SaveAs(path);
                    }
                    ViewBag.Message = "Upload successful";
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "Upload failed";
                    return RedirectToAction("Index");
                }
            }

            ViewData["alert"] = "Select file please";
            return View(document);
        }

        public FileDownloadResult Download(string uniqueName)
        {
            try
            {
                var fileData = uniqueName.GetFileData(Server.MapPath("~/Documents"));

                return new FileDownloadResult(uniqueName, fileData);
            }
            catch (FileNotFoundException)
            {
                throw new HttpException(404, "File not found.");
            }
        }

        // GET: /Documents/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = documentRepository.GetById(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            return View(document);
        }

        // POST: /Documents/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = documentRepository.GetById(id);
            documentRepository.Delete(id);
            documentRepository.Save();

            string fullPath = Request.MapPath("~/Documents/" + document.uniqueName);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                documentRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}