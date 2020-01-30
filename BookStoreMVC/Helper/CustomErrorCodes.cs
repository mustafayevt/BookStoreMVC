namespace BookStoreMVC.Helper
{
    public class CustomErrorCodes
    {
        public enum AccountErrors
        {
            UserNotFound,
            PasswordIsWrong,
            Unauthorized,
            Ok,
            CantCreate,
            UsernameAlreadyExists,
            EmailAlreadyExists,
            Exception
        }

        public enum AdErrors
        {
            Ok
        }
    }
}