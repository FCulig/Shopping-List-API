using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ShoppingListAPI.Models;

namespace ShoppingListAPI.Controllers
{
    [Authorize]
    public class ShoppingCategoriesController : ApiController
    {
        private ShoppingListDbContext db = new ShoppingListDbContext();

        // GET: api/ShoppingCategories
        public IQueryable<ShoppingCategory> GetCategories()
        {
            return db.Categories;
        }

        // GET: api/ShoppingCategories/5
        [ResponseType(typeof(ShoppingCategory))]
        public IHttpActionResult GetShoppingCategory(int id)
        {
            ShoppingCategory shoppingCategory = db.Categories.Find(id);
            if (shoppingCategory == null)
            {
                return NotFound();
            }

            return Ok(shoppingCategory);
        }

        // PUT: api/ShoppingCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingCategory(int id, ShoppingCategory shoppingCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingCategory.ID)
            {
                return BadRequest();
            }

            db.Entry(shoppingCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ShoppingCategories
        [ResponseType(typeof(ShoppingCategory))]
        public IHttpActionResult PostShoppingCategory(ShoppingCategory shoppingCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(shoppingCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shoppingCategory.ID }, shoppingCategory);
        }

        // DELETE: api/ShoppingCategories/5
        [ResponseType(typeof(ShoppingCategory))]
        public IHttpActionResult DeleteShoppingCategory(int id)
        {
            ShoppingCategory shoppingCategory = db.Categories.Find(id);
            if (shoppingCategory == null)
            {
                return NotFound();
            }

            db.Categories.Remove(shoppingCategory);
            db.SaveChanges();

            return Ok(shoppingCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingCategoryExists(int id)
        {
            return db.Categories.Count(e => e.ID == id) > 0;
        }
    }
}