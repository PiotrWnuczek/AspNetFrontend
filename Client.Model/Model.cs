namespace Client.Model
{
    using System.Linq;
    using System.Collections.Generic;

    public partial class Model : IModel
    {
        public string SearchText { get; set; }

        public List<Item> GetItemList
        {
            get { return this.getItemList; }
            set { this.getItemList = value; }
        }
        private List<Item> getItemList = new List<Item>();

        public List<Item> SearchItemList
        {
            get { return this.searchItemList; }
            set { this.searchItemList = value; }
        }
        private List<Item> searchItemList = new List<Item>();

        public void GetItemsLoad()
        {
            Item[] items = new Service().GetItems();
            this.GetItemList = items.ToList();
        }

        public void SearchItemsLoad()
        {
            if (!string.IsNullOrEmpty(this.SearchText))
            {
                Item[] items = new Service().GetItem(this.SearchText);
                this.SearchItemList = items.ToList();
            }
        }
    }
}
