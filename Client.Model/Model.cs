namespace Client.Model
{
    using System.Linq;
    using System.Collections.Generic;

    public partial class Model : IModel
    {
        public string SearchText { get; set; }

        public List<Item> ItemList
        {
            get { return this.itemList; }
            set { this.itemList = value; }
        }
        private List<Item> itemList = new List<Item>();

        public void LoadItemList()
        {
            if (!string.IsNullOrEmpty(this.SearchText))
            {
                Item[] items = new Service().GetItems(this.SearchText);
                this.ItemList = items.ToList();
            }
        }
    }
}
