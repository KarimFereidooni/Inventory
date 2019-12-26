namespace Inventory.Models.DataModels
{
    public class Product
    {
        public Product()
        {
        }

        public Product(Models.ViewModels.ProductViewModel model)
        {
            this.Name = model.Name;
            this.Count = model.Count;
            this.EnumerationUnit = model.EnumerationUnit;
        }

        public Product(int id, string name, float count, string enumerationUnit)
        {
            this.Id = id;
            this.Name = name;
            this.Count = count;
            this.EnumerationUnit = enumerationUnit;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public float Count { get; set; }

        public string EnumerationUnit { get; set; }
    }
}
