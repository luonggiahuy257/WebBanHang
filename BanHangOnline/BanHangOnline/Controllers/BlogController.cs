using BanHangOnline.Database;
using BanHangOnline.Models;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BanHangOnline.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataContext _context;

        public BlogController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/danh-muc-tin-tuc")]
        public IActionResult BlogList()
        {
            List<WebPostViewModel> webPosts = _context.WebPost
                .Where(item => item.PostEnable == true)
                .OrderByDescending(item => item.PostID)
                .ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.WebPosts = webPosts;
            mymodel.ListPagination = this.GetProduct(1, webPosts.Count());

            return View(mymodel);
        }
        [HttpPost("/danh-muc-tin-tuc")]
        public IActionResult BlogList(int currentPageIndex)
        {
            List<WebPostViewModel> webPosts = _context.WebPost
                .Where(item => item.PostEnable == true)
                .OrderByDescending(item => item.PostID)
                .ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.WebPosts = webPosts;
            mymodel.ListPagination = this.GetProduct(currentPageIndex, webPosts.Count());

            return View(mymodel);
        }

        [Route("chi-tiet-{BlogDetailUrl}")]
        public IActionResult BlogDetail(string BlogDetailUrl)
        {
            DetailListPost detailListPosts = new DetailListPost();
            List<WebPostViewModel> webPosts = _context.WebPost.Where(item => item.PostEnable == true && item.PostParentID == 2).OrderByDescending(item => item.PostID).Take(3).ToList();
            WebPostViewModel WebPost = _context.WebPost.Where(item => item.PostTitleURL == BlogDetailUrl).FirstOrDefault();
            if (webPosts != null)
            {
                detailListPosts.WebPosts = webPosts;
            }

            if (WebPost != null)
            {
                detailListPosts.PostID = WebPost.PostID;
                detailListPosts.PostParentID = WebPost.PostParentID;
                detailListPosts.PostTypeID = WebPost.PostTypeID;
                detailListPosts.PostTitle = WebPost.PostTitle;
                detailListPosts.PostLowerTitle = WebPost.PostLowerTitle;
                detailListPosts.PostReadmore = WebPost.PostReadmore;
                detailListPosts.PostContent = WebPost.PostContent;
                detailListPosts.PostUserName = WebPost.PostUserName;
                detailListPosts.PostImage = WebPost.PostImage;
                detailListPosts.PostEnable = WebPost.PostEnable;
                detailListPosts.PostSeoKeyword = WebPost.PostSeoKeyword;
                detailListPosts.PostTitleURL = WebPost.PostTitleURL;
                detailListPosts.PostTitleGoogle = WebPost.PostTitleGoogle;
                detailListPosts.PostTitleImage = WebPost.PostTitleImage;
                detailListPosts.PostTitleAlt = WebPost.PostTitleAlt;
                detailListPosts.PostLastModified = WebPost.PostLastModified;
                detailListPosts.CreatedAt = WebPost.CreatedAt;
                detailListPosts.CreatedBy = WebPost.CreatedBy;
                detailListPosts.UpdatedAt = WebPost.UpdatedAt;
                detailListPosts.UpdatedBy = WebPost.UpdatedBy;
            }


            if (WebPost != null)
            {
                return View(detailListPosts);
            }
            return View(new DetailListPost());
        }

        private PaginationWebPost GetProduct(int currentPage, int webpostCount)
        {
            int maxRows = 4;
            PaginationWebPost webPostList = new PaginationWebPost();
            webPostList.WebPosts = (from customer in this._context.WebPost
                                    select customer)
                    .Where(item => item.PostEnable == true)
                    .OrderByDescending(customer => customer.CreatedAt)
                    .Skip((currentPage - 1) * maxRows)
                    .Take(maxRows).ToList();

            double pageCount = (double)((decimal)webpostCount / Convert.ToDecimal(maxRows));
            webPostList.PageCount = (int)Math.Ceiling(pageCount);

            webPostList.CurrentPageIndex = currentPage;

            return webPostList;
        }
    }
}
