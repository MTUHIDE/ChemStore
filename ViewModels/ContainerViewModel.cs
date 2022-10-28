namespace ChemStoreWebApp.ViewModels
{
    public partial class ContainerViewModel : Models.Container
    {
        public string ChemicalName { get; set; }
        public int BuildingEditIndex { get; set; }
        public int RoomEditIndex { get; set; }
    }
}
