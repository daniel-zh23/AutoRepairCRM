namespace AutoRepairCRM.Core.Models;

public class AllResultModel<T>
{
    public int Total { get; set; }

    public IEnumerable<T> Items { get; set; } = new List<T>();
}