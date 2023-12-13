using System;
using System.Collections.Generic;

namespace VBSaveInterceptor.VBModels;

public abstract class BaseDomainModel
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
}
public class Customer : BaseDomainModel
{
    public string? CustomerName { get; set; }
    public string? CustomerAddress { get; set; }
    public ICollection<Order> Orders { get; set; }
}
