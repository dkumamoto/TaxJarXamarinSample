using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using taxcalc.DTO.TaxJar;
using taxcalc.Models;

namespace taxcalc.Services.Converters
{
    public class TaxJarConverters
    {
        public TaxJarConverters()
        {
        }

        public TaxJarPostOrder ConvertToPostOrder(Order order)
        {
            TaxJarPostOrder postOrder = new TaxJarPostOrder();
            postOrder.from_zip = order.FromAddress.Zip;
            postOrder.from_country = order.FromAddress.Country;
            postOrder.from_state = order.FromAddress.State;
            postOrder.from_city = order.FromAddress.City;
            postOrder.from_street = order.FromAddress.Street;
            postOrder.to_zip = order.ToAddress.Zip;
            postOrder.to_country = order.ToAddress.Country;
            postOrder.to_state = order.ToAddress.State;
            postOrder.to_city = order.ToAddress.City;
            postOrder.to_street = order.ToAddress.Street;
            var list = new List<PostLine_Items>();
            foreach (var anItem in order.LineItems)
            {
                PostLine_Items orderItem = new PostLine_Items();
                orderItem.id = anItem.ID;
                orderItem.product_tax_code = anItem.TaxCode;
                orderItem.quantity = (int)anItem.Quantity;
                orderItem.unit_price = anItem.UnitPrice;
                orderItem.discount = anItem.Discount;
                list.Add(orderItem);
            }
            postOrder.line_items = list.ToArray();
            return postOrder;
        }

        public Order ConvertFromPostOrder(TaxJarPostOrder postOrder)
        {
            Order order = new Order();
            order.FromAddress = new Address();
            order.FromAddress.Zip = postOrder.from_zip;
            order.FromAddress.Country = postOrder.from_country;
            order.FromAddress.State = postOrder.from_state;
            order.FromAddress.City = postOrder.from_city;
            order.FromAddress.Street = postOrder.from_street;
            order.ToAddress = new Address();
            order.ToAddress.Zip = postOrder.to_zip;
            order.ToAddress.Country = postOrder.to_country;
            order.ToAddress.State = postOrder.to_state;
            order.ToAddress.City = postOrder.to_city;
            order.ToAddress.Street = postOrder.to_street;
            var list = new List<LineItem>();
            foreach (var orderItem in postOrder.line_items)
            {
                LineItem anItem = new LineItem();
                anItem.ID = orderItem.id;
                anItem.TaxCode = orderItem.product_tax_code;
                anItem.Quantity = orderItem.quantity;
                anItem.UnitPrice = orderItem.unit_price;
                anItem.Discount = orderItem.discount;

                list.Add(anItem);
            }
            order.LineItems = list;
            return order;
        }

        public Order ReadFromJson(string orderStr)
        {
            TaxJarPostOrder postOrder = JsonConvert.DeserializeObject<TaxJarPostOrder>(orderStr);
            Order order = ConvertFromPostOrder(postOrder);
            return order;
        }
    }
}
