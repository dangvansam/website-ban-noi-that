using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
namespace Models.DAO
{
   
    public class ProductDao
    {
        
         DBNoiThat db  = new DBNoiThat();
       
        public List<Product> ListSanPham()
        {
            return db.Products.ToList();
        }
        public List<Product> CateKorea()
        {
            return db.Products.Where(n => n.Discount == 0 || n.EndDate < DateTime.Now || n.StartDate > DateTime.Now).Where(n => n.CateId== 9).Take(4).ToList();
        }
        public List<Product> CateHavana()
        {
            return db.Products.Where(n => n.Discount == 0 || n.EndDate < DateTime.Now || n.StartDate > DateTime.Now).Where(n => n.CateId == 8).Take(4).ToList();
        }
        public List<ProductView> ProductHot()
        {
            var model = (from a in db.Products
                         join b in db.OrderDetails on a.ProductId equals b.ProductId
                         group b by new { a.Description,a.ProductId,a.Photo,a.Price,a.Discount,a.EndDate,a.StartDate } 
                         into g
                         select new ProductView
                         {
                            Description= g.Key.Description,
                            ProductId=g.Key.ProductId,
                            Price=g.Key.Price,
                            Discount=g.Key.Discount,
                            StartDate=g.Key.StartDate,
                            EndDate=g.Key.EndDate,
                            Photo=g.Key.Photo,
                          
                            Quantity = g.Sum(s => s.Quantity),

                         }).OrderByDescending(n => n.Quantity).Take(6).ToList();
           return model;
        }

        public List<Product> SaleProduct() { 
            var model = db.Products.Where(n => n.Discount > 0 ).OrderByDescending(n => n.Discount).Take(8).ToList();
            return model;
        }
        
        public List<Product> NewProduct()
        {
            return db.Products.Where(n => n.Discount == 0 || n.EndDate < DateTime.Now|| n.StartDate > DateTime.Now).OrderByDescending(n=>n.StartDate).Take(8).ToList();
        }
        public Product DetailsProduct(int Id)
        {
           return db.Products.SingleOrDefault(n => n.ProductId == Id);
        }
        
        //in ra loai san pham
       
        public List<Product> ListByCategoryId(int cateId, ref int total, int pageindex = 1, int pagesize = 12)
        {
            total = db.Products.Where(x => x.CateId == cateId).Count();
            var model = db.Products.Where(x => x.CateId == cateId).OrderByDescending(x => x.Price).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return model;
        }
        public List<string> ListName(string keyword)
        {
        
            return db.Products.Where(n => n.Name.Contains(keyword)).Select(n => n.Name).Distinct().ToList();
        }
        public List<Product> Search(string keyword, ref int total, int pageindex = 1, int pagesize = 12)
        {
            total = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            var model = db.Products.Where(n => n.Name.Contains(keyword)).OrderByDescending(x => x.Price).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return model;
        }
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
        public bool Update(Product entity, bool autoSave = true)
        {
            try
            {

                db.Products.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }


    }
    
}
