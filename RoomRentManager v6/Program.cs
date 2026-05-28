class Program
{
    static void Main()
    {
        using FileLogger logger = new FileLogger("roomrent-log.txt");

        // Delegate: service classes receive a method reference instead of depending directly on FileLogger.
        LogAction logAction = logger.WriteLog;

        HouseService houseService = new HouseService(logAction);
        RenterService renterService = new RenterService(logAction);
        BookingService bookingService = new BookingService(logAction);
        PaymentService paymentService = new PaymentService(logAction);
        ReportService reportService = new ReportService(houseService, renterService, bookingService);
        DemoDataSeeder demoDataSeeder = new DemoDataSeeder(houseService, renterService, bookingService, paymentService);
        ConsoleInput input = new ConsoleInput();

        ConsoleMenu menu = new ConsoleMenu(
            houseService,
            renterService,
            bookingService,
            paymentService,
            reportService,
            demoDataSeeder,
            input,
            logAction);

        menu.Run();
    }
}
