namespace effectively.ExtractAndOverride
{
    public class OrderService
    {
        public bool Add(Order order)
        {
            var userService = GetUserService();
            if (userService.IsValidUser && order.Amount > 0)//Can the current user save an order
            {
                //save order
                return true;
            }
            else
            {
                //dont add send an exception and log it
                return false;
            }
        }

        protected virtual UserService GetUserService()
        {
            return new UserService();
        }
    }
}
    