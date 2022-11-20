namespace ECommerceCommon.Contants
{
    public static class Messages
    {
        public static string NotFoundBasketError(string id) => $"Bu {id}'ye ait bir sepet bilgisi bulunamadı.";

        public static string BasketCreateError = "Sepet kayıt edilirken bir hata meydana geldi";
        public static string BasketDeleteError = "Sepet silinirken bir hata meydana geldi";
        public static string EventBasketDeleteError = "Sepet silme sırasında bir hata meydana geldi.";
        public static string SuccessCreateBasket = "Sepet başarıyla oluşturulmuştur.";
        public static string SuccessDeleteBasket = "Sepet başarıyla silinmiştir.";
        public static string EventOrderCreateError = "Siparişler oluşurken bir hata meydana geldi.";
        public static string SuccessEventOrderCreate = "Siparişler başarıyla oluşturuldu.";
        public static string SuccessOrderCreate = "Siparişler başarıyla oluşturuldu.";
        public static string OrderNullError = "Sipariş listesi bulunamadı.";
    }
}