namespace Ui.Helpers
{
    public enum MessageTypes
    {
        SaveSucess=1,
        SaveFailed=2,
        DeleteSuccess=3,
        DeleteFailed=4,
    }
    public enum ShipmentStatus
    {
        deleted = 0, // soft delete
        Created=1,  // when shipment created succesfully it state is 1
        Approved =2, // Reviewer review info and update its state into 2
        Rejected =7,
        ReadyForShipment =3, // Operation Set carrier for shipment and update its status into 3
        Shipped =4,         // operation manager set delivery date and update status into 4
        Delivered =5,       //  admin update state into 5 or 6
        Returned =6
    };
}
