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
using Microsoft.AspNet.Identity;
using ShoppingListAPI.Models;

namespace ShoppingListAPI.Controllers
{
    [Authorize]
    public class ShoppingItemsController : ApiController
    {
        private ShoppingListDbContext db = new ShoppingListDbContext();

        // GET: api/ShoppingItems
        public IQueryable<ShoppingItem> GetItems()
        {
            string userID = User.Identity.GetUserId();
            return db.Items.Where(p=>p.UserID == userID);
        }

        [HttpPut]
        [Route("api/ShoppingItems/{id}/Check")]
        public IHttpActionResult Check(int id)
        {
            var item = db.Items.Find(id);

            item.Bought = !item.Bought;
            db.SaveChanges();

            return Ok(item);
        }

        // GET: api/ShoppingItems/5
        [ResponseType(typeof(ShoppingItem))]
        public IHttpActionResult GetShoppingItem(int id)
        {
            ShoppingItem shoppingItem = db.Items.Find(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            return Ok(shoppingItem);
        }

        // PUT: api/ShoppingItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingItem(int id, ShoppingItem shoppingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingItem.ID)
            {
                return BadRequest();
            }

            db.Entry(shoppingItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingItemExists(id))
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

        // POST: api/ShoppingItems
        [ResponseType(typeof(ShoppingItem))]
        public IHttpActionResult PostShoppingItem(ShoppingItem shoppingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userID = User.Identity.GetUserId();
            shoppingItem.UserID = userID;
            shoppingItem.ListID = db.Lists.Where(p => p.UserID == userID).First().ID;
            db.Items.Add(shoppingItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shoppingItem.ID }, shoppingItem);
        }

        // DELETE: api/ShoppingItems/5
        [ResponseType(typeof(ShoppingItem))]
        public IHttpActionResult DeleteShoppingItem(int id)
        {
            ShoppingItem shoppingItem = db.Items.Find(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            db.Items.Remove(shoppingItem);
            db.SaveChanges();

            return Ok(shoppingItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingItemExists(int id)
        {
            return db.Items.Count(e => e.ID == id) > 0;
        }
    }
}