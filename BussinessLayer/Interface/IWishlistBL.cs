using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IWishlistBL
    {
        public object AddBookToWishlist(CWModel model);
        public object ViewWishlist();
        public object RemoveBookFromWishlist(int wishlistId);
        public object ViewWishlistByUserId(int userId);
    }
}
