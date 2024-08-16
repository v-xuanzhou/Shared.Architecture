namespace UserApi.Msic.CustomerException
{
    public class MyException:Exception
    {
        public MyException(string messageFormat) : base(string.Format(messageFormat)) { }
    }
}
