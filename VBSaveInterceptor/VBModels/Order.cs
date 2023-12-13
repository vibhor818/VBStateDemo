using System;
using System.Collections.Generic;

namespace VBSaveInterceptor.VBModels;

public class Order : BaseDomainModel
{
    public string? OrderName { get; set; }
    public int CustomerId { get; set; }
    public string? OrderDesc { get; set; }
    public Customer Customer { get; set; }
}