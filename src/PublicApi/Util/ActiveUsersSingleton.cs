using System.Collections.Generic;

public interface IActiveUsersSingleton
{
    Dictionary<string, int> ActiveUsersId { get; set; }
}

public class ActiveUsersSingleton : IActiveUsersSingleton
{
    public Dictionary<string, int> ActiveUsersId { get; set; } = new Dictionary<string, int>();
}