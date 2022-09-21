# FinalProject-DenizSelcuk
195-PayCore-Net-Bootcamp Bitirme Projesi

Bu proje 195-PayCore-Net-Bootcamp bitirme projesi kapsamında oluşturulmuştur.

# Nasıl Kullanılır? 
![image](https://user-images.githubusercontent.com/42787488/191574680-ef2062ff-e1e5-44e5-b14d-a1b0e76c93cb.png)

{
  "userName": "string",
  "email": "string",
  "password": "string"
}
Bilgileri ile uygulamaya kayıt olunur.
***
![image](https://user-images.githubusercontent.com/42787488/191574862-939ebe28-d49b-4a9b-8ca9-e5a72c6fe824.png)
{
  "email": "string",
  "password": "string"
}
Bilgileri ile token oluşturulur.
***
![image](https://user-images.githubusercontent.com/42787488/191575065-f00591f0-ddc9-4e72-a39c-dd2ab6dbbfdc.png)
Categories: Kategori CRUD işlemleri yapılır.

***
![image](https://user-images.githubusercontent.com/42787488/191577945-61471eb5-2699-4994-960b-59f82ff45281.png)
<p>/api/Products             : Veri tabanındaki tüm ürünler listelenir.</p>
<p>/api/Products             : Veri tanına yeni bir ürün kaydedilir. Ürün sahibi kullanıcıya otomatik olarak atanır.</p>
/api/Products             : Kullanıcı kendi ürününü günceller. Sahibi olmadığı ürünü güncelleyemez.
/api/Products/MyProducts  : Kullanıcının sahip olduğu ürünler listelenir.
/api/Products/{id}        : Id'ye göre ürün döner.
/api/Products/{id}        : Kullanıcı kendi ürününü siler. Sahibi olmadığı ürünü silemez.
/api/Products/Buy         : Kullanıcı ürün satın alınabilir ise ürünü satın alır. Satılan ürün ilk sahibinin listesinde satıldı olarak işaretlenir, *silinmez*. Tekrar                                 satılamaz. Ürünü satın alan kullanıcının listesine yeni ürün satılabilir olarak eklenir.
***
