
public class Upgrade
{
    private string name;
    private float value;
    private string description;

    protected void SetName(string name)
    {
        this.name = name;
    }
    
    public void SetValue(float value)
    {
        this.value = value;
    }

    public string GetName()
    {
        return name;
    }

    public float GetValue()
    {
        return value;
    }

    public void SetDescription(string description)
    {
        this.description = description;
    }

    public string GetDescription()
    {
        return description;
    }
}
