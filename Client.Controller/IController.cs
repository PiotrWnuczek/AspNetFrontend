namespace Client.Controller
{
    using System.Threading.Tasks;
    using Client.Model;

    public interface IController
    {
        IModel Model { get; set; }
        Task SearchItemsAsync();
    }
}
