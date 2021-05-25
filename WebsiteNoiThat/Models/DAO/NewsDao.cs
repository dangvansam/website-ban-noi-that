using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
namespace Models.DAO
{
  public  class NewsDao
    {
        DBNoiThat db = new DBNoiThat();

        public List<News> NewsHot()
        {
            return db.News.Take(4).ToList();
        }
        public bool Update(News entity, bool autoSave = true)
        {
            try
            {

                db.News.Attach(entity);
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
        public bool DeleteNew(int id)
        {
        
            try
            {
                var model = db.News.SingleOrDefault(n=>n.NewsId==id);
                db.News.Remove(model);
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
