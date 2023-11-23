using MidTerm_Exm.Models;
using MidTerm_Exm.Repositories.Interfaces;
using MidTerm_Exm.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using X.PagedList.Mvc;
namespace MidTerm_Exm.Controllers
{
    [Authorize]
    public class CountriesController : Controller
    {
        private readonly CountryDbContext db = new CountryDbContext();
        IGenericRepo<Country> repo;
      
        public CountriesController()
        {
            this.repo=new GenericRepo<Country>(db);
        }

        // GET: Countries
        [AllowAnonymous]
        public ActionResult Index(int pg=1)
        {
            var data=this.repo.GetAll("Universities").ToPagedList(pg,5);
            return View(data);


        }
        public ActionResult Create()
        {
            CountryInputModel c = new CountryInputModel();
            c.Universities.Add(new University { });
            return View(c);
        }
        [HttpPost]
        public ActionResult Create(CountryInputModel data, string act = "")
        {


            if (act == "add")
            {
                data.Universities.Add(new University { });
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act.StartsWith("remove"))
            {
                int i = int.Parse(act.Substring(act.IndexOf("_") + 1));
                var ic = data.Universities.Find(x=> x.Id == i);
                data.Universities.Remove(ic);
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var c = new Country
                    {
                        CountryName = data.CountryName,
                        Capital = data.Capital,
                        Currency = data.Currency,
                        Symbol = data.Symbol,
                        IsDeveloped = data.IsDeveloped

                    };
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Server.MapPath("~/Pictures/") + fileName;
                    data.Picture.SaveAs(savePath);
                    c.Picture = fileName;
                    foreach (var u in data.Universities)
                    {
                        c.Universities.Add(u);
                    }
                    this.repo.Insert(c);
                    
                    
                

                }
            }
            ViewBag.Act = act;
            return PartialView("_CreatePartial", data);

        }
        public ActionResult Edit(int id)
        {
            var x = this.repo.Get(id, "Universities");
               var c = new CountryEditModel
                {
                    Id = x.Id,
                    CountryName = x.CountryName,
                    Capital = x.Capital,
                    Currency = x.Currency,
                    Symbol = x.Symbol,
                    IsDeveloped = x.IsDeveloped,
                    Universities = x.Universities.ToList()

                };
                  ViewData["CurrentPic"] = x.Picture;
                  return View(c);

        }
        [HttpPost]
        public ActionResult Edit(CountryEditModel data, string act = "")
        {
            if (act == "add")
            {
                data.Universities.Add(new University { });
            }

            if (act.StartsWith("remove"))
            {
                int i = int.Parse(act.Substring(act.IndexOf("_") + 1));
                var ic = data.Universities.First(x => x.Id == i);
                data.Universities.Remove(ic);
                //data.Universities.RemoveAt(index);
            }

            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var c = this.repo.Get(data.Id);

                    c.CountryName = data.CountryName;
                    c.Capital = data.Capital;
                    c.Currency = data.Currency;
                    c.Symbol = data.Symbol;
                    c.IsDeveloped = data.IsDeveloped;
                    
                

                if (data.Picture != null)
                    {
                        string ext = Path.GetExtension(data.Picture.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Server.MapPath("~/Pictures/") + fileName;
                        data.Picture.SaveAs(savePath);
                        c.Picture = fileName;
                    }
                    //c.Universities.Clear();
                    this.repo.Update(c);
                    this.repo.ExecuteCommand($" DELETE FROM Universities WHERE [CountryId]={c.Id}");
                    foreach (var item in data.Universities)
                    {
                        this.repo.ExecuteCommand($"INSERT INTO Universities (UniversityName, EstublishDate, PayingCost, [AdmissionRequirment], Ranking, CountryId )\r\nVALUES ('{item.UniversityName}','{item.EstublishDate}',{item.PayingCost},'{item.AdmissionRequirment}','{item.Ranking}',{c.Id})");
                    }

                    
                    return RedirectToAction("Index");
                }

            }
            ViewData["CurrentPic"] = db.Countries.First(x => x.Id == data.Id).Picture;
            return View(data);

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            this.repo.ExecuteCommand($"dbo.DeleteCountry {id}");
            return Json(new { success = true, deleted = id });
        }
    }
}
    
