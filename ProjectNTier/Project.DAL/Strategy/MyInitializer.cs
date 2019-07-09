using Bogus;
using Bogus.DataSets;
using Project.DAL.Context;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Strategy
{
    public class MyInitializer : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            AppUser admin = new AppUser();
            admin.UserName = "BerkeGzl";
            admin.Password = "Berke123";
            admin.Email = "berke.guzel96@gmail.com";
            context.AppUsers.Add(admin);
            context.SaveChanges();

            AppUserDetail admind = new AppUserDetail();
            admind.ID = admin.ID;
            admind.FirstName = "Berke";
            admind.LastName = "Güzel";
            admind.Address = "Maltepe";
            context.AppUserDetails.Add(admind);
            context.SaveChanges();

            AppUser member = new AppUser();
            member.UserName = "BerkeGzl2";
            member.Password = "Berke123";
            member.Email = "guzel.berke96@gmail.com";
            context.AppUsers.Add(member);
            context.SaveChanges();

            AppUserDetail memberd = new AppUserDetail();
            memberd.ID = member.ID;
            memberd.FirstName = "Berke";
            memberd.LastName = "Güzel";
            memberd.Address = "Maltepe";
            context.AppUserDetails.Add(memberd);
            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                AppUser ap = new AppUser();
                ap.UserName = new Internet("tr").UserName();
                ap.Password = new Internet("tr").Password();
                ap.Email = new Internet("tr").Email();
                context.AppUsers.Add(ap);
                context.SaveChanges();

                AppUserDetail apd = new AppUserDetail();
                apd.ID = ap.ID;
                apd.FirstName = new Name("tr").FirstName();
                apd.LastName = new Name("tr").LastName();
                apd.Address = new Address("tr").Locale;
                context.AppUserDetails.Add(apd);
                context.SaveChanges();
            }
            

            //for (int i = 0; i < 11; i++)
            //{
            //    AppUserDetail apd = new AppUserDetail();
            //    apd.ID = i;
            //    apd.FirstName = new Name("tr").FirstName();
            //    apd.LastName = new Name("tr").LastName();
            //    apd.Address = new Address("tr").Locale;
            //    context.AppUserDetails.Add(apd);
            //}
            //context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                Category c = new Category();
                c.CategoryName = new Commerce("tr").Categories(1)[0];
                c.Description = new Lorem("tr").Sentence(100);

                for (int x = 0; x < 50; x++)
                {
                    Randomizer random = new Bogus.Randomizer();
                    Product p = new Product();
                    p.ProductName = new Commerce("tr").ProductName();
                    p.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
                    p.ImagePath = new Images("en").Nightlife();
                    p.UnitsInStock = Convert.ToInt16(random.Number(1, 500)); 
                    p.Description = new Lorem("tr").Sentence(5);
                    c.Products.Add(p);
                }
                context.Categories.Add(c);
                context.SaveChanges();
            }
        }
    }
}
