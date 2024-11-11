using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Utilities.Messages
{
    public class OutputMessages
    {
        public const string InvalidStoreType = "The type of the store must be BookStore or OfficeStore";
        public const string StoreExist = "Store wint name {0} already exist";
        public const string StoreAdded = "Store with name {0} successfully added to the repository";
        public const string ItemAdde = "Item {0} with delivery price {1} and product number {2} successfully addred to Genral Store.";
        public const string NoSuchStore = "Store with name {} does not exist in repository";
        public const string StoreDelivery = "The store type {0} with name {1} received delivery of {2} items";
    }
}
