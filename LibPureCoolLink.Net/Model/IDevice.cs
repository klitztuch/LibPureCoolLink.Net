namespace LibPureCoolLink.Net.Model
{
    public interface IDevice
    {
        string Serial { get; set; }
        string Name { get; set; }
        string Version { get; set; }
        string LocalCredentials { get; set; }
        bool AutoUpdate { get; set; }
        bool NewVersionAvailable { get; set; }
        string ProductType { get; set; }
        string ConnectionType { get; set; }
        
    }
}