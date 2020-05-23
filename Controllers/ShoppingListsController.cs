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
    public class ShoppingListsController : ApiController
    {
        private ShoppingListDbContext db = new ShoppingListDbContext();

        // GET: api/ShoppingLists
        public ShoppingList GetLists()
        {
            string userId = User.Identity.GetUserId();
            var list = db.Lists.Where(p => p.UserID == userId).FirstOrDefault();
            
            if(list != null)
            {
                var items = db.Items.Where(p => p.ListID == list.ID).ToList();
                list.Items = items;

                return list;
            }
            else
            {
                list = new ShoppingList{
                    UserID = User.Identity.GetUserId()
                };
                db.Lists.Add(list);
                db.SaveChanges();

                var items = db.Items.Where(p => p.ListID == list.ID).ToList();
                list.Items = items;

                return list;
            }
        }

        // GET: api/ShoppingLists/5
        [ResponseType(typeof(ShoppingList))]
        public IHttpActionResult GetShoppingList(int id)
        {
            ShoppingList shoppingList = db.Lists.Find(id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            return Ok(shoppingList);
        }

        // PUT: api/ShoppingLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingList(int id, ShoppingList shoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingList.ID)
            {
                return BadRequest();
            }

            db.Entry(shoppingList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(id))
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

        // POST: api/ShoppingLists
        [ResponseType(typeof(ShoppingList))]
        public IHttpActionResult PostShoppingList(ShoppingList shoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            shoppingList.UserID = User.Identity.GetUserId();
            db.Lists.Add(shoppingList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shoppingList.ID }, shoppingList);
        }

        // DELETE: api/ShoppingLists/5
        [ResponseType(typeof(ShoppingList))]
        public IHttpActionResult DeleteShoppingList(int id)
        {
            ShoppingList shoppingList = db.Lists.Find(id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            db.Lists.Remove(shoppingList);
            db.SaveChanges();

            return Ok(shoppingList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingListExists(int id)
        {
            return db.Lists.Count(e => e.ID == id) > 0;
        }
    }
}