using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanHangOnline.Database;
using BanHangOnline.ViewModels;
using BanHangOnline.Areas.Manager.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using static BanHangOnline.Common.WebConst;
using System.Security.Claims;
using BanHangOnline.Common;
using Microsoft.AspNetCore.Authorization;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = ("Admin"))]
    public class WebPostController : Controller
    {
        private readonly DataContext _context;
        private IWebHostEnvironment Environment;

        public WebPostController(DataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }

        // GET: Manager/WebPostViewModels
        public async Task<IActionResult> Index()
        {
              return View(await _context.WebPost.OrderByDescending(item => item.PostID).ToListAsync());
        }

        // GET: Manager/WebPostViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WebPost == null)
            {
                return NotFound();
            }

            var webPostViewModel = await _context.WebPost
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (webPostViewModel == null)
            {
                return NotFound();
            }

            return View(webPostViewModel);
        }

        // GET: Manager/WebPostViewModels/Create
        public IActionResult Create()
        {
            return View(MakeWebPost());
        }

        // POST: Manager/WebPostViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WebPostViewModel webPostViewModel, List<IFormFile> postedFiles)
        {
            if (ModelState.IsValid)
            {
                if (postedFiles.Count == 0)
                {
                    return View(MakeWebPost());
                }
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                DateTime now = System.DateTime.Now;
                
                webPostViewModel.PostTitleURL = func.GetUrlName(webPostViewModel.PostTitle);
                webPostViewModel.PostImage = func.saveImage(Environment, postedFiles, FileSave.PathSaveWebPost, FileSave.PathWebPost);
                webPostViewModel.CreatedAt = now;
                webPostViewModel.CreatedBy = userId;
                webPostViewModel.PostLastModified = now;
                webPostViewModel.UpdatedAt = now;
                webPostViewModel.UpdatedBy = userId;

                if (webPostViewModel.PostImage == WebConst.FileSave.MaxLengImg)
                {
                    return View(MakeWebPost());
                }

                _context.Add(webPostViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webPostViewModel);
        }

        // GET: Manager/WebPostViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WebPost == null)
            {
                return NotFound();
            }
            //DateTime now = System.DateTime.Now;

            WebPostDropdown webPostDropdown = MakeWebPost();
            var webPostViewModel = await _context.WebPost.FindAsync(id);
            if (webPostViewModel == null)
            {
                return NotFound();
            }
            else
            {
                webPostDropdown.PostID = webPostViewModel.PostID;
                webPostDropdown.PostParentID = webPostViewModel.PostParentID;
                webPostDropdown.PostTypeID = webPostViewModel.PostTypeID;
                webPostDropdown.PostTitle = webPostViewModel.PostTitle;
                webPostDropdown.PostLowerTitle = webPostViewModel.PostLowerTitle;
                webPostDropdown.PostReadmore = webPostViewModel.PostReadmore;
                webPostDropdown.PostContent = webPostViewModel.PostContent;
                webPostDropdown.PostUserName = webPostViewModel.PostUserName;
                webPostDropdown.PostImage = webPostViewModel.PostImage;
                webPostDropdown.PostEnable = webPostViewModel.PostEnable;
                webPostDropdown.PostSeoKeyword = webPostViewModel.PostSeoKeyword;
                webPostDropdown.PostTitleURL = webPostViewModel.PostTitleURL;
                webPostDropdown.PostTitleGoogle = webPostViewModel.PostTitleGoogle;
                webPostDropdown.PostTitleImage = webPostViewModel.PostTitleImage;
                webPostDropdown.PostTitleAlt = webPostViewModel.PostTitleAlt;
                webPostDropdown.PostLastModified = webPostViewModel.PostLastModified;
                webPostDropdown.CreatedAt = webPostViewModel.CreatedAt;
                webPostDropdown.CreatedBy = webPostViewModel.CreatedBy;
                webPostDropdown.UpdatedAt = webPostViewModel.UpdatedAt;
                webPostDropdown.UpdatedBy = webPostViewModel.UpdatedBy;
            }


            return View(webPostDropdown);
        }

        // POST: Manager/WebPostViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,WebPostViewModel webPostViewModel, List<IFormFile> postedFiles)
        {
            if (id != webPostViewModel.PostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    DateTime now = System.DateTime.Now;

                    webPostViewModel.PostTitleURL = func.GetUrlName(webPostViewModel.PostTitle);
                    webPostViewModel.PostImage = func.saveImage(Environment, postedFiles, FileSave.PathSaveWebPost, FileSave.PathWebPost);
                    webPostViewModel.CreatedAt = now;
                    webPostViewModel.CreatedBy = userId;
                    webPostViewModel.PostLastModified = now;
                    webPostViewModel.UpdatedAt = now;
                    webPostViewModel.UpdatedBy = userId;

                    if (webPostViewModel.PostImage == WebConst.FileSave.MaxLengImg)
                    {
                        return View(MakeWebPost());
                    }

                    _context.Update(webPostViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebPostViewModelExists(webPostViewModel.PostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(webPostViewModel);
        }

        // GET: Manager/WebPostViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WebPost == null)
            {
                return NotFound();
            }

            var webPostViewModel = await _context.WebPost
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (webPostViewModel == null)
            {
                return NotFound();
            }

            return View(webPostViewModel);
        }

        // POST: Manager/WebPostViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WebPost == null)
            {
                return Problem("Entity set 'DataContext.WebPost'  is null.");
            }
            var webPostViewModel = await _context.WebPost.FindAsync(id);
            if (webPostViewModel != null)
            {
                _context.WebPost.Remove(webPostViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPostViewModelExists(int id)
        {
          return _context.WebPost.Any(e => e.PostID == id);
        }
        private WebPostDropdown MakeWebPost()
        {
            List<WebPostParentViewModel> webPostParent = _context.WebPostParent.Where(item => item.PostParentEnable == true).Select(x => new WebPostParentViewModel { PostParentID = x.PostParentID, PostParentTitle = x.PostParentTitle }).ToList();
            WebPostDropdown WebPostViewModel = new WebPostDropdown();
            if (webPostParent.Count != 0)
            {
                WebPostViewModel.WebPostParent = webPostParent;
            }

            return WebPostViewModel;
        }
    }
}
