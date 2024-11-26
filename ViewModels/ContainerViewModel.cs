namespace ChemStoreWebApp.ViewModels
{
    public partial class ContainerViewModel : Models.X_Container
    {
        public string ContainerName { get; set; }
        public int BuildingIndex { get; set; }
        public int RoomIndex { get; set; }
    }
}
