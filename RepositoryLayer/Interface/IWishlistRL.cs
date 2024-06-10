using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        public object AddBookToWishlist(CWModel model);
        public object ViewWishlist();
        public object RemoveBookFromWishlist(int wishlistId);
        public object ViewWishlistByUserId(int userId);
    }
}
