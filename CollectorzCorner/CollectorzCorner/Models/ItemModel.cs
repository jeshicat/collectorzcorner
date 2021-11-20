using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CollectorzCorner.Utilities;

namespace CollectorzCorner.Models
{
    public class ItemModel
    {
        public Item Item { get; private set; }

        public ItemModel(){}

        public ItemModel(int ID)
        {
            this.Item = DBUtility.GetItemByID(ID);
        }
    }
}
