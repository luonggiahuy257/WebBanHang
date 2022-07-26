using BanHangOnline.Database;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanHangOnline.Common
{
    public class DbResultJson : Controller
    {
        private readonly DataContext _context;

        public DbResultJson(DataContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> GetCategorys()
        {
            try
            {
                List<CategoryViewModel> categorys = await _context.Category.Where(a => a.CategoryEnable == true && a.ParentId != null).ToListAsync();

                return Json(categorys);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<JsonResult> Getuser()
        {
            try
            {
                WebInfoViewModels webinfo = await _context.WebInfo.Where(a => a.Id == 1).FirstOrDefaultAsync();

                return Json(webinfo);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<JsonResult> GetPostNew()
        {
            try
            {
                List<WebPostViewModel> webPostViewModels = await _context.WebPost.Where(a => a.PostParentID == 1 && a.PostEnable == true).OrderByDescending(a => a.PostID).Take(4).ToListAsync();

                return Json(webPostViewModels);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public async Task<JsonResult> GetPostShop()
        {
            try
            {
                List<WebPostViewModel> webPostViewModels = await _context.WebPost.Where(a => a.PostParentID == 2 && a.PostEnable == true).OrderBy(a => a.PostID).Take(6).ToListAsync();

                return Json(webPostViewModels);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
