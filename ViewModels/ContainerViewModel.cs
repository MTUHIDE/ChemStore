namespace ChemStoreWebApp.ViewModels
{
    public partial class ContainerViewModel : Models.Container
    {
        public string ChemicalName { get; set; }
        public int BuildingIndex { get; set; }
        public int RoomIndex { get; set; }
    }
}
