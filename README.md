# 🏆 Online Quiz System API

## 📌 **Proje Açıklaması**

Online Quiz System API, çevrimiçi bir quiz (sınav) sistemi için geliştirilmiş bir backend API’sidir. Kullanıcıların quiz oluşturmasına, quizlere katılmasına ve sonuçları görmesine olanak tanır. Session yönetimi için Redis kullanılmıştır ve oturum verileri belirli bir süre sonra MySQL veritabanına aktarılır.

## 🚀 **Kullanılan Teknolojiler**

Bu projede aşağıdaki teknolojiler ve kütüphaneler kullanılmıştır:

- **Backend:** .NET (ASP.NET Core)
- **Dil:** C#
- **Veritabanı:** MySQL (Pomelo MySQL)
- **Cache:** Redis (StackExchange.Redis)
- **ORM:** Entity Framework Core
- **Paketler:**
  - AutoMapper
  - ASP.NET MVC
  - Entity Framework Core, Entity Design, Entity Tools
  - StackExchange.Redis (Cache için)
  - Pomelo MySQL

## ⚡ **Kurulum**

Projeyi çalıştırmak için aşağıdaki adımları takip edebilirsin:

1. **Gereksinimleri yükle:**

   - .NET SDK (uygun sürüm)
   - MySQL Server
   - Redis Server

2. **Bağımlılıkları yükle:**

   ```sh
   dotnet restore
   ```

3. **Veritabanını hazırla:**

   - MySQL’de bir veritabanı oluştur.
   - `appsettings.json` dosyasında MySQL bağlantı bilgilerini güncelle.

4. **Projeyi çalıştır:**

   ```sh
   dotnet run
   ```

## 🎯 **Kullanım**

- Quiz oluşturabilir, silebilir ve güncelleyebilirsin.
- Kullanıcılar quizlere katılabilir ve sonuçları alabilir.
- Quiz başlatmak için session oluiturulur,  quiz soruları bittiğinde session sonlandırılır. 
- Session verileri Redis’e kaydedilir ve session bittiginde database aktarılır.

## 🔥 **Katkıda Bulunma**

Bu proje kişisel bir proje olarak geliştirilmiştir, ancak kodu isteyen alıp kullanabilir.

## 📜 **Lisans**

Bu proje herhangi bir lisans içermemektedir.

---

