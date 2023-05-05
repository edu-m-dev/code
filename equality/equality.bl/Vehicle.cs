namespace equality.bl;
public class Vehicle:IEquatable<Vehicle>
{
    private string _manufacturer;
    private string _model;
    private int _productionYear;

    public Vehicle(string manufacturer, string model, int productionYear)
    {
        _manufacturer = manufacturer;
        _model = model;
        _productionYear = productionYear;
    }

    public string GetLabel()
    {
        return string.Format("{0} {1}", _manufacturer, _model);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Vehicle);
    }

    public override int GetHashCode()
    {
        return _manufacturer.GetHashCode() ^ _model.GetHashCode();
    }

    public static bool operator ==(Vehicle? x, Vehicle? y) 
    {
        return x?.Equals(y) ?? false;
    }

    public static bool operator !=(Vehicle? x, Vehicle? y)
    {
        return !x?.Equals(y) ?? true;
    }
    public bool Equals(Vehicle? vehicle) => vehicle switch
    {
        Vehicle v when _manufacturer.Equals(v._manufacturer) && _model.Equals(v._model) => true,
        _ => false,
    };
}
