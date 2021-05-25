using System;
using System.Collections.Generic;
using System.Linq;
using Models.EF;

namespace Models.DAO
{
    public class CategoryDao
    {
        DBNoiThat db = new DBNoiThat();
        // public List<Category> 
        public List<Category> ListCategory()
        {
            return db.Categories.ToList();
        }
      
        public Category ViewDetail(int id)
        {

            return db.Categories.SingleOrDefault(n => n.CategoryId == id);

        }
        public bool DeleteCate(int id)
        {

            try
            {
                var model = db.Categories.SingleOrDefault(n => n.CategoryId== id);
                db.Categories.Remove(model);
                db.SaveChanges();
                return true;

            }
            catch

            {
                return false;
            }
        }


    }
}
