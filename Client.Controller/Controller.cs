namespace Client.Controller
{
    using System.Threading.Tasks;
    using Client.Model;

    public partial class Controller : IController
    {
        public IModel Model { get; set; }
        public Controller(IModel model)
        {
            this.Model = model;
        }

        public async Task GetItemsAsync()
        {
            await Task.Run(() => this.Model.GetItemsLoad());
        }

        public async Task SearchItemsAsync()
        {
            await Task.Run(() => this.Model.SearchItemsLoad());
        }

        public async Task AddItemsAsync()
        {
            await Task.Run(() => this.Model.AddItemsLoad());
        }
    }
}
