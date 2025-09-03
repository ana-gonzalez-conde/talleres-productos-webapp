using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Services.OrderService
{
    public class ShoppingCartActions
    {
        public List<CartUnit> cartUnits { get; private set; }

        public ShoppingCartActions()
        {
            cartUnits = new List<CartUnit>();
        }

        public bool AddCartUnit(CartUnit cartUnit)
        {
            // Buscar si ya existe un CartUnit con el mismo ProductId
            var existingCartUnit = cartUnits.Find(cu => cu.ProductId == cartUnit.ProductId);
            if (cartUnit == null || cartUnit.ProductDetails == null)
            {
                throw new ArgumentException("Intento de añadir un cartUnit nulo o sin detalles de producto.");
            }
            if (existingCartUnit != null)
            {
                // Si existe, incrementar la cantidad en 1
                bool enoughStock = existingCartUnit.EnoughStock(1);
                if (enoughStock) existingCartUnit.Quantity++;
                return enoughStock;
            }
            else
            {
                // Si no existe, agregarlo a la lista
                bool enoughStock = cartUnit.EnoughStock(0);
                if (enoughStock) cartUnits.Add(cartUnit);
                return enoughStock;
            }
        }

        public void ResetShoppingCart()
        {
            // Eliminar todos los elementos de la lista
            cartUnits.Clear();
        }

        public long GetTotalItemCount()
        {
            long totalCount = 0;
            foreach (var cartUnit in cartUnits)
            {
                totalCount += cartUnit.Quantity;
            }
            return totalCount;
        }

        public void RemoveCartUnit(long productId)
        {
            cartUnits.RemoveAll(c => c.ProductId == productId);
        }

        public void SetQuantity(long productId, long quantity)
        {
            var cartUnit = cartUnits.Find(c => c.ProductId == productId);
            if (cartUnit != null && quantity > 0)
            {
                cartUnit.Quantity = quantity;
            }
        }

        public float GetTotalPrice()
        {
            return cartUnits.Sum(c => c.ProductDetails.Price * c.Quantity);
        }

        public List<OrderLinePurchaseDetails> RetrieveOrderLinesFromCart()
        {
            return cartUnits.Select(cartUnit => new OrderLinePurchaseDetails(
                cartUnit.ProductId,
                (int)cartUnit.Quantity,
                Convert.ToDecimal(cartUnit.ProductDetails.Price)
            )).ToList();
        }

        public override bool Equals(object obj)
        {
            return obj is ShoppingCartActions actions &&
                   EqualityComparer<List<CartUnit>>.Default.Equals(cartUnits, actions.cartUnits);
        }

        public override int GetHashCode()
        {
            return 295182282 + EqualityComparer<List<CartUnit>>.Default.GetHashCode(cartUnits);
        }


    }
}