using VBStateDemo.MyContexts;
using VBStateDemo.VBModels;
using Microsoft.EntityFrameworkCore;

using (VbcustomerorderdbContext context=new VbcustomerorderdbContext())
{
    //Customer cust = new Customer()
    //{
    //    CustomerId=9,
    //    CustomerName="ABC",
    //    CustomerAddress="123abc"
    //};
    //Console.WriteLine(context.Entry(cust).State.ToString());
    //context.Entry(cust).State = EntityState.Added;
    //Console.WriteLine(context.ChangeTracker.DebugView.LongView);
    ////context.Customers.Add(cust);
    ////Console.WriteLine(context.Entry(cust).State.ToString());
    ////context.SaveChanges();

    var data = context.Customers.ToList();
    Console.WriteLine(context.ChangeTracker.DebugView.LongView);
    foreach (var item in data)
    {
        Console.WriteLine(item.CustomerName);
    }

    //var data = context.Customers.Find(9);
    //Console.WriteLine(context.Entry(data).State.ToString());
    //data.CustomerName = "ABC2";
    //Console.WriteLine(context.Entry(data).State.ToString());
    ////context.SaveChanges();
    //var data1 = context.Customers.ToList();
    //Console.WriteLine(context.ChangeTracker.DebugView.LongView);
    //context.SaveChanges();
    //Console.WriteLine(context.ChangeTracker.DebugView.LongView);
    //foreach (var item in data1)
    //{
    //    Console.WriteLine(item.CustomerName);
    //}
   // context.Entry(data).State = EntityState.Deleted;
   // Console.WriteLine(context.ChangeTracker.DebugView.LongView);
   // var data1 = context.Customers.ToList();
   // Console.WriteLine(context.ChangeTracker.DebugView.LongView);
   //await  context.SaveChangesAsync();
   // Console.WriteLine(context.ChangeTracker.DebugView.LongView);
   // Thread.Sleep(1000);
   
}

