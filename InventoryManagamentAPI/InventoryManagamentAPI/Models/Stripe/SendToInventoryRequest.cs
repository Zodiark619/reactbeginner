namespace InventoryManagamentAPI.Models.Stripe
{
    
        public class SendToInventoryRequest
        {
            public List<SendToInventoryItemRequest> Items { get; set; }
        }

        public class SendToInventoryItemRequest
    {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
        }
     
}
