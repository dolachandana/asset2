// See https://aka.ms/new-console-template for more information

using asset2;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
Console.WriteLine("hello");
asset s = new asset();
MyDbContext MyDb = new MyDbContext();// Creating object for mydbcontext to refer table in database
item newitem = new item();//creating object for item class

while (true)
{

    //creating CRUD operations
    Console.WriteLine($"Pick an option\n" +
        "(1) Create\n" +
        "(2) Read\n" +
        "(3)Update\n" +
     "(4) Delete\n"+
     "(5) Reporting Features\n"+
        "(6) Quit" );//pick up any option
    Console.WriteLine("__________________________________________________________________________________________");
    string a = Console.ReadLine();
    bool isInt1 = int.TryParse(a, out int option);
    if (isInt1)
    {    if(option == 5) { s.reporting(MyDb, newitem); }
        if (option == 4)
        {
            s.delete(MyDb, newitem);
        }
        if (option == 2)
        { s.Read(MyDb,newitem); 
        }
        if (option == 3)
        {
            s.update(MyDb,newitem);

        }
        if (option == 1)
        {
         s.create(MyDb,newitem);   
        }
        if(option == 6) { break; }
    }


  Console.WriteLine("__________________________________________________________________________________________");
}
class asset
{
    public void create(MyDbContext MyDb, item newitem)//creating  data into  database table
    {

        Console.WriteLine("Enter type(computer or phone) ");
        newitem.type = Console.ReadLine();
        Console.WriteLine("Enter Brand");
        newitem.brand = Console.ReadLine();
        Console.WriteLine("Enter Model");
        newitem.modelname = Console.ReadLine();
        Console.WriteLine("Enter PurchaseDate");
        string f = Console.ReadLine();
        DateTime dt1 = Convert.ToDateTime(f);
        newitem.purchase_date = dt1;
        Console.WriteLine("Enter Office");
        newitem.office = Console.ReadLine();
        Console.WriteLine("Enter Price");

        string p = Console.ReadLine();
        bool isInt = int.TryParse(p, out int value);

        if (isInt)// giving todayprice 
        {
            newitem.price = value;
            if (newitem.office.ToLower().Trim() == "spain")
            {
                newitem.currency = "EUR";
                int mul = value * 1;
                newitem.todayprice = mul;
            }
            else if (newitem.office.ToLower().Trim() == "sweden")
            {
                newitem.currency = "SEK";
                double mul = value * 10.98;
                newitem.todayprice = mul;
            }
            else
            {
                newitem.currency = "USD";
                newitem.todayprice = value;
            }
        }
        MyDb.items.Add(newitem);
        MyDb.SaveChanges();

    }
    public void Read(MyDbContext MyDb, item newitem)// reading from database  to console app
    {
        

        List<item> Result = MyDb.items.ToList();
        List<item>result1=Result.OrderBy(x => x.type).ThenBy(x=>x.purchase_date).ToList();//sort by type and purchasedate
        List<item> result2 = result1.OrderBy(x => x.office).ThenBy(x => x.purchase_date).ToList();//sort by office and purchasedate
        foreach (var g in result2)
        {
            DateTime dt2 = DateTime.Now;
            var dt3 = g.purchase_date;
            TimeSpan diff = dt2 - dt3;
            if (diff.TotalDays >= 1005 && diff.TotalDays <= 1095)// mark as red if purchase date is less than 3 months away from 3 years.
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(g.Id.ToString().PadRight(10)+g.type.PadRight(10) + g.brand.PadRight(10) + g.modelname.PadRight(10) + g.office.PadRight(10) + g.purchase_date.ToString("yyyy-MM-dd").PadRight(20) + g.price.ToString().PadRight(14) + g.currency.PadRight(13) + g.todayprice);
                Console.ResetColor();
            }
            else if (diff.TotalDays >= 915 && diff.TotalDays <= 1005)//mark yellow  if date less than 6 months away from 3 years
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine(g.Id.ToString().PadRight(10)+g.type.PadRight(10) + g.brand.PadRight(10) + g.modelname.PadRight(10) + g.office.PadRight(10) + g.purchase_date.ToString("yyyy-MM-dd").PadRight(20) + g.price.ToString().PadRight(14) + g.currency.PadRight(13) + g.todayprice);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(g.Id.ToString().PadRight(10)+g.type.PadRight(10) + g.brand.PadRight(10) + g.modelname.PadRight(10) + g.office.PadRight(10) + g.purchase_date.ToString("yyyy-MM-dd").PadRight(20) + g.price.ToString().PadRight(14) + g.currency.PadRight(13) + g.todayprice);
            }

        }
    }
        public void update(MyDbContext MyDb, item newitem)//Updating the data
        {
       Console.WriteLine("Enter ID you  want to Update");
            int i = int.Parse(Console.ReadLine());
            //updating record i in database
            var user = MyDb.items.SingleOrDefault(x => x.Id == i);// Comparing the ID you want to update with  all the Ids

            Console.Write("Enter Updated Asset Type: ");
            user.type = Console.ReadLine();
            Console.Write("Enter updated Asset Brand: ");
            user.brand = Console.ReadLine();
            Console.Write("Enter updated Asset Model: ");
            user.modelname = Console.ReadLine();
             Console.WriteLine("Enter the  updated Asset office:(India/Sweden/Germany/Norway) ");
            user.office = Console.ReadLine();
            Console.WriteLine("Enter updated Asset Purchase date(YYYY-MM-DD): ");
            string date = Console.ReadLine();
            DateTime dt = Convert.ToDateTime(date);//converting to dateformat
            user.purchase_date = dt;

            Console.WriteLine("Enter updated Asset Price in USD: ");
        string p = Console.ReadLine();
        bool isInt = int.TryParse(p, out int value);

        if (isInt)// giving todayprice 
        {
            user.price = value;
            if (user.office.ToLower().Trim() == "spain")
            {
                user.currency = "EUR";
                int mul = value * 1;
                user.todayprice = mul;
            }
            else if (user.office.ToLower().Trim() == "sweden")
            {
                user.currency = "SEK";
                double mul = value * 10.98;
                user.todayprice = mul;
            }
            else
            {
                user.currency = "USD";
                user.todayprice = value;
            }
        }
        MyDb.SaveChanges();
    }
    public void delete(MyDbContext MyDb, item newitem)//deleting the record you want to
    {
        Console.WriteLine("Enter the ID u want to delete:");
        int i = int.Parse(Console.ReadLine());
        var user1 = MyDb.items.SingleOrDefault(x => x.Id == i);// comparingthe ID u want to delete  with all IDs
        MyDb.items.Remove(user1);
        MyDb.SaveChanges();
    }
 
    public void reporting(MyDbContext MyDb, item newitem)
        {
    List<item> Result1 = MyDb.items.ToList();
         double priceSum = Result1.Sum(newitem => newitem.price);
        double averageAssetPrice = Result1.Average(item => item.price);
        double averageAssetAge = Result1.Average(asset => (DateTime.Now - asset.purchase_date).Days);
        Console.WriteLine("Sum of assets: {0}", priceSum);
        Console.WriteLine("Average price of assets : {0}", averageAssetPrice.ToString("0.##"));
        Console.WriteLine("Average age of assets : {0}", averageAssetAge.ToString("0."));
        Console.ResetColor();
    }
}