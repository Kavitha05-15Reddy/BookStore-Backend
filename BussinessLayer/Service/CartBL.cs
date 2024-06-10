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
    public class CartBL : ICartBL
    {
        private readonly ICartRL icartRL;
        public CartBL(ICartRL icartRL)
        {
            this.icartRL = icartRL;
        }

        //AddBookToCart
        public object AddBookToCart(CWModel model)
        {
            return icartRL.AddBookToCart(model);
        }

        //ViewCart
        public object ViewCart()
        {
            return icartRL.ViewCart();
        }

        //UpdateCart
        public bool UpdateCart(int cartId, int quantity)
        {
            return icartRL.UpdateCart(cartId, quantity);
        }

        //RemoveBookFromCart
        public object RemoveBookFromCart(int cartId)
        {
            return icartRL.RemoveBookFromCart(cartId);
        }

        //ViewCartByUserId
        public object ViewCartByUserId(int userId)
        {
            return icartRL.ViewCartByUserId(userId);
        }

        public object CountOfBooksInCartByUserId(int userId)
        {
            return icartRL.CountOfBooksInCartByUserId(userId);
        }
    }
}
