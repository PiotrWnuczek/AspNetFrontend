namespace Client.Model
{
    using System.Collections.Generic;
    public interface IModel
    {
        string SearchText { get; set; }
        List<Item> ItemList { get; }
        void GetItemsLoad();
        void SearchItemsLoad();
    }
}
