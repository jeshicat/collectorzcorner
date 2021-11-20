using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectorzCorner.Utilities;
using CollectorzCorner.Models;
using CollectorzCorner.Membership;
using System.Web.Script.Serialization;
using System.Web.Helpers;

namespace CollectorzCorner.Controllers
{
    public class CollectionController : Controller
    {
        //
        // GET: /Collection/
        [Authorize]
        public ActionResult Index()
        {
            CollectionModel model = new CollectionModel(); 
            return View(model);
        }

        [Authorize]
        public ActionResult Icon()
        {
            CollectionModel model = new CollectionModel();
            return View(model);
        }

        public string GetCollectionTypeNameByID(string TypeID)
        {
            string name = DBUtility.GetCollectionTypeNameByID(TypeID);
            return name;
        }
        // GET: /Collection/Create
        // Users create a collection here
        [Authorize]
        public ActionResult Create()
        {
            ViewData["CollectionTypes"] = DBUtility.GetItemTypes();
            return View();
        }

        // POST:/Collection/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateCollectionModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Create");

            using (CCEntities db = new CCEntities())
            {
                Collection collection = new Collection();
                collection.Name = model.Name;
                collection.TypeID = model.TypeID;
                collection.IsPublic = model.IsPublic;
                collection.Description = model.Description;
                collection.UserID = Utility.GetUserToken().UserId;
                collection.IsComplete = model.IsComplete;
                db.Collections.AddObject(collection);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Items(int id)
        {
            CollectionItemsModel model = new CollectionItemsModel(id);
            return View(model);
        }

        public string GetItemTitleByID(int ItemID)
        {
            var item = DBUtility.GetItemByID(ItemID);
            return item.Title;
        }

        public string GetStatusDescriptionByID(string StatusID)
        {
            CollectionItemStatu status = DBUtility.GetStatusByID(StatusID);
            return status.Description;
        }

        public ActionResult RemoveItemFromCollection(int CollectionID, int ItemID)
        {
            DBUtility.RemoveItemFromCollection(CollectionID, ItemID);
            return RedirectToAction("Items", new { id = CollectionID });
        }

        public ActionResult RemoveCollection(int CollectionID)
        {
            DBUtility.RemoveCollection(CollectionID);
            return RedirectToAction("Index");
        }

        public string GetCollectionItemCount(int CollectionID)
        {
            return DBUtility.GetCollectionItemCount(CollectionID).ToString();
        }

        public string EditCollection(int CollectionID, String cName, String cType, String cDesc, bool isPublic, bool isComplete)
        {
            return DBUtility.EditCollection(CollectionID, cName, cType, cDesc, isPublic, isComplete);
        }

        public string EditCollectionItem(int collectionID, int itemID, String statusID, String statusNote, byte rating, String review){
            return DBUtility.EditCollectionItem(collectionID, itemID, statusID, statusNote, rating, review); 
        }

        public string PopulateStatusList()
        {
            var status = DBUtility.GetItemStatus();
            var JSON = new JavaScriptSerializer();
            string serialized = JSON.Serialize(status);
            return serialized;
        }

        public FileContentResult GetCollectionImage(int id)
        {
            int? pictureID = DBUtility.FindCollectionPicture(id);
            WebImage wImg;
            if (pictureID != null)
            {
                byte[] imgArray = DBUtility.GetPictureByID(pictureID);
                wImg = new WebImage(imgArray);
            }
            else
            {
                wImg = new WebImage("../../Content/Images/CCLogo_white.png");
            }
            wImg.Resize(200, 200);
            var image = wImg.GetBytes("image/jpeg");
            return new FileContentResult(image, "image/jpeg");           
        }
    }
}
