using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CollectorzCorner.Utilities;
using CollectorzCorner.Membership;

namespace CollectorzCorner.Models
{
    public class CollectionModel
    {
        private List<Collection> Collections { get; set; }
        CCMembershipUser cUser = Utility.GetUserToken();
        public CollectionModel()
        {
            this.Collections = DBUtility.FindCollectionsForSpecificUser(cUser.UserId);
        }

        public List<Collection> GetCollections()
        {
            return this.Collections;
        }
    }

    public class CreateCollectionModel
    {
        [Required]
        [Display(Name="Collection Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Collection Type")]
        public string TypeID { get; set; }

        [Display(Name = "Description (Optional)")]
        public string Description { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }

    public class CollectionItemsModel
    {
        public String CollectionName { get; private set; }
        public List<CollectionItem> Items { get; private set; }

        public CollectionItemsModel() { }

        public CollectionItemsModel(int id)
        {
            this.CollectionName = DBUtility.FindCollectionNameById(id);
            this.Items = DBUtility.FindCollectionItems(id);
        }
    }
}