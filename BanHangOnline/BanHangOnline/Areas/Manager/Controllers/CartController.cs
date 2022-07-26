using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanHangOnline.Database;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BanHangOnline.Areas.Manager.Models;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = ("Admin"))]
    public class CartController : Controller
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }

        // GET: Manager/Cart
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cart.ToListAsync());
        }

        // GET: Manager/Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cartViewModelCheckOut = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartViewModelCheckOut == null)
            {
                return NotFound();
            }

            return View(cartViewModelCheckOut);
        }

        // GET: Manager/Cart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manager/Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,SessionId,Token,Status,OrderStatusId,Quantity,Contents,FirstName,LastName,MobileNumber,PhoneNumber,Email,Address,AddressExt,CountryId,ProvinceId,CityId,TownId,GenderId,CreatedAt,UpdatedAt")] CartViewModelCheckOut cartViewModelCheckOut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartViewModelCheckOut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartViewModelCheckOut);
        }

        // GET: Manager/Cart/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_context.Cart == null)
            {
                return NotFound();
            }
            CartAreaModels cartModels = MakeCart(id);
            var cartViewModelCheckOut = await _context.Cart.FindAsync(id);
            if (cartViewModelCheckOut != null)
            {
                cartModels.Id = cartViewModelCheckOut.Id;
                cartModels.UserId = cartViewModelCheckOut.UserId;
                cartModels.SessionId = cartViewModelCheckOut.SessionId;
                cartModels.Token = cartViewModelCheckOut.Token;
                cartModels.Status = cartViewModelCheckOut.Status;
                cartModels.OrderStatusId = cartViewModelCheckOut.OrderStatusId;
                cartModels.Quantity = cartViewModelCheckOut.Quantity;
                cartModels.Contents = cartViewModelCheckOut.Contents;
                cartModels.FirstName = cartViewModelCheckOut.FirstName;
                cartModels.LastName = cartViewModelCheckOut.LastName;
                cartModels.MobileNumber = cartViewModelCheckOut.MobileNumber;
                cartModels.PhoneNumber = cartViewModelCheckOut.PhoneNumber;
                cartModels.Email = cartViewModelCheckOut.Email;
                cartModels.Address = cartViewModelCheckOut.Address;
                cartModels.AddressExt = cartViewModelCheckOut.AddressExt;
                cartModels.CountryId = cartViewModelCheckOut.CountryId;
                cartModels.ProvinceId = cartViewModelCheckOut.ProvinceId;
                cartModels.CityId = cartViewModelCheckOut.CityId;
                cartModels.TownId = cartViewModelCheckOut.TownId;
                cartModels.GenderId = cartViewModelCheckOut.GenderId;
                cartModels.CreatedAt = cartViewModelCheckOut.CreatedAt;
                cartModels.UpdatedAt = cartViewModelCheckOut.UpdatedAt;
            }

            if (cartViewModelCheckOut == null)
            {
                return NotFound();
            }

            return View(cartModels);
        }

        // POST: Manager/Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,SessionId,Token,Status,OrderStatusId,Quantity,Contents,FirstName,LastName,MobileNumber,PhoneNumber,Email,Address,AddressExt,CountryId,ProvinceId,CityId,TownId,GenderId,CreatedAt,UpdatedAt")] CartViewModelCheckOut cartViewModelCheckOut)
        {
            if (id != cartViewModelCheckOut.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CartAreaModels cartModels = MakeCart(id);



                    _context.Update(cartViewModelCheckOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartViewModelCheckOutExists(cartViewModelCheckOut.Id))
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
            return View(cartViewModelCheckOut);
        }

        // GET: Manager/Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cartViewModelCheckOut = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartViewModelCheckOut == null)
            {
                return NotFound();
            }

            return View(cartViewModelCheckOut);
        }

        // POST: Manager/Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_context.Cart == null)
                    {
                        return Problem("Entity set 'DataContext.Cart'  is null.");
                    }
                    var cartViewModelCheckOut = await _context.Cart.FindAsync(id);
                    var listCartitem = await _context.CartItems.Where(item => item.CartId == id).ToListAsync();
                    foreach (var item in listCartitem)
                    {
                        var cartitem = await _context.CartItems.FindAsync(item.Id);
                        if (cartitem != null)
                        {
                            _context.CartItems.Remove(cartitem);
                            await _context.SaveChangesAsync();
                        }
                    }

                    if (cartViewModelCheckOut != null)
                    {
                        _context.Cart.Remove(cartViewModelCheckOut);
                        await _context.SaveChangesAsync();
                    }

               
                    transaction.Commit();

                return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }

        private bool CartViewModelCheckOutExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }

        private CartAreaModels MakeCart(int id)
        {
            List<StatusOrderViewModel> statusOrder = _context.StatusOrder.Select(x => new StatusOrderViewModel { Id = x.Id, Name = x.Name }).ToList();
            List<CartItemsViewModel> CartItems = _context.CartItems.Where(item => item.CartId == id).ToList();
            List<ProductViewModel> listProduct = new();

            foreach (var item in CartItems)
            {
                ProductViewModel productViewModel = _context.Product.Where(itemproduct => itemproduct.Id == item.ProductId).FirstOrDefault();
                listProduct.Add(productViewModel);
            };
            CartAreaModels cartModels = new();
            if (statusOrder.Count > 0 && listProduct.Count > 0)
            {
                cartModels.StatusOrders = statusOrder;
                cartModels.Products = listProduct;
            }

            return cartModels;
        }
    }
}
