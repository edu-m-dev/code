namespace equality.bl;
public record VehicleRecord
{
    private string _manufacturer;
    private string _model;
    private int _productionYear;

    public VehicleRecord(string manufacturer, string model, int productionYear)
    {
        _manufacturer = manufacturer;
        _model = model;
        _productionYear = productionYear;
    }
    public string GetLabel()
    {
        return string.Format("{0} {1}", _manufacturer, _model);
    }
}
