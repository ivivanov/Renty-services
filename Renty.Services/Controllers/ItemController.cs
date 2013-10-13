using Renty.Services.Attributes;
using Renty.Services.DataTransferObects;
using Renty.Services.DataTtransferObjects;
using Renty.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace Renty.Services.Controllers
{
    public class ItemController : BaseController
    {
        [HttpPost, ActionName("rent")]
        public HttpResponseMessage RentItem([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, AddItemModel item)
        {
            var response = this.PerformOperationAndHandleExceptions(
              () =>
              {
                  var dbContext = new RentyDbContext();
                  var user = dbContext.Users.Single(x => x.SessionKey == sessionKey);
                  if (user == null)
                  {
                      throw new InvalidOperationException("Invalid user session!");
                  }

                  Item itemEntity = new Item()
                  {
                      DateBorrowed = item.DateBorrowed,
                      ReturnDeadline = item.ReturnDeadline,
                      ImageBase64 = item.ImageBase64,
                      Name = item.Name,
                      Notes = item.Notes,
                      Owner = item.Owner,
                      Renter = user.Username
                  };

                  item.Renter = user.Username;
                  user.ItemsToReturn.Add(itemEntity);
                  dbContext.SaveChanges();

                  var request = this.Request.CreateResponse(HttpStatusCode.Created, item);
                  return request;
              });

            return response;
        }

        [HttpGet, ActionName("getrented")]
        public HttpResponseMessage GetRented([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {

            var response = this.PerformOperationAndHandleExceptions(
              () =>
              {
                  var dbContext = new RentyDbContext();
                  var user = dbContext.Users.Single(x => x.SessionKey == sessionKey);
                  if (user == null)
                  {
                      throw new InvalidOperationException("Invalid user session!");
                  }

                  var result = user.ItemsToReturn.Where(x=>x.IsReturned == false).ToList();
                  var reqeust = this.Request.CreateResponse(HttpStatusCode.OK, result);
                  return reqeust;
              });

            return response;
        }

        [HttpPost, ActionName("lend")]
        public HttpResponseMessage LendItem([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, AddItemModel item)
        {
            var response = this.PerformOperationAndHandleExceptions(
             () =>
             {
                 var dbContext = new RentyDbContext();
                 var user = dbContext.Users.Single(x => x.SessionKey == sessionKey);
                 if (user == null)
                 {
                     throw new InvalidOperationException("Invalid user session!");
                 }

                 Item itemEntity = new Item()
                 {
                     DateBorrowed = item.DateBorrowed,
                     ReturnDeadline = item.ReturnDeadline,
                     ImageBase64 = item.ImageBase64,
                     Name = item.Name,
                     Notes = item.Notes,
                     Owner = user.Username,
                     Renter = item.Renter
                 };

                 item.Owner = user.Username;
                 user.ItemsToReceive.Add(itemEntity);
                 dbContext.SaveChanges();

                 var request = this.Request.CreateResponse(HttpStatusCode.Created, item);
                 return request;
             });

            return response;
        }

        [HttpGet, ActionName("getlended")]
        public HttpResponseMessage GetLended([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {

            var response = this.PerformOperationAndHandleExceptions(
              () =>
              {
                  var dbContext = new RentyDbContext();
                  var user = dbContext.Users.Single(x => x.SessionKey == sessionKey);
                  if (user == null)
                  {
                      throw new InvalidOperationException("Invalid user session!");
                  }

                  var result = user.ItemsToReceive.Where(x => x.IsReturned == false).ToList();
                  var reqeust = this.Request.CreateResponse(HttpStatusCode.OK, result);
                  return reqeust;
              });

            return response;
        }

        [HttpGet, ActionName("getitem")]
        public HttpResponseMessage GetItem([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, int id)
        {
            var response = this.PerformOperationAndHandleExceptions(
                  () =>
                  {
                      var dbContext = new RentyDbContext();
                      var user = dbContext.Users.Single(x => x.SessionKey == sessionKey);
                      if (user == null)
                      {
                          throw new InvalidOperationException("Invalid user session!");
                      }

                      var item = dbContext.Items.Find(id);
                      var itemDto = new ItemModel()
                      {
                          DateBorrowed = item.DateBorrowed,
                          ImageBase64 = item.ImageBase64,
                          ItemId = item.ItemId,
                          Name = item.Name,
                          Notes = item.Notes,
                          Owner = item.Owner,
                          Renter = item.Renter,
                          ReturnDeadline = item.ReturnDeadline
                      };
                      var reqeust = this.Request.CreateResponse(HttpStatusCode.OK, itemDto);
                      return reqeust;
                  });

            return response;
        }

        [HttpPut, ActionName("returned")]
        public HttpResponseMessage MarkAsReturned([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, int id)
        {
            var response = this.PerformOperationAndHandleExceptions(
                 () =>
                 {
                     var dbContext = new RentyDbContext();
                     var user = dbContext.Users.Single(x => x.SessionKey == sessionKey);
                     if (user == null)
                     {
                         throw new InvalidOperationException("Invalid user session!");
                     }

                     var item = dbContext.Items.Find(id);
                     item.IsReturned = true;
                     dbContext.SaveChanges();
                     var reqeust = this.Request.CreateResponse(HttpStatusCode.OK);
                     return reqeust;
                 });

            return response;
        }
    }
}
