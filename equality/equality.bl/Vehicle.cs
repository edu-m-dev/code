namespace equality.bl;
public class Vehicle
{

    private string manufacturer;
    private string model;
    private int productionYear;

    public Vehicle(string manufacturer, string model, int productionYear)
    {
        this.manufacturer = manufacturer;
        this.model = model;
        this.productionYear = productionYear;
    }

    public string GetLabel()
    {
        return string.Format("{0} {1}", this.manufacturer, this.model);
    }

}
