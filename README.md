- MongoDb veritabanını kullanarak, e-ticaret sitesine ait admin paneli çalışması yapıldı.
  
- Tablolar;
1-Category
2-Product
3-Order
4-OrderLine
5-Customer
tabloları kullanıldı ve hepsi birbiri ile ilişkilendirildi.

- Karmaşık ilişkili tablolardan veriler çekildi.

- Sipariş oluşturma aşamasında, ilişkili tablolardan veri çekildi,

- Sepet oluşturuldu. Sipariş oluşturulurken ürünlerin dinamik olarak stok bilgileri ve fiyat bilgileri hesaplandı. 

- Ürün resimlerini tutmak için local storage kullanıldı. Ürün ekleme, silme ve güncelleme aşamalarının tümünde local storage'den yararlanıldı ve aktif olarak kullanıldı.

# Sipariş Detayı - Siparişe Ürün Ekleme ve Çıkarma - Güncel Sepet Bilgisi - Güncel Stok Bilgisi Detayları
![Ekran görüntüsü 2024-06-13 011004](https://github.com/oguzturan92/MongoDb-ve-LocalStorage/assets/157590022/c895faa0-54c9-4c93-92a6-dc17929a183b)

# Sepete Ürün Ekleme ve Ürün Çıkarma Aşamasında Gerçekleşen İşlemler
![Ekran görüntüsü 2024-06-13 011111](https://github.com/oguzturan92/MongoDb-ve-LocalStorage/assets/157590022/ac0b5554-0d88-4811-96be-5ebd1408dd12)

# İlişkili Tablodan Veri Çekme
![Ekran görüntüsü 2024-06-13 011133](https://github.com/oguzturan92/MongoDb-ve-LocalStorage/assets/157590022/8fa5325f-9ae6-462d-a480-6edc29c384e1)

# Örnek Controller
![Ekran görüntüsü 2024-06-13 011214](https://github.com/oguzturan92/MongoDb-ve-LocalStorage/assets/157590022/5ef5bbb6-05df-48e1-b220-6b99610e340c)

# Ürün listesi
![Ekran görüntüsü 2024-06-13 010921](https://github.com/oguzturan92/MongoDb-ve-LocalStorage/assets/157590022/5ec79b1f-f3dc-433e-8b0b-42a8b40915c3)

# Sipariş Listesi
![Ekran görüntüsü 2024-06-13 010939](https://github.com/oguzturan92/MongoDb-ve-LocalStorage/assets/157590022/40ad7e6a-6521-4675-a8b7-c018cc588cae)
