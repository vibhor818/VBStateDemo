using VBSaveInterceptor.MyContexts;
using VBSaveInterceptor.VBModels;


        RajcustomerorderdbContext context = new RajcustomerorderdbContext();
        Customer cust = new Customer
        {
            CustomerName = "Test",
            CustomerAddress = "123Test"
        };
        context.Add(cust);
       await context.SaveChangesAsync();
    

