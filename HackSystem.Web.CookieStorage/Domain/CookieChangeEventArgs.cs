namespace HackSystem.Web.CookieStorage;

public class CookieChangedEventArgs : EventArgs
{
    public CookieChangedEventArgs(string name, string newValue, string oldValue)
    {
        this.Name = name;
        this.NewValue = newValue;
        this.OldValue = oldValue;
    }

    public string Name { get; set; }

    public string NewValue { get; set; }

    public string OldValue { get; set; }
}
