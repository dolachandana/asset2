// See https://aka.ms/new-console-template for more information

using asset2;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
Console.WriteLine("hello");
asset s = new asset();
MyDbContext MyDb = new MyDbContext();
item newitem = new item();

while (true)
{

    
    Console.WriteLine($"Pick an option\n" +
        "(1) Create\n" +
        "(2) Read\n" +
        "(3)Update\n" +
     "(4) Delete\n"+
        "(5) Quit" );//pick up any option
    Console.WriteLine("__________________________________________________________________________________________");
    string a = Console.ReadLine();
    bool isInt1 = int.TryParse(a, out int option);
    if (isInt1)
    {    if(option == 5) { break; }
        if (option == 4)
        {
            
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

                Console.WriteLine(g.type.PadRight(10) + g.brand.PadRight(10) + g.modelname.PadRight(10) + g.office.PadRight(10) + g.purchase_date.ToString("yyyy-MM-dd").PadRight(20) + g.price.ToString().PadRight(14) + g.currency.PadRight(13) + g.todayprice);
                Console.ResetColor();
            }
            else if (diff.TotalDays >= 915 && diff.TotalDays <= 1005)//mark yellow  if date less than 6 months away from 3 years
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine(g.type.PadRight(10) + g.brand.PadRight(10) + g.modelname.PadRight(10) + g.office.PadRight(10) + g.purchase_date.ToString("yyyy-MM-dd").PadRight(20) + g.price.ToString().PadRight(14) + g.currency.PadRight(13) + g.todayprice);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(g.type.PadRight(10) + g.brand.PadRight(10) + g.modelname.PadRight(10) + g.office.PadRight(10) + g.purchase_date.ToString("yyyy-MM-dd").PadRight(20) + g.price.ToString().PadRight(14) + g.currency.PadRight(13) + g.todayprice);
            }

        }

        //foreach (var item in result2)
        //{
        //    Console.WriteLine(item.type);
        //}


    }
        public void update(MyDbContext MyDb, item newitem)
        {
        //    string d;
        
        //    Console.WriteLine("Enter item Id:");
        //    d = Console.ReadLine();
        //bool isInt = int.TryParse(d, out int value);
        //var user = MyDb.items.SingleOrDefault(c => c.Id == 1);
        //Console.WriteLine("What do u want to update\n" +
        //        " 1.type\n2.brand \n3.purchase_date\n4.price\n5.modelname\n6.office ");
        //    string update1 = Console.ReadLine();
        //    //var user = from c in MyDb.items
        //    //           where d ;
            
        //newitem.type = update1;
        //MyDb.items.Add(newitem);
        //MyDb.SaveChanges();
        }
    public void delete(MyDbContext MyDb, item newitem)
    {
        //var user1 = MyDb.items.SingleOrDefault(x => x.Id == 4);
        //MyDb.items.Remove(user1);
        //MyDb.SaveChanges();


    }
}