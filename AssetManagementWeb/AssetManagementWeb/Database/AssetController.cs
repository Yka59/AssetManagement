using AssetManagementWeb.Models;
using AssetManagementWeb.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetManagementWeb.Database; 

namespace AssetManagementWeb.Database
{
    public class AssetController : Controller
    {
        private object assetId;

        // GET: Asset
        public ActionResult Index()
        {
            return View();
        }

        // GET: Asset/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Asset/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asset/Create
        [HttpPost]
        public JsonResult AssignLocation()
        {
            string json = Request.InputStream.ReadToEnd();
            AssignLocationModel inputData = 
                JsonConvert.DeserializeObject<AssignLocationModel>(json);
            bool success = false;
            string error = "";
            WestwindEntities entities = new WestwindEntities();
            try
            {
                //haetaan ensin paikan id-numero koodin perusteella
                int locationId = (from l in entities.AssetLocations where l.Code == inputData.
                              LocationCode select l.Id).FirstOrDefault();

                //haetaan ensin paikan id-numero koodin perusteella
                int AssetId = (from a in entities.Asset
                                  where a.Code == inputData.
                              LocationCode select a.Id).FirstOrDefault();
                if ((locationId > 0) && (AssetId > 0))
                {
                    //tallennetaan uusi rivi aikaleiman kanssa kantaan
                    Location newEntry = new Location();
                    newEntry.LocationId = locationId;
                    newEntry.AssetId = assetId;
                    newEntry.LastSeen = DateTime.Now;

                    entities.Location.Add(newEntry);
                    entities.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = ex.GetType().Name + ": " + ex.Message;
            }
            finally
            {
                entities.Dispose();
            }
            // palautetaan JSON-muotoinen tulos kutsujalle
            var result = new { success = success, error = error };
            return Json(result);
        }

        // GET: Asset/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Asset/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asset/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Asset/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
