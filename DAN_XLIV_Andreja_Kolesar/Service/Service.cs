using System;
using System.Collections.Generic;
using System.Linq;

namespace DAN_XLIV_Andreja_Kolesar.Service
{
    class Service
    {
        #region READ 
        //read menu
        public static List<tblDish> GetMenu()
        
{
            try
            {
                using (dbPizzeria1Entities context = new dbPizzeria1Entities())
                {
                    List<tblDish> list = new List<tblDish>();
                    list = (from x in context.tblDishes select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        //read orders
        public static List<tblOrder> GetAllOrders()
        {
            try
            {
                using (dbPizzeria1Entities context = new dbPizzeria1Entities())
                {
                    List<tblOrder> list = new List<tblOrder>();
                    list = (from x in context.tblOrders select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region ADD ORDER
        public static tblOrder AddNewOrder(tblOrder order)
        {
            try
            {
                using (dbPizzeria1Entities context = new dbPizzeria1Entities())
                {
                    if (order.orderId == 0)
                    {
                        //add 
                        tblOrder newOrder = new tblOrder();
                        newOrder.status = order.status;
                        newOrder.username = order.username;
                        newOrder.dateAndTime = order.dateAndTime;
                        newOrder.dishId = order.dishId;
                        newOrder.count = order.count;
                        context.tblOrders.Add(newOrder);
                        context.SaveChanges();
                        order.orderId = newOrder.orderId;
                        return order;
                    }
                    else
                    {
                        //edit
                        tblOrder orderToEdit = (from x in context.tblOrders where x.orderId == order.orderId select x).FirstOrDefault();
                        orderToEdit.status = order.status;
                        context.SaveChanges();
                        return order;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region DELETE
        public static void DeleteOrder(tblOrder order)
        {
            try
            {
                using (dbPizzeria1Entities context = new dbPizzeria1Entities())
                {
                    tblOrder orderToDelete = (from u in context.tblOrders where u.orderId == order.orderId select u).First();
                    context.tblOrders.Remove(orderToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        #endregion

        #region GET LIST OF ORDERS BY USERNAME
        public static List<tblOrder> GetOrdersByUsername(string username)
        {
            try
            {
                using (dbPizzeria1Entities context = new dbPizzeria1Entities())
                {
                    List<tblOrder> result = (from x in context.tblOrders where x.username == username select x).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region GET PIZZA NAME
        public static string GetPizzaName(int ?dishId)
        {
            try
            {
                using (dbPizzeria1Entities context = new dbPizzeria1Entities())
                {

                    string result = (from x in context.tblDishes where x.dishId == dishId select x.dishName).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region GET TOTAL PRICE
        public static int? GetTotalPrice(int orderId)
        {
            try
            {
                using (dbPizzeria1Entities context = new dbPizzeria1Entities())
                {

                    int ?result = (from x in context.vwOrders where x.orderId == orderId select x.totalPrice).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

    }
}
