# FinalProject-DenizSelcuk
195-PayCore-Net-Bootcamp Bitirme Projesi

Bu proje 195-PayCore-Net-Bootcamp bitirme projesi kapsamında oluşturulmuştur.

## Nasıl Kullanılır? 
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
<p>/api/Products             : Kullanıcı kendi ürününü günceller. Sahibi olmadığı ürünü güncelleyemez.</p>
<p>/api/Products/MyProducts  : Kullanıcının sahip olduğu ürünler listelenir.</p>
<p>/api/Products/{id}        : Id'ye göre ürün döner.</p>
<p>/api/Products/{id}        : Kullanıcı kendi ürününü siler. Sahibi olmadığı ürünü silemez.</p>
<p>/api/Products/Buy         : Kullanıcı ürün satın alınabilir ise ürünü satın alır. Satılan ürün ilk sahibinin listesinde satıldı olarak işaretlenir, silinmez. Tekrar                                 satılamaz. Ürünü satın alan kullanıcının listesine yeni ürün satılabilir olarak eklenir.</p>

***
![image](https://user-images.githubusercontent.com/42787488/191579994-fb68368d-d474-4ca6-86de-a13883d44cec.png)

<p>/api/Offers                 : Kullanıcı bir ürün için teklif oluşturur. Teklifi yapan kullanıcının outboxOffer listesine, teklif yapılan ürünün sahibinin inboxOffer    listesine teklif eklenir.</p>
<p>/api/Offers                 : Teklifin sahibi kullanıcı ise teklifi yaptığı teklifi günceller.</p>
<p>/api/Offers                 : Teklifin sahibi kullanıcı ise teklifi yaptığı teklifi siler.</p>
<p>/api/Offers/Inbox           : Kullanıcının ürünlerine gelen teklifleri listeler.</p>
<p>/api/Offers/Outbox          : Kullanıcının başka kullanıcıların ürünlerine yaptığı teklifleri listeler.</p>
<p>/api/Offers/Confirm         : Kullanıcı kendi ürününe yapılan bir teklifi onaylar.</p>
<p>/api/Offers/Refuse          : Kullanıcı kendi ürününe yapılan bir teklifi reddeder.</p>

***
## Nasıl Çalıştırılır? 
<ul>1- appsettings.json içerisinde bulunan ConnectionStrings'in Data Source'u uygun şekilde değiştirilir.</ul>
<ul>2- .NET CLI "dotnet ef migrations add InitialCreate" komutu veya PowerShell "Add-Migration InitialCreate" komutu migration işlemi gerçekleştirilir.</ul>
<ul>3- .NET CLI "dotnet ef database update" komutu veya PowerShell "Update-Database" komutu ile databse güncellenir/oluşturulur.</ul>

