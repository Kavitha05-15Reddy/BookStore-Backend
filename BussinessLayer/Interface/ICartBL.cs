﻿using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface ICartBL
    {
        public object AddBookToCart(CWModel model);
        public object ViewCart();
        public bool UpdateCart(int cartId, int quantity);
        public object RemoveBookFromCart(int cartId);
        public object ViewCartByUserId(int userId);
        public object CountOfBooksInCartByUserId(int userId);
    }
}