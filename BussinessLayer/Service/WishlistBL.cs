using BussinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistRL iwishlistRL;
        public WishlistBL(IWishlistRL iwishlistRL)
        {
            this.iwishlistRL = iwishlistRL;
        }


        //AddBookToWishlist
        public object AddBookToWishlist(CWModel model)
        {
            return iwishlistRL.AddBookToWishlist(model);
        }

        public object ViewWishlist()
        {
            return iwishlistRL.ViewWishlist();
        }

        //RemoveBookFromWishlist
        public object RemoveBookFromWishlist(int wishlistId)
        {
            return iwishlistRL.RemoveBookFromWishlist(wishlistId);    
        }

        //ViewWishlistByUserId
        public object ViewWishlistByUserId(int userId)
        {
            return iwishlistRL.ViewWishlistByUserId(userId);
        }
    }
}
