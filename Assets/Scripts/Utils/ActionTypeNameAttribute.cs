using System;
using System.Linq;

public class ActionTypeNameAttribute : Attribute
{
    public string Name { get; private set; }

    public ActionTypeNameAttribute(string name)
    {
        this.Name = name;
    }

    public string GetName()
    {
        return this.Name; 
    }
}
