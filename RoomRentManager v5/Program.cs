class Program
{
    static void Main()
    {
        using Logger logger = new Logger();

        LogAction logAction = logger.WriteLog;

        HouseService houseService = new HouseService(logAction);
        RenterService renterService = new RenterService(logAction);
        BookingService bookingService = new BookingService(logAction);
        PaymentService paymentService = new PaymentService(logAction);

        ConsoleMenu menu = new ConsoleMenu(houseService, renterService, bookingService, paymentService);
        menu.Run();
    }
}
