namespace Client.Model
{
    using System.Collections.Generic;
    public interface IModel
    {
        string SearchText { get; set; }
        List<Item> GetItemList { get; }
        List<Item> SearchItemList { get; }
        void GetItemsLoad();
        void SearchItemsLoad();
        void AddItemsLoad();
    }
}
